using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Web.Domain;
using Web.Repositories.Interface;

namespace Web.Controllers
{
    [Route("Orders")]
    public class OrderController : ControllerBase
    {
        #region Properties

        private readonly IOrderRepository _orderRepo;

        #endregion

        #region Constructor

        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        #endregion

        /// <summary>
        /// 取得訂單全部資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<string> Orders()
        {
            return JsonConvert.SerializeObject(await _orderRepo.GetAllAsync());
        }

        /// <summary>
        /// 依據ID取得訂單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<string> Order(string id)
        {
            return JsonConvert.SerializeObject(await _orderRepo.FindAsync(id));
        }

        /// <summary>
        /// 刪除訂單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            await _orderRepo.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// 新增訂單資料
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertOrder([FromBody]Order Order)
        {
            await _orderRepo.InsertAsync(Order);
            return Ok();
        }

        /// <summary>
        /// 更新訂單資料
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOrder([FromBody]Order Order)
        {
            await _orderRepo.UpdateAsync(Order);
            return Ok();
        }
    }
}
