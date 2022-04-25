using Northwind.Entity.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Dal.Abstract
{
    public interface IGenericRepository<T> where T:IEntityBase
    {
        //listeleme
        List<T> GetAll();

        //filtreli listeleme
        List<T> GetAll(Expression<Func<T, bool>> expression);//dinamik sorgu sağlar.

        //getirme
         T Find(int id);

        //ekleme
          T Add(T item);

        //async kaydetme
        Task<T> AddAsync(T item);

        //güncelle
        T Update(T item);

        //async güncelle
        //Task<T> UpdateAsync(T item);

        //silme
        bool Delete(int id);
        bool Delete(T item);
        //async Silme
        //Task<bool> DeleteAsync(int id);

        //IQerible listeleme
        IQueryable<T> GetQueryable();//IQueryable,veritabanında sorgular ve projeye  getirir,Performans sağlar.
    }
}
