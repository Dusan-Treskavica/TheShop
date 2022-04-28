using Common.Models;

namespace BusinessLogic.Interfaces.Services
{
    public interface IArticleService
    {
        ShopArticle GetById(int articleId);
        void Save(ShopArticle shopArticle);
    }
}
