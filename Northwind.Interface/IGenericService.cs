using Northwind.Entity.Base;
using Northwind.Entity.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Interface
{
    public interface IGenericService<T,TDto> where T:IEntityBase where TDto:IDtoBase
    {
        //listeleme
        IResponse<List<TDto>> GetAll();
        //filtreli listeleme
        IResponse<List<TDto>> GetAll(Expression<Func<T,bool>> expression);//dinamik sorgu sağlar.

        //getirme
        IResponse<TDto> Find(int id);

        //ekleme,kaydetme
        IResponse<TDto> Add(TDto item,bool saveChanges=true);

        //async kaydetme
        Task<IResponse<TDto>> AddAsync(TDto item, bool saveChanges = true);

        //güncelle
        IResponse<TDto> Update(TDto item, bool saveChanges = true);

        //async güncelle
        Task<IResponse<TDto>> UpdateAsync(TDto item, bool saveChanges = true);

        //silme
        IResponse<bool> DeleteById(int id,bool saveChanges=true);
        //async Silme
        Task<IResponse<bool>> DeleteByIdAsync(int id, bool saveChanges = true);

        //IQeryable listeleme
        IResponse<IQueryable<TDto>> GetQueryable();//IQueryable,veritabanında sorgular ve projeye  getirir,Performans sağlar.
        void Save();
    }
}
