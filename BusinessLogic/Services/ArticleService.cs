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

        public ArticleService()
        {
            _logger = new Logger();
        }

        public ShopArticle GetById(int articleId)
        {
            ShopArticle shopArticle = DatabaseDriver.Instance.GetById(articleId);
            ValidateIfShopArticleExists(articleId, shopArticle);

            return shopArticle;
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
        
        private void ValidateIfShopArticleExists(int articleId, ShopArticle shopArticle)
        {
            if (shopArticle == null)
            {
                throw new ValidationException($"Article with Id = {articleId} doesn't exist.");
            }
        }
    }
}