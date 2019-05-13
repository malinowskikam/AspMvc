using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspMvc.Models;

namespace AspTests.FakeRepositories
{
    class DictionarySetRepository : IRepository
    {
        Dictionary<long, Tool> tools;
        Dictionary<long, Manufacturer> manufacturers;

        long nextToolId;
        long nextManId;

        public DictionarySetRepository()
        {
            tools = new Dictionary<long, Tool>();
            manufacturers = new Dictionary<long, Manufacturer>();

            nextToolId = 0;
            nextManId = 0;
        }

        public IQueryable<Tool> Tools
        {
            get { return tools.Select(entry => entry.Value).AsQueryable<Tool>(); }
        }

        public IQueryable<Manufacturer> Manufacturers
        {
            get { return manufacturers.Select(entry => entry.Value).AsQueryable<Manufacturer>(); }
        }

        public T Add<T>(T model) where T : class
        {
            if (model is Tool)
            {
                Tool tool = model as Tool;
                tool.Id = nextToolId;
                nextToolId++;
                tools.Add(tool.Id, tool);
                return model;
            }
            else if (model is Manufacturer)
            {
                Manufacturer manufacturer = model as Manufacturer;
                manufacturer.Id = nextManId;
                nextManId++;
                manufacturers.Add(manufacturer.Id, manufacturer);
                return model;
            }
            else
                throw new ArgumentException("Database contains only tools and manufacturers");
        }

        public T Delete<T>(T model) where T : class
        {
            if (model is Tool)
            {
                tools.Remove((model as Tool).Id);
                return model;
            }
            else if (model is Manufacturer)
            {
                manufacturers.Remove((model as Manufacturer).Id);
                return model;
            }
            else
                throw new ArgumentException("Database contains only tools and manufacturers");
        }

        public Manufacturer FindManufacturerById(long id)
        {
            if (manufacturers.ContainsKey(id))
                return manufacturers[id];
            else
                return null;
        }

        public Tool FindToolById(long id)
        {
            if (tools.ContainsKey(id))
                return tools[id];
            else
                return null;
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
