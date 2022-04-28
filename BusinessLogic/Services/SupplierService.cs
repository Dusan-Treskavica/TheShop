using System.Collections.Generic;
using BusinessLogic.Interfaces.Services;
using Common.Models;

namespace BusinessLogic.Services
{
    public class SupplierService : ISupplierService
    {
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
    }
}
