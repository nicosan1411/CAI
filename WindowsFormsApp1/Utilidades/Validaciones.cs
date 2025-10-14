using System;
using System.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Reglas de validación y normalización de inputs.
    /// </summary>
    internal static class Validaciones
    {
        /// <summary>
        /// Valida DNI argentino: exactamente 8 dígitos (sin puntos/guiones).
        /// Devuelve false y el mensaje de error si no cumple.
        /// </summary>
        public static bool EsDniValido(string dni, out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(dni))
            {
                error = "Ingresá el DNI del destinatario.";
                return false;
            }

            // Normalizo a dígitos y valido largo exacto
            var limpio = new string(dni.Where(char.IsDigit).ToArray());
            if (limpio.Length != 8)
            {
                error = "El DNI debe tener exactamente 8 dígitos.";
                return false;
            }

            return true;
        }

        /// <summary>Requerido genérico para textos.</summary>
        public static bool Requerido(string valor, string nombreCampo, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(valor))
            {
                error = $"Ingresá {nombreCampo}.";
                return false;
            }
            return true;
        }

        /// <summary>Requerido genérico para combos (texto visible).</summary>
        public static bool ComboRequerido(string textoCombo, string nombreCampo, out string error) =>
            Requerido(textoCombo, nombreCampo, out error);

        /// <summary>Devuelve sólo los dígitos de un string (útil para normalizar antes de guardar).</summary>
        public static string SoloDigitos(string s) => new string((s ?? "").Where(char.IsDigit).ToArray());
    }
}
