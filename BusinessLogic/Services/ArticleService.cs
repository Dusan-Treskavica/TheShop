using BusinessLogic.Interfaces.Services;
using Common.Interfaces.Logger;
using Common.Logger;
using Common.Models;
using DataAccess.Database;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ILogger _logger;

        public ArticleService()
        {
            _logger = new Logger();
        }

        public ShopArticle GetById(int articleId)
        {
            return DatabaseDriver.Instance.GetById(articleId);
        }

        public void Save(ShopArticle shopArticle)
        {
            DatabaseDriver.Instance.Save(shopArticle);
        }
    }
}