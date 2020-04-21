using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Web.Core.Interfaces;

namespace Web.Core
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Properties

        protected IDbConnection Conn { get; }

        #endregion

        #region Constructor

        protected GenericRepository(IConnectionFactory connectionFactory)
        {
            Conn = connectionFactory.GetConnection();

            // 開啟連線
            Conn.Open();
        }

        #endregion

        public abstract Task<int> InsertAsync(T entity);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<T> FindAsync(object id);

        public abstract Task<int> UpdateAsync(T entityToUpdate);

        public abstract Task<int> DeleteAsync(object id);
    }
}
