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
        private readonly IDatabaseDriver _databaseDriver;
        private readonly ILogger _logger;

        public ArticleService()
        {
            _databaseDriver = new DatabaseDriver();
            _logger = new Logger();
        }

        public ShopArticle GetById(int articleId)
        {
            return _databaseDriver.GetById(articleId);
        }

        public void Save(ShopArticle shopArticle)
        {
            _databaseDriver.Save(shopArticle);
        }
    }
}