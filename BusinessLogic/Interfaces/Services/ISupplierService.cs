using System.Collections.Generic;
using Common.Models;

namespace BusinessLogic.Interfaces.Services
{
    public interface ISupplierService
    {
        IList<Supplier> GetSuppliers();
        SupplierArticle FindArticleByExpectedPrice(int id, int expectedPrice);
    }
}
