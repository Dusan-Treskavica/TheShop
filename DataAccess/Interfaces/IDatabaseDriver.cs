using Common.Models;

namespace DataAccess.Interfaces
{
    public interface IDatabaseDriver
    {
		ShopArticle GetById(int id);
		void Save(ShopArticle shopArticle);
    }
}
