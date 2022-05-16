namespace WebAppMVC.Models
{
    public class EmployeeParams
    {
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public bool Estatus { get; set; }

        public EmployeeParams(string nombre, string rfc, bool estatus)
        {
            Nombre = nombre;
            Rfc = rfc;
            Estatus = estatus;
        }
    }
}
