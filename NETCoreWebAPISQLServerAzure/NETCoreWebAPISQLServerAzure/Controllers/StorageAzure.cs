using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace NETCoreWebAPISQLServerAzure.Controllers
{
    public class StorageAzure
    {
        public List<Clientes> ListadeClientes;
        public bool Almacenar(string Nombre, string Domicilio, 
            string Correo, int Edad, double Saldo)
        {
            var connect = new SqlConnection
                ("Server=tcp:enriqueazure.database.windows.net,1433;" +
                "Initial Catalog=informacion;" +
                "Persist Security Info=False;" +
                "User ID=enrique;" +
                "Password=Mexico2018;" +
                "MultipleActiveResultSets=False;" +
                "Encrypt=True;" +
                "TrustServerCertificate=False;" +
                "Connection Timeout=30;");
            var query = new SqlCommand
                ("INSERT INTO Datos (Nombre, Domicilio, Correo, Edad, Saldo) VALUES " +
                "('" + Nombre + "','" + Domicilio + "','" + Correo + "','" + 
                Edad + "','" + Saldo + "')", connect);
            try
            {
                connect.Open();
                query.ExecuteNonQuery();
                connect.Close();
                return true;
            }
            catch (SqlException ex)
            {
                connect.Close();
                return false;
            }
        }
        public List<Clientes> Consulta(int ID)
        {
            var dt = new DataTable();
            var connect = new SqlConnection
                ("Server=tcp:enriqueazure.database.windows.net,1433;" +
                "Initial Catalog=informacion;" +
                "Persist Security Info=False;" +
                "User ID=enrique;" +
                "Password=Mexico2018;" +
                "MultipleActiveResultSets=False;" +
                "Encrypt=True;" +
                "TrustServerCertificate=False;" +
                "Connection Timeout=30;");
            var cmd = new SqlCommand
                ("SELECT * From Datos WHERE ID='" + ID + "'", connect);
            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ListadeClientes = new List<Clientes>();
            Clientes cliente = new Clientes();
            cliente.ID = int.Parse((dt.Rows[0]["ID"]).ToString());
            cliente.Nombre = (dt.Rows[0]["Nombre"]).ToString();
            cliente.Domicilio = (dt.Rows[0]["Domicilio"]).ToString();
            cliente.Correo = (dt.Rows[0]["Correo"]).ToString();
            cliente.Edad = int.Parse((dt.Rows[0]["Edad"]).ToString());
            cliente.Saldo = double.Parse((dt.Rows[0]["Saldo"]).ToString());
            ListadeClientes.Add(cliente);
            return ListadeClientes;
        }
    }
}
