using API_A_HOME.Models.response;
using RestSharp;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using static API_A_HOME.Models.response.Response;
using NLog;

namespace API_A_HOME.Adapter
{
    public static class CheckAcc
    {

        public static string GetAcc(int txn_id, string account)
        { 
            Logger logger = LogManager.GetCurrentClassLogger();

            var options = new RestClientOptions("https://195.191.5.17/amra_osmp.php");
            options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            var client = new RestClient(options);
            
            var request = new RestRequest($"/payment_app.cgi?command=check&txn_id={txn_id}&account={account}", Method.Get);
            request.AddHeader("Authorization", "Basic YW1yYTpiYW5rMTMyMQ==");

            RestResponse response;

            try
            {
                response = client.Execute(request);

                if (response != null)
                {
                    logger.Info("Клиент найден");
                }
                else
                {
                    logger.Info("Клиент не найден");
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Response));

                using (StringReader reader = new StringReader(response.Content))
                {
                    var test = (Response)serializer.Deserialize(reader);
                }

            }

            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }

            return response.Content;
        }
    }
}