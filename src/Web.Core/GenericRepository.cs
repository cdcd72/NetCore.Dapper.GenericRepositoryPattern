using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Web.Core
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Properties

        protected IDbConnection Conn { get; }

        #endregion

        #region Constructor

        protected GenericRepository(IDbConnection connection)
        {
            Conn = connection;
        }

        #endregion

        public abstract Task<int> InsertAsync(T entity);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<T> FindAsync(object id);

        public abstract Task<int> UpdateAsync(T entityToUpdate);

        public abstract Task<int> DeleteAsync(object id);
    }
}
