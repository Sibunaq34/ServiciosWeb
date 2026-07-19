namespace ServiciosMedicos.Entities
{
    public class SeguridadLog
    {
        public int IdUsuario { get; set; }

        public string Usuario { get; set; } = string.Empty;

        public string NombreCompleto { get; set; } = string.Empty;

        public string PasswordCifrada { get; set; } = string.Empty;

        public bool Activo { get; set; }

        public string Estado { get; set; } = string.Empty;

        public int IntentosFallidos { get; set; }

        public int IdRol { get; set; }

        public string NombreRol { get; set; } = string.Empty;
    }
}
