using API_A_HOME.Adapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_A_HOME.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        [HttpGet(Name = "Check")]

        public string Check(int txn_id, string account)
        {
            return CheckAcc.GetAcc(txn_id, account);
        }
    }
}
