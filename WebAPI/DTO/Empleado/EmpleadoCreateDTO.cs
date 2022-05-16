namespace WebAPI.DTO.PersonaFisica
{
    public class EmpleadoCreateDTO
    {
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int? Edad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Genero { get; set; }
        public string? EstadoCivil { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Puesto { get; set; }
        public string? Rfc { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
