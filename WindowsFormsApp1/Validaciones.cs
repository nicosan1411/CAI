using System;
using System.Linq;

namespace WindowsFormsApp1
{
    internal static class Validaciones
    {
        /// <summary>
        /// DNI argentino: exactamente 8 dígitos (sin puntos/guiones).
        /// </summary>
        public static bool EsDniValido(string dni, out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(dni))
            {
                error = "Ingresá el DNI del destinatario.";
                return false;
            }

            var limpio = new string(dni.Where(char.IsDigit).ToArray());

            if (limpio.Length != 8)
            {
                error = "El DNI debe tener exactamente 8 dígitos.";
                return false;
            }

            if (!limpio.All(char.IsDigit))
            {
                error = "El DNI debe ser numérico.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Requerido genérico para textos.
        /// </summary>
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

        /// <summary>
        /// Requerido genérico para combos (texto visible).
        /// </summary>
        public static bool ComboRequerido(string textoCombo, string nombreCampo, out string error)
        {
            return Requerido(textoCombo, nombreCampo, out error);
        }

        /// <summary>
        /// Devuelve sólo los dígitos de un string (útil si querés normalizar antes de guardar).
        /// </summary>
        public static string SoloDigitos(string s) => new string((s ?? "").Where(char.IsDigit).ToArray());
    }
}
