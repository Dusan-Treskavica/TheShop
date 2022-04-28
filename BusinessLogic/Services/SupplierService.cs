using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Interfaces.Services;
using Common.Models;
using DataAccess.Database;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IDatabaseDriver _databaseDriver;

        public SupplierService()
        {
            _databaseDriver = new DatabaseDriver();
        }

        public IList<Supplier> GetSuppliers()
        {
        	return new List<Supplier>
        	{
        		new Supplier()
        		{
        			Id = 1, 
        			Name = "Supplier1", 
        			SupplierArticles = new List<SupplierArticle>
        			{
        				new SupplierArticle()
        				{
        					Id = 1,
        					Name = "Article1 from supplier1",
        					Price = 458
        				}
        			}
        		},
        		new Supplier()
        		{
        			Id = 2, 
        			Name = "Supplier2", 
        			SupplierArticles = new List<SupplierArticle>
        			{
        				new SupplierArticle()
        				{
        					Id = 1,
        					Name = "Article1 from supplier2",
        					Price = 459
        				},
        				new SupplierArticle()
        				{
        					Id = 2,
        					Name = "Article2 from supplier2",
        					Price = 555
        				}
        			}
        		},
        		new Supplier()
        		{
        			Id = 3, 
        			Name = "Supplier3", 
        			SupplierArticles = new List<SupplierArticle>
        			{
        				new SupplierArticle()
        				{
        					Id = 1,
        					Name = "Article1 from supplier3",
        					Price = 460
        				}
        			}
        		}
        	};
        }
        	

        public SupplierArticle FindArticleByExpectedPrice(int id, int expectedPrice)
        {
            foreach (Supplier supplier in GetSuppliers())
            {
                SupplierArticle supplierArticle = supplier.SupplierArticles.FirstOrDefault(x => x.Id == id);
                if (supplierArticle != null && supplierArticle.Price <= expectedPrice)
                {
                    return supplierArticle;
                }
            }

            return null;
        }
    }
}
