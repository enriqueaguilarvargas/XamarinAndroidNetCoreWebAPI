namespace NETCoreWebAPISQLServerAzure.Controllers
{
    public class Clientes
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Correo { get; set; }
        public int Edad { get; set; }
        public double Saldo { get; set; }
    }
}