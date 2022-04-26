using System;
using TheShop.Common;
using TheShop.Database;
using TheShop.Interfaces.Common;
using TheShop.Interfaces.Database;
using TheShop.Interfaces.Services;
using TheShop.Model;

namespace TheShop.Services
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

        public Article GetById(int id)
        {
            return _databaseDriver.GetById(id);
        }

        public void Save(Article article)
        {
            _databaseDriver.Save(article);
        }

        public void OrderArticleForBuyer(Article article, int buyerId)
        {
            if (article == null)
            {
                _logger.Error("Could not order article");
                throw new Exception("Could not order article");
            }
            _logger.Debug("Trying to sell article with id=" + article.Id);
            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            
            article.BuyerId = buyerId;
        }

        public void SellArticle(Article article)
        {
            try
            {
                Save(article);
                _logger.Info("Article with id=" + article.Id + " is sold.");
            }
            catch (ArgumentNullException ex)
            {
                _logger.Error("Could not save article with id=" + article.Id);
                throw new Exception("Could not save article with id");
            }
            catch (Exception)
            {
            }
        }
    }
}