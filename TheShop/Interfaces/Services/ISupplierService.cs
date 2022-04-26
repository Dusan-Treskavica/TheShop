using TheShop.Model;

namespace TheShop.Interfaces.Services
{
    public interface ISupplierService
    {
        bool HasArticle(int id);
        Article GetById(int id);
    }
}
