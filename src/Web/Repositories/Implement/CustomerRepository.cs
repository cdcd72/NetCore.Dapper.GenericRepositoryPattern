using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Web.Core;
using Web.Domain;
using Web.Repositories.Interface;

namespace Web.Repositories.Implement
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        #region Constructor

        public CustomerRepository(IDbConnection connection) : base(connection) { }

        #endregion

        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override async Task<int> InsertAsync(Customer entity)
        {
            int result;

            using (var db = Conn)
            {
                string sql = @"Insert into Customers 
                                (CustomerID, CompanyName) 
                               Values 
                                (@CustomerID, @CompanyName)";

                result = await db.ExecuteAsync(sql, entity);
            }

            return result;
        }

        /// <summary>
        /// 取得全部客戶資料
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Customer>> GetAllAsync()
        {
            IEnumerable<Customer> customers;

            using (var db = Conn)
            {
                string sql = @"Select * from Customers";

                customers = await db.QueryAsync<Customer>(sql);
            }

            return customers;
        }

        /// <summary>
        /// 依據ID取得客戶資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Customer> FindAsync(object id)
        {
            Customer customer;

            using (var db = Conn)
            {
                string sql = @"Select * from Customers where CustomerID = @id";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("id", id);

                customer = await db.QueryFirstAsync<Customer>(sql, parameters);
            }

            return customer;
        }

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        public override async Task<int> UpdateAsync(Customer entityToUpdate)
        {
            int result;

            using (var db = Conn)
            {
                string sql = @"Update Customers Set 
                                 CompanyName = @CompanyName
                                Where CustomerID = @CustomerID";

                result = await db.ExecuteAsync(sql, entityToUpdate);
            }

            return result;
        }

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(object id)
        {
            int result;

            using (var db = Conn)
            {
                string sql = @"Delete Customers where CustomerID = @id";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("id", id);

                result = await db.ExecuteAsync(sql, parameters);
            }

            return result;
        }

        /// <summary>
        /// 一些客製化非同步方法
        /// </summary>
        /// <returns></returns>
        public Task<bool> SomeCustomMethodAsync()
        {
            throw new NotImplementedException();
        }
    }
}
