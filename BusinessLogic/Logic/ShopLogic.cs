using System;
using System.Linq;
using BusinessLogic.Interfaces.Logic;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Services;
using Common.Interfaces.Logger;
using Common.Logger;
using Common.Models;

namespace BusinessLogic.Logic
{
    public class ShopLogic : IShopLogic
    {
        private readonly ISupplierService _supplierService;
        private readonly IArticleService _articleService;
        private readonly ILogger _logger;

        public ShopLogic()
        {
            _supplierService = new SupplierService();
            _articleService = new ArticleService();
            _logger = new Logger();
        }

        public SupplierArticle FindArticleByExpectedPrice(int id, int expectedPrice)
        {
            foreach (Supplier supplier in _supplierService.GetSuppliers())
            {
                SupplierArticle supplierArticle = supplier.SupplierArticles.FirstOrDefault(x => x.Id == id);
                if (supplierArticle != null && supplierArticle.Price <= expectedPrice)
                {
                    return supplierArticle;
                }
            }

            return null;
        }

        public void OrderArticleForBuyer(ShopArticle shopArticle, int buyerId)
        {
            if (shopArticle == null)
            {
                _logger.Error("Could not order article");
                throw new Exception("Could not order article");
            }
            _logger.Debug("Trying to sell article with id=" + shopArticle.Id);
            shopArticle.IsSold = true;
            shopArticle.SoldDate = DateTime.Now;
            shopArticle.BuyerId = buyerId;
        }
        
        public void SellArticle(ShopArticle shopArticle)
        {
            try
            {
                _articleService.Save(shopArticle);
                _logger.Info("Article with id=" + shopArticle.Id + " is sold.");
            }
            catch (ArgumentNullException ex)
            {
                _logger.Error("Could not save article with id=" + shopArticle.Id);
                throw new Exception("Could not save article with id");
            }
            catch (Exception)
            {
            }
        }
    }
}