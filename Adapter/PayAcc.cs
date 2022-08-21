using API_A_HOME.Models.response;
using RestSharp;
using System.Xml.Serialization;

namespace API_A_HOME.Adapter
{
    public class PayAcc
    {
        public static string ReqPay(int txn_id, string account, decimal sum)
        {
            var options = new RestClientOptions("https://pay.a-home.biz/osmp_amra_test.php");
            options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            var client = new RestClient(options);
            var request = new RestRequest($"/payment_app.cgi?command=pay&txn_id={txn_id}&txn_date=20050815120133&account={account}&sum={sum}", Method.Get);
            request.AddHeader("Authorization", "Basic YW1yYXRlc3Q6YW1yYXRlc3Q=");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);


            var response = client.Execute(request);

            IsNullException(response);


            ResponsePay rp = GetPaymentCustomerFromJSON(response);



            return response.Content;
        }

        private static ResponsePay GetPaymentCustomerFromJSON(RestResponse? response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.ErrorException.ToString());


            XmlSerializer serializer = new XmlSerializer(typeof(ResponsePay));

            StringReader reader = new StringReader(response.Content);

            var test = (ResponsePay)serializer.Deserialize(reader);



            if (test.Result != 0)

                throw new Exception($"Платеж не успешный, ошибка провайдера!");

            return test;
        }

        private static void IsNullException(RestResponse? response)
        {
            if (response == null)
                throw new Exception("Ответ запроса на проверку равен NULL ");
        }
    }
}