using System;
using BusinessLogic.Interfaces.Services;
using Common.Exceptions;
using Common.Interfaces.Logger;
using Common.Logger;
using Common.Models;
using DataAccess.Database;

namespace BusinessLogic.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ILogger _logger;

        public ArticleService() : this(new Logger())
        { }

        public ArticleService(ILogger logger)
        {
            _logger = logger;
        }

        public ShopArticle GetById(int articleId)
        {
            return DatabaseDriver.Instance.GetById(articleId);
        }

        public void Save(ShopArticle shopArticle)
        {
            try
            {
                DatabaseDriver.Instance.Save(shopArticle);
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Not able to store shopArticle. Database error: {ex.Message}", ex);
            }
        }
    }
}