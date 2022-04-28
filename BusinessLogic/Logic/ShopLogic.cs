using System;
using System.Linq;
using BusinessLogic.Interfaces.Logic;
using BusinessLogic.Interfaces.Mapper;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Mapper;
using BusinessLogic.Services;
using Common.Exceptions;
using Common.Interfaces.Logger;
using Common.Logger;
using Common.Models;

namespace BusinessLogic.Logic
{
    public class ShopLogic : IShopLogic
    {
        private readonly ISupplierService _supplierService;
        private readonly IArticleService _articleService;
        private readonly IShopMapper _shopMapper;
        private readonly ILogger _logger;

        public ShopLogic()
        {
            _supplierService = new SupplierService();
            _articleService = new ArticleService();
            _shopMapper = new ShopMapper();
            _logger = new Logger();
        }

        public ShopArticle OrderArticleForBuyer(int articleId, int maxExpectedPrice, int buyerId)
        {
            SupplierArticle supplierArticle = FindSupplierArticleByExpectedPrice(articleId, maxExpectedPrice);
            ValidateSupplierArticle(articleId, maxExpectedPrice, supplierArticle);
            
            ShopArticle shopArticle = _shopMapper.MapToShopArticle(supplierArticle);
            OrderArticleForBuyer(buyerId, shopArticle);

            return shopArticle;
        }

        public void SellShopArticle(ShopArticle shopArticle)
        {
            ValidateShopArticleOnSell(shopArticle);
            _articleService.Save(shopArticle);
            _logger.Info("Article with id=" + shopArticle.Id + " is sold.");
        }

        public ShopArticle GetShopArticleById(int articleId)
        {
            _logger.Info($"Getting ShopArticle with Id={articleId}");
            return _articleService.GetById(articleId);
        }

        private SupplierArticle FindSupplierArticleByExpectedPrice(int articleId, int expectedPrice)
        {
            foreach (Supplier supplier in _supplierService.GetSuppliers())
            {
                SupplierArticle supplierArticle = supplier.SupplierArticles.FirstOrDefault(x => x.Id == articleId);
                if (supplierArticle != null && supplierArticle.Price <= expectedPrice)
                {
                    return supplierArticle;
                }
            }

            return null;
        }
        
        private void OrderArticleForBuyer(int buyerId, ShopArticle shopArticle)
        {
            _logger.Info("Ordering article with id=" + shopArticle.Id);
            shopArticle.IsSold = true;
            shopArticle.SoldDate = DateTime.Now;
            shopArticle.BuyerId = buyerId;
        }
        
        private static void ValidateSupplierArticle(int articleId, int maxExpectedPrice, SupplierArticle supplierArticle)
        {
            if (supplierArticle == null)
            {
                throw new ValidationException(
                    $"Article with Id={articleId} doesn't exist or there is no article with price<={maxExpectedPrice}.");
            }
        }
        
        private static void ValidateShopArticleOnSell(ShopArticle shopArticle)
        {
            if (shopArticle == null)
            {
                throw new ValidationException("ShopArticle that is null can't be sold.");
            }
        }
    }
}