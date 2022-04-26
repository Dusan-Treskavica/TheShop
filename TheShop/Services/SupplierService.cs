using System;
using System.Collections.Generic;
using System.Linq;
using TheShop.Database;
using TheShop.Interfaces.Database;
using TheShop.Interfaces.Services;
using TheShop.Model;

namespace TheShop.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IDatabaseDriver _databaseDriver;

        public SupplierService()
        {
            this._databaseDriver = new DatabaseDriver();
        }

        public bool HasArticle(int articleId)
        {
            throw new NotImplementedException();
        }

        public Article GetById(int articleId)
        {
            throw new NotImplementedException();
        }

        public Article FindArticleByExpectedPrice(int id, int expectedPrice)
        {
            foreach (Supplier supplier in this._databaseDriver.GetSuppliers())
            {
                Article supplierArticle = supplier.Articles.FirstOrDefault(x => x.Id == id);
                if (supplierArticle != null && supplierArticle.Price <= expectedPrice)
                {
                    return supplierArticle;
                }
            }

            return null;
        }
    }
}
