using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Threading.Tasks;
using System.Net;
using System;
using Newtonsoft.Json;
using System.Json;
using System.Collections.Generic;
using Android.Runtime;
using Newtonsoft.Json.Linq;
using System.IO;
namespace AccesoSQLServerAzure
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txtNombre, txtDomicilio, txtCorreo, txtEdad, txtSaldo, txtID;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            txtID = FindViewById<EditText>(Resource.Id.txtid);
            txtNombre = FindViewById<EditText>(Resource.Id.txtnombre);
            txtDomicilio = FindViewById<EditText>(Resource.Id.txtdomicilio);
            txtCorreo = FindViewById<EditText>(Resource.Id.txtcorreo);
            txtEdad = FindViewById<EditText>(Resource.Id.txtedad);
            txtSaldo = FindViewById<EditText>(Resource.Id.txtsaldo);
            var btnAlmacenar = FindViewById<Button>(Resource.Id.btnguardar);
            var btnConsultar = FindViewById<Button>(Resource.Id.btnbuscar);
            btnConsultar.Click += async delegate
            {
                try
                {
                    int ID = int.Parse(txtID.Text);
                    var API = "http://enriqueaguilar2018.azurewebsites.net/api/values/ConsultarSQLServer?ID=" + ID;
                    JsonValue json = await Datos(API);
                    Transform(json);
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            };
            btnAlmacenar.Click += delegate
            {
                try
                {
                    var Nombre = txtNombre.Text;
                    var Domicilio = txtDomicilio.Text;
                    var Correo = txtCorreo.Text;
                    var Edad = int.Parse(txtEdad.Text);
                    var Saldo = double.Parse(txtSaldo.Text);
                    var API = "http://enriqueaguilar2018.azurewebsites.net/api/values/AlmacenarSQLServer?Nombre=" +
                        Nombre + "&Domicilio=" + Domicilio + "&Correo=" + Correo + "&Edad=" + Edad +
                        "&Saldo=" + Saldo + "";
                    var request = (HttpWebRequest)WebRequest.Create(API);
                    WebResponse response = request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseText = reader.ReadToEnd();
                    Toast.MakeText(this, responseText.ToString(), ToastLength.Long).Show();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            };
        }
        public void Transform(JsonValue json)
        {
            try
            {
                var Resultados = json[0];
                txtNombre.Text = Resultados["nombre"];
                txtCorreo.Text = Resultados["correo"];
                txtDomicilio.Text = Resultados["domicilio"];
                txtEdad.Text = Resultados["edad"].ToString();
                txtSaldo.Text = Resultados["saldo"].ToString();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }
        public async Task<JsonValue> Datos(string API)
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(API));
            request.ContentType = "application/json";
            request.Method = "GET";
            using (WebResponse response = await request.GetResponseAsync())
            {
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    var jsondoc = await Task.Run(() => JsonValue.Load(stream));
                    return jsondoc;
                }
            }
        }
    }
}