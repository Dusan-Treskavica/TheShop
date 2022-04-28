using Common.Models;

namespace BusinessLogic.Interfaces.Mapper
{
    public interface IShopMapper
    {
        ShopArticle MapToShopArticle(SupplierArticle supplierArticle);
    }
}