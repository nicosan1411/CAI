using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Linq;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class GuiaAlmacen
    {
        private static List<GuiaEntidad> guias = new List<GuiaEntidad>();

        // Ruta en tiempo de ejecución (carpeta de la aplicación / bin\Debug)
        private static readonly string DatosFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");
        private static readonly string GuiasPath = Path.Combine(DatosFolder, "Guias.json");

        // Ruta proyectual (intento de escribir también en la carpeta Datos del proyecto)
        private static readonly string ProjectGuiasPath = ComputeProjectGuiasPath();

        public static IReadOnlyCollection<GuiaEntidad> Guias => guias.AsReadOnly();

        static GuiaAlmacen()
        {
            try
            {
                // Intentamos cargar desde la ruta de ejecución primero
                if (File.Exists(GuiasPath))
                {
                    var GuiaJson = File.ReadAllText(GuiasPath);
                    guias = JsonSerializer.Deserialize<List<GuiaEntidad>>(GuiaJson) ?? new List<GuiaEntidad>();
                }
                else if (!string.IsNullOrEmpty(ProjectGuiasPath) && File.Exists(ProjectGuiasPath))
                {
                    // Si no existe en ejecución, intentamos cargar desde la ruta del proyecto (útil en desarrollo)
                    var GuiaJson = File.ReadAllText(ProjectGuiasPath);
                    guias = JsonSerializer.Deserialize<List<GuiaEntidad>>(GuiaJson) ?? new List<GuiaEntidad>();
                }
                else
                {
                    guias = new List<GuiaEntidad>();
                }

                Debug.WriteLine($"GuiaAlmacen inicializado. GuiasPath = {GuiasPath}. ProjectGuiasPath = {ProjectGuiasPath}. Count = {guias.Count}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GuiaAlmacen ctor error: {ex}");
                guias = new List<GuiaEntidad>();
            }
        }

        public static void Grabar()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                };

                var GuiaJson = JsonSerializer.Serialize(guias, options);

                // Escribir en la carpeta de ejecución (siempre)
                Directory.CreateDirectory(DatosFolder);
                File.WriteAllText(GuiasPath, GuiaJson);
                Debug.WriteLine($"GuiaAlmacen.Grabar: escrito runtime {GuiasPath}. Existe: {File.Exists(GuiasPath)}");

                // Intentar escribir también en la carpeta Datos del proyecto (si detectada)
                if (!string.IsNullOrWhiteSpace(ProjectGuiasPath))
                {
                    try
                    {
                        var projectDatosFolder = Path.GetDirectoryName(ProjectGuiasPath);
                        if (!string.IsNullOrWhiteSpace(projectDatosFolder))
                            Directory.CreateDirectory(projectDatosFolder);

                        File.WriteAllText(ProjectGuiasPath, GuiaJson);
                        Debug.WriteLine($"GuiaAlmacen.Grabar: escrito project {ProjectGuiasPath}. Existe: {File.Exists(ProjectGuiasPath)}");
                    }
                    catch (Exception exProj)
                    {
                        Debug.WriteLine($"GuiaAlmacen.Grabar: error al escribir en ProjectGuiasPath: {exProj}");
                        // No interrumpimos la operación por este fallo; el runtime file ya está escrito.
                    }
                }

                // Volcar un fragmento para verificación rápida en Output
                var contenidoRuntime = File.ReadAllText(GuiasPath);
                Debug.WriteLine($"Contenido runtime (truncado 400): {(contenidoRuntime.Length > 400 ? contenidoRuntime.Substring(0, 400) + "..." : contenidoRuntime)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GuiaAlmacen.Grabar error: {ex}");
                throw;
            }
        }

        public static void Agregar(GuiaEntidad guia)
        {
            if (guia == null) return;
            guias.Add(guia);
            Grabar();
        }

        // ----- Helpers -----
        private static string ComputeProjectGuiasPath()
        {
            try
            {
                // Subimos desde el base directory buscando el .csproj (máx 10 niveles)
                var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                for (int i = 0; i < 10 && dir != null; i++)
                {
                    // Si encontramos .csproj asumimos que es la raíz del proyecto
                    if (dir.GetFiles("*.csproj").Any())
                    {
                        var projectDatos = Path.Combine(dir.FullName, "Datos", "Guias.json");
                        return Path.GetFullPath(projectDatos);
                    }
                    dir = dir.Parent;
                }

                // Fallback: subir dos niveles (bin\Debug -> proyecto)
                var fallback = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Datos", "Guias.json"));
                return fallback;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ComputeProjectGuiasPath error: {ex}");
                return null;
            }
        }
    }
}