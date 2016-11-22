using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UFSCar.BD.Commom.Autenticacao
{
    public class Token
    {
        public decimal ID { get; set; }
        public string token { get; set; }
        public DateTime Data { get; set; }
        public int StatusCode { get; set; }
        public string Ambiente { get; set; }

        public UsuarioToken Usuario { get; set; }

        public Ambiente GetAmbiente(string secretKey)
        {
            //return JsonConvert.DeserializeObject<Ambiente>(UFSCar.Core.Security.BasicEncryptor.Decrypt(this.Ambiente, true, secretKey));
            return new Autenticacao.Ambiente();
        }
    }

    public class UsuarioToken
    {
        public int Chapa { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string CEP { get; set; }
        public string Apelido { get; set; }
        public int CentroCusto { get; set; }
        public string Cargo { get; set; }

        public string URLFoto
        {
            get
            {
                return string.Format("http://adi.UFSCar.com.br/fotos/{0}.jpg", Chapa);
            }
        }

        public List<PerfilToken> PerfisAcesso { get; set; }

        public List<Acoes> Acoes { get; set; }

        public List<RegiaoComercialToken> Regioes { get; set; }

        private bool _ATV = false;
        private bool _GCD = false;
        private bool _GMR = false;

        public bool ATV { get { return Regioes != null && Regioes.Any(d => d.ATV == true); } set { this._ATV = value; } }
        public bool GCD { get { return Regioes != null && Regioes.Any(d => d.GCD == true); } set { this._GCD = value; } }
        public bool GMR { get { return Regioes != null && Regioes.Any(d => d.GMR == true); } set { this._GMR = value; } }

        

        public string RegiaoComercial
        {
            get
            {
                if (Regioes != null && Regioes.Count > 0)
                {
                    if (Regioes.Count(d => d.Responsavel == "S") > 0)
                        return Regioes.Where(d => d.Responsavel == "S").FirstOrDefault().RegiaoComercial;
                    else
                    {
                        return Regioes.FirstOrDefault().RegiaoComercial;
                    }
                }
                else
                {
                    _ATV = _GCD = _GMR = false;
                    return string.Empty;
                }
            }
        }

        public List<string> RegioesComerciais
        {
            get
            {
                if (Regioes != null && Regioes.Count > 0)
                {
                    List<string> lista = new List<string>();
                    Regioes.ForEach(d => lista.Add(d.RegiaoComercial));
                    return lista;
                }
                else
                {
                    return new List<string>();
                }
            }
        }
    }

    public class RegiaoComercialToken
    {
        public string RegiaoComercial { get; set; }
        public string RegiaoComercialMascara { get; set; }
        public bool ATV { get; set; }
        public bool GCD { get; set; }
        public bool GMR { get; set; }
        public string Responsavel { get; set; }
    }

    public class PerfilToken
    {
        public int IDPerfil { get; set; }
        public string Nome { get; set; }
        public bool EhCargo { get; set; }
    }

    public class Acoes
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public string Target { get; set; }
        public string Target2 { get; set; }
        public Programa Programa { get; set; }
    }

    public class Programa
    {
        public int CodigoModulo { get; set; }
        public int CodigoPrograma { get; set; }
        public string Descricao { get; set; }
    }

    public class Ambiente
    {
        private static Ambiente instance;
        private static string cryptedStr
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["cookiecryptedStr"] != null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["cookiecryptedStr"];
                    return cookie.Value.ToString();
                }
                else
                    return string.Empty;
            }
            set
            {
                
                HttpCookie cookie = new HttpCookie("cookiecryptedStr");
                cookie.Value = value.ToString();
                cookie.Expires = DateTime.Now.AddHours(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        public string ServerName { get; set; }
        public string LogID { get; set; }
        public string LogPass { get; set; }
        public string UserID { get; set; }
        public string UserPass { get; set; }
        public string Database { get; set; }
        public string IPAddress { get; set; }
        public string Username { get; set; }


        public static string GetString(string ambiente)
        {
            return System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + ambiente);
        }

        public static Ambiente GetInstace(string ambiente)
        {
            return GetByService(ambiente);
        }

        public static Ambiente GetByService(string ambiente)
        {
            if (instance == null)
            {
                if(string.IsNullOrEmpty(cryptedStr))
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    client.Headers.Add("content-type", "application/json");

                    var url = System.Configuration.ConfigurationManager.AppSettings["URL.API.ACESSO"] + "id/ambiente/" + ambiente;
                    cryptedStr = Encoding.UTF8.GetString(client.UploadData(url, "POST", Encoding.Default.GetBytes(""))).Replace("\"","");
                }

                //instance = JsonConvert.DeserializeObject<Ambiente>(UFSCar.Core.Security.BasicEncryptor.Decrypt(cryptedStr, true, System.Configuration.ConfigurationManager.AppSettings["SecretKey"].ToString()));
                instance = JsonConvert.DeserializeObject<Ambiente>("");
            }
                
            return instance;
        }
    }
}
