using Microsoft.EntityFrameworkCore;
using Northwind.Dal.Abstract;
using Northwind.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Dal.Concrete.EtityFramework.Repository
{
    public class CustomerRepository:GenericRepository<Customer>,ICustomerRepository
    {
        public CustomerRepository(DbContext context):base(context)//base kalıtım aldığım sınıfa argüman göndermek için kullanılır.
        {

        }

       

        public Customer Find(string id)
        {
            return dbset.Find(id);
        }

        

        public IQueryable<Customer> GetTotalReport()
        {
            return dbset.AsQueryable<Customer>();
        }
    }
}
