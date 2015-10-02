using BamApps.Excido.Interface.Data;
using System.Data.Entity;

namespace BamApps.Excido.Data.Context {
    public class ContextTransaction : IContextTransaction {
        readonly DbContextTransaction _dbContextTransaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextTransaction"/> class.
        /// </summary>
        /// <param name="dbContextTransaction"></param>
        public ContextTransaction(DbContextTransaction dbContextTransaction) {
            _dbContextTransaction = dbContextTransaction;
        }
        public void Commit() {
            _dbContextTransaction.Commit();
        }

        public void Dispose() {
            _dbContextTransaction.Dispose();
        }

        public void Rollback() {
            _dbContextTransaction.Rollback();
        }
    }
}
