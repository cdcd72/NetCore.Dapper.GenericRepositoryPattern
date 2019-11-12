using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Web.Domain;
using Web.Repositories.Interface;

namespace Web.Controllers
{
    [Route("Customers")]
    public class CustomerController : ControllerBase
    {
        #region Properties

        private readonly ICustomerRepository _customerRepo;

        #endregion

        #region Constructor

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        #endregion

        /// <summary>
        /// 取得全部客戶資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<string> Customers()
        {
            return JsonConvert.SerializeObject(await _customerRepo.GetAllAsync());
        }

        /// <summary>
        /// 依據ID取得客戶資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<string> Customer(string id)
        {
            return JsonConvert.SerializeObject(await _customerRepo.FindAsync(id));
        }

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _customerRepo.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertCustomer([FromBody]Customer Customer)
        {
            await _customerRepo.InsertAsync(Customer);
            return Ok();
        }

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCustomer([FromBody]Customer Customer)
        {
            await _customerRepo.UpdateAsync(Customer);
            return Ok();
        }
    }
}
