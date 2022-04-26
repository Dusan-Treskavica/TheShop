using TheShop.Model;

namespace TheShop.Interfaces.Services
{
    public interface IArticleService
    {
        Article GetById(int id);
        void Save(Article article);
        void OrderArticleForBuyer(Article article, int buyerId);
        void SellArticle(Article article);
    }
}
