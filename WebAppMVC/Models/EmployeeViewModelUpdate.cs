namespace WebAppMVC.Models
{
    public class EmployeeViewModelUpdate
    {

        public int Id { get; set; }
        public string EstadoCivil { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Puesto { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
