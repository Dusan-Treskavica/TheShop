using Common.Models;

namespace BusinessLogic.Interfaces.Mapper
{
    public interface IShopMapper
    {
        /// <summary>
        /// Maps data from SupplierArticle to ShopArticle.
        /// </summary>
        /// <param name="supplierArticle">The supplier article to be mapped.</param>
        /// <returns>ShopArticle</returns>
        ShopArticle MapToShopArticle(SupplierArticle supplierArticle);
    }
}