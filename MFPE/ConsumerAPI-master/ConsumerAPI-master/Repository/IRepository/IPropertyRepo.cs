using ConsumerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Repository.IRepository
{
    public interface IPropertyRepo
    {
        IEnumerable<Property> GetProperty();
        Property GetProperties(int propertyId);
        bool CreateProperty(Property property);
        bool UpdateProperty(int propertyId, Property property);
        //bool DeleteProperty(int PropertyId);
        bool PropertyExists(int propertyId);
        bool SavingDetails();
    }
}
