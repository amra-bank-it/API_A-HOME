using API_A_HOME.Adapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_A_HOME.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        [HttpGet(Name = "Pay")]

        public string Pay(int txn_id, string account, double sum)
        {
            return PayAcc.ReqPay(txn_id , account , sum);
        }
    }
}
