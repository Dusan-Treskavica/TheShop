using BusinessLogic.Interfaces.Mapper;
using Common.Models;

namespace BusinessLogic.Mapper
{
    public class ShopMapper : IShopMapper
    {
        public ShopArticle MapToShopArticle(SupplierArticle supplierArticle)
        {
            return new ShopArticle
            {
                Id = supplierArticle.Id,
                Name = supplierArticle.Name,
                Price = supplierArticle.Price
            };
        }
    }
}