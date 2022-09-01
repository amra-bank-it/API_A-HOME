using API_A_HOME.Models.response;
using RestSharp;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using static API_A_HOME.Models.response.ResponseCheck;
using NLog;
using Newtonsoft.Json;

namespace API_A_HOME.Adapter
{
    public static class CheckAcc
    {

        public static string GetAcc(int txn_id, string account)
        { 
            Logger logger = LogManager.GetCurrentClassLogger();

            var configuration = new Configuration();
            var appSettings = configuration.Get("AppSettings"); // null
            var token = configuration.Get("token"); // null

            var options = new RestClientOptions("https://195.191.5.17/amra_osmp.php");

            options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            var client = new RestClient(options);
            
            var request = new RestRequest($"/payment_app.cgi?command=check&txn_id={txn_id}&account={account}", Method.Get);
            request.AddHeader("Authorization", "Basic YW1yYTpiYW5rMTMyMQ==");

            var response = client.Execute(request);

            IsNullException(response);

            ResponseCheck rc = GetCustomerFromXML(response);

            return response.Content;
        }
        private static ResponseCheck GetCustomerFromXML(RestResponse? response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.ErrorException.ToString());


            XmlSerializer serializer = new XmlSerializer(typeof(ResponseCheck));

            StringReader reader = new StringReader(response.Content);
            
            var test = (ResponseCheck)serializer.Deserialize(reader);
            

            if (test.Result != 0)

                throw new Exception("Произошла ошибка при попытке поиска клиента в базе A-HOME");
            return test;
        }

        private static void IsNullException(RestResponse? response)
        {
            if (response == null)
                throw new Exception("Ответ запроса на проверку равен NULL ");
        }
    }
}