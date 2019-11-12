using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Web.Core;
using Web.Domain;
using Web.Repositories.Interface;

namespace Web.Repositories.Implement
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        #region Constructor

        public OrderRepository(IDbConnection connection) : base(connection) { }

        #endregion

        /// <summary>
        /// 新增訂單資料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override async Task<int> InsertAsync(Order entity)
        {
            int result;

            using (var db = Conn)
            {
                string sql = @"Insert into Orders 
                                (CustomerID, EmployeeID, ShipVia) 
                               Values 
                                (@CustomerID, @EmployeeID, @ShipVia)";

                result = await db.ExecuteAsync(sql, entity);
            }

            return result;
        }

        /// <summary>
        /// 取得訂單全部資料
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            IEnumerable<Order> orders;

            using (var db = Conn)
            {
                string sql = @"Select * from Orders";

                orders = await db.QueryAsync<Order>(sql);
            }

            return orders;
        }

        /// <summary>
        /// 依據ID取得訂單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Order> FindAsync(object id)
        {
            Order order;

            using (var db = Conn)
            {
                string sql = @"Select * from Orders where OrderID = @id";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("id", id);

                order = await db.QueryFirstAsync<Order>(sql, parameters);
            }

            return order;
        }

        /// <summary>
        /// 更新訂單資料
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        public override async Task<int> UpdateAsync(Order entityToUpdate)
        {
            int result;

            using (var db = Conn)
            {
                string sql = @"Update Orders Set 
                                 ShipName = @ShipName
                                Where OrderID = @OrderID";

                result = await db.ExecuteAsync(sql, entityToUpdate);
            }

            return result;
        }

        /// <summary>
        /// 刪除訂單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(object id)
        {
            int result;

            using (var db = Conn)
            {
                string sql = @"Delete Orders where OrderID = @id";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("id", id);

                result = await db.ExecuteAsync(sql, parameters);
            }

            return result;
        }
    }
}
