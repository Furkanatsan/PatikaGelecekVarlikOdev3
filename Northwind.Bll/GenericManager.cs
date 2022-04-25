using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Dal.Abstract;
using Northwind.Dal.Abstract.IUnitOfWork;
using Northwind.Entity.Base;
using Northwind.Entity.IBase;
using Northwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Northwind.Bll
{
    public class GenericManager<T, TDto> : IGenericService<T, TDto> where T : EntityBase where TDto : DtoBase
    {
        //unit of work
        //IServiceProvider
        //GenericRepository
        //Constructor
        #region Variables
        private readonly IUnitOfWork unitOfWork;
        private readonly IServiceProvider service;
        private readonly IGenericRepository<T> repository;
        #endregion

        #region Constructor

        public GenericManager(IServiceProvider service)
        {
            this.service = service;
            unitOfWork = service.GetService<IUnitOfWork>();

            repository = unitOfWork.GetRepository<T>();
        }
        #endregion


        #region Methods
        public IResponse<TDto> Add(TDto item, bool saveChanges = true)
        {
            try
            {
                var model = ObjectMapper.Mapper.Map<T>(item);//DTO T YE DÖNÜŞTÜRÜLDÜ
                //var resolvesResult = "";
                //var resolvesResult = String.Join(',',model.GetType().GetProperties().Select(x=> $" - {x.Name} : {x.GetValue(model) ?? ""} - "));
                var result = repository.Add(model);
                if (saveChanges)
                {
                   Save();//kaydetme işlemi olduğunda  transactionu commitliyoruz.
                }
                return new Response<TDto> {//dönüş tipini ayarlıyoruz
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = ObjectMapper.Mapper.Map<T, TDto>(result)
                };
            }
            catch (Exception ex)
            {
                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };

            }
        }

        public async Task<IResponse<TDto>> AddAsync(TDto item, bool saveChanges = true)
        {
            try
            {
                var model = ObjectMapper.Mapper.Map<T>(item);//DTO T YE DÖNÜŞTÜRÜLDÜ
                //var resolvesResult = "";
                //var resolvesResult = String.Join(',',model.GetType().GetProperties().Select(x=> $" - {x.Name} : {x.GetValue(model) ?? ""} - "));
                var result = await repository.AddAsync(model);
                if (saveChanges)
                {
                    Save();//kaydetme işlemi olduğunda  transactionu commitliyoruz.
                }
                return new Response<TDto>
                {//dönüş tipini ayarlıyoruz
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = ObjectMapper.Mapper.Map<T, TDto>(result)
                };
            }
            catch (Exception ex)
            {
                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };

            }
        }

        public IResponse<bool> DeleteById(int id, bool saveChanges = true)
        {
            repository.Delete(id);
            try
            {
                if (saveChanges)
                {
                    Save();
                }
                return new Response<bool>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "success",
                    Data = true
                };

            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"error:{ex.Message}",
                    Data =false
                };

            }
            

        }

        public Task<IResponse<bool>> DeleteByIdAsync(int id, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public IResponse<TDto> Find(int id)
        {
            try
            {
                var entity =ObjectMapper.Mapper.Map<T,TDto>(repository.Find(id));
                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "success",
                    Data = entity
                };
            }
            catch (Exception ex)
            {

                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<List<TDto>> GetAll()
        {
            try
            {
                var list = repository.GetAll();
                var listDto = list.Select(x => ObjectMapper.Mapper.Map<TDto>(x)).ToList();
                return new Response<List<TDto>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "success",
                    Data = listDto
                };
            }
            catch (Exception ex)
            {

                return new Response<List<TDto>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"error:{ex.Message}",
                    Data = null
                };
            }

        }

        public IResponse<List<TDto>> GetAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                var list = repository.GetAll(expression);
                var listDto = list.Select(x => ObjectMapper.Mapper.Map<TDto>(x)).ToList();
                return new Response<List<TDto>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "success",
                    Data = listDto
                };
            }
            catch (Exception ex)
            {

                return new Response<List<TDto>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"error:{ex.Message}",
                    Data = null
                };
            }

        }

        public IResponse<IQueryable<TDto>> GetQueryable()
        {
            try
            {
                var list = repository.GetQueryable();

                var listDto = list.Select(x => ObjectMapper.Mapper.Map<TDto>(x));
                return new Response<IQueryable<TDto>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "success",
                    Data = listDto
                };
            }
            catch (Exception ex)
            {

                return new Response<IQueryable<TDto>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"error:{ex.Message}",
                    Data = null
                };
            }
        }

        public void Save()
        {
            unitOfWork.SaveChanges();
        }

        public IResponse<TDto> Update(TDto item, bool saveChanges = true)
        {
            try
            {
                var model = ObjectMapper.Mapper.Map<T>(item);//DTO T YE DÖNÜŞTÜRÜLDÜ
                //var resolvesResult = "";
                //var resolvesResult = String.Join(',',model.GetType().GetProperties().Select(x=> $" - {x.Name} : {x.GetValue(model) ?? ""} - "));
                var result = repository.Update(model);
                if (saveChanges)
                {
                    Save();//kaydetme işlemi olduğunda  transactionu commitliyoruz.
                }
                return new Response<TDto>
                {//dönüş tipini ayarlıyoruz
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = ObjectMapper.Mapper.Map<T, TDto>(result)
                };
            }
            catch (Exception ex)
            {
                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };

            }
        }

        public Task<IResponse<TDto>> UpdateAsync(TDto item, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}
