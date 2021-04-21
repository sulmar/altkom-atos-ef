using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRepositories
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        Product GetByBarCode(string barCode);
        IEnumerable<Product> GetByColor(string color);


    }
}
