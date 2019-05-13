using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvc.Models
{
    public interface IRepository
    {
        IQueryable<Tool> Tools { get; }
        IQueryable<Manufacturer> Manufacturers { get; }
        int SaveChanges();
        T Add<T>(T model) where T : class;
        Tool FindToolById(long id);
        Manufacturer FindManufacturerById(long id);
        T Delete<T>(T model) where T : class;

    }
}
