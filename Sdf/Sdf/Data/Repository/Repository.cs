using Newtonsoft.Json;
using Sdf.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdf.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly string _filePath;

        public Repository(string filePath)
        {
            _filePath= filePath;
        }

        public void Add(T item)
        {
            var items = GetAll() ?? new List<T>();
            items.Add(item);
            Update(items);
        }


        public List<T> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public void Update(List<T> item)
        {
            var json = JsonConvert.SerializeObject(item, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
