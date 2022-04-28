using System;
using System.Linq;
using BusinessLogic.Interfaces.Logic;
using BusinessLogic.Interfaces.Mapper;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Mapper;
using BusinessLogic.Services;
using Common.Constants;
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

        public ShopLogic() : this(new SupplierService(), new ArticleService(), new ShopMapper(), new Logger())
        { }

        public ShopLogic(ISupplierService supplierService, IArticleService articleService, IShopMapper shopMapper, ILogger logger)
        {
            _supplierService = supplierService;
            _articleService = articleService;
            _shopMapper = shopMapper;
            _logger = logger;
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
            _logger.Info(string.Format(InfoConstants.SoldShopArticleInfo, shopArticle.Id));
        }

        public ShopArticle GetShopArticleById(int articleId)
        {
            _logger.Info(string.Format(InfoConstants.GettingShopArticleInfo, articleId));
            ShopArticle shopArticle = _articleService.GetById(articleId);
            ValidateIfShopArticleExists(articleId, shopArticle);
            
            return shopArticle;
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
            _logger.Info(string.Format(InfoConstants.OrderingShopArticleInfo, shopArticle.Id));
            shopArticle.IsSold = true;
            shopArticle.SoldDate = DateTime.Now;
            shopArticle.BuyerId = buyerId;
        }
        
        private static void ValidateSupplierArticle(int articleId, int maxExpectedPrice, SupplierArticle supplierArticle)
        {
            if (supplierArticle == null)
            {
                throw new ValidationException(string.Format(ErrorConstants.NotFoundedSupplierArticleValidationMessage, articleId, maxExpectedPrice));
            }
        }
        
        private static void ValidateShopArticleOnSell(ShopArticle shopArticle)
        {
            if (shopArticle == null)
            {
                throw new ValidationException(ErrorConstants.CannotSellNullShopArticleValidationMessage);
            }
        }
        
        private void ValidateIfShopArticleExists(int articleId, ShopArticle shopArticle)
        {
            if (shopArticle == null)
            {
                throw new ValidationException(string.Format(ErrorConstants.ShopArticleNotExistsValidationMessage, articleId));
            }
        }
    }
}