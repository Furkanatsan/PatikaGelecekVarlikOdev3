using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Northwind.Dal.Abstract;
using Northwind.Entity.Base;
using Northwind.Entity.Dto;
using Northwind.Entity.IBase;
using Northwind.Entity.Models;
using Northwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Bll
{
    public class UserManager:GenericManager<User,DtoUser>,IUserService
    {
        public readonly IUserRepository userRepository;
        private IConfiguration configuration;
        public UserManager(IServiceProvider service):base(service)
        {
            userRepository = service.GetService<IUserRepository>();
        }

        public IResponse<DtoUserToken> Login(DtoLogin login)
        {
            var user = userRepository.Login(ObjectMapper.Mapper.Map<User>(login));
            if (user!=null)
            {
                var dtoUser = ObjectMapper.Mapper.Map<DtoLoginUser>();
                var token = new TokenManager();
                var userToken= new DtoUserToken() { 
                
                    DtoLoginUser=dtoUser,
                    AccessToken=token
                };
                return new Response<DtoUserToken> { 
                Message="TokenUretildi",
                StatusCode=StatusCodes.Status200OK,
                Data=userToken
                };
            }
            else
            {
                return new Response<DtoUserToken>
                {
                    Message="kullanıcı kodu yada şifre yanlış",
                    StatusCode=StatusCodes.Status406NotAcceptable,
                    Data=null
                };
            }

        }
    }
}
