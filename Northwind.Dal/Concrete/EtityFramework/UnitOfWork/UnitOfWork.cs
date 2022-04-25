using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Northwind.Dal.Abstract;
using Northwind.Dal.Abstract.IUnitOfWork;
using Northwind.Dal.Concrete.EtityFramework.Repository;
using Northwind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Dal.Concrete.EtityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Variables
        DbContext context;
        IDbContextTransaction transaction;
        bool dispose;
        #endregion
        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }
        public bool BeginTransaction()
        {
            try
            {
                transaction = context.Database.BeginTransaction();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);//Garbage collector çalıştırır.
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!dispose)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            dispose = true;
        }

        public IGenericRepository<T> GetRepository<T>() where T : EntityBase
        {
            return new GenericRepository<T>(context);
        }

        public bool RollBackTransaction()
        {
            try
            {
                transaction.Rollback();
                transaction = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int SaveChanges()
        {
            var _transaction = transaction != null ? transaction : context.Database.BeginTransaction();
            using (_transaction)//transactionu öldürür siler iptal eder.
            {
                try
                {
                    if (context == null)
                    {
                        throw new ArgumentException("contextis null");
                    }
                    int result = context.SaveChanges();//etkilenen satır sayısını döndürür.
                    
                    _transaction.Commit();//onaylar commitler

                    return result;//etkilenen savechange sayısı nöner

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("error on save changes", ex );
                }
            }
        }
    }
}
