using Microsoft.AspNetCore.Mvc;
namespace NETCoreWebAPISQLServerAzure.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet("ConsultarSQLServer")]
        public JsonResult Consultar(int ID)
        {
            var Consulta = new StorageAzure();
            var Lista = Consulta.Consulta(ID);
            return new JsonResult(Lista);
        }
        [HttpGet("AlmacenarSQLServer")]
        public string Almacenar(string Nombre, string Domicilio, string Correo,
            int Edad, double Saldo)
        {
            var Almacena = new StorageAzure();
            bool resultado = Almacena.Almacenar(Nombre, Domicilio, Correo, Edad, Saldo);
            if (resultado == true)
                return "Almacenado en SQL Server sobre Azure desde .Net Core Web API";
            else
                return "No almacenado";
        }
    }
}
