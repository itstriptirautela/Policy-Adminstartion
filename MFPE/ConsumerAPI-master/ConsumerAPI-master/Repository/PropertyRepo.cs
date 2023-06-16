using ConsumerMicroservice.Models;
using ConsumerMicroservice.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Repository
{
    public class PropertyRepo:IPropertyRepo
    {
        private readonly ConsumerContext _dbContext;
        public PropertyRepo(ConsumerContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public IEnumerable<Property> GetProperty()
        {
            
            return _dbContext.Property.Include(c => c.PropertyMaster).ToList();
        }

        
        public Property GetProperties(int propertyId)
        {
            return _dbContext.Property.Include(c => c.PropertyMaster).FirstOrDefault(x => x.PropertyId == propertyId);
        }

        
        public bool CreateProperty(Property property)
        {
            _dbContext.Property.Add(property);
            return SavingDetails();
        }

        
        public bool UpdateProperty(int propertyId, Property property)
        {
            _dbContext.Property.Update(property);
            return SavingDetails();
        }

        /// <summary>
        /// return true if there is a insertion into database else false
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public bool PropertyExists(int propertyId)
        {
            return _dbContext.Property.Any(a => a.PropertyId == propertyId);
        }

        public bool SavingDetails()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}
