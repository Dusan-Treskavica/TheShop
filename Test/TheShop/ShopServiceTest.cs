using System;
using BusinessLogic.Interfaces.Logic;
using Common.Constants;
using Common.Exceptions;
using Common.Interfaces.Logger;
using Common.Models;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using TheShop.Interfaces.Services;
using TheShop.Services;

namespace Test.TheShop
{
    [TestFixture]
    public class ShopServiceTest
    {
        private IShopService _shopService;
        private IShopLogic _shopLogic;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _shopLogic = Substitute.For<IShopLogic>();
            _logger = Substitute.For<ILogger>();

            _shopService = new ShopService(_shopLogic, _logger);
        }

        [Test]
        public void OrderAndSellArticle_Successfully()
        {
            int articleId = 1;
            int maxExpectedPrice = 100;
            int buyerId = 10;
            ShopArticle shopArticle = new ShopArticle
            {
                Id = articleId,
                BuyerId = 10,
                IsSold = true
            };
            _shopLogic.OrderArticleForBuyer(Arg.Is(articleId), Arg.Is(maxExpectedPrice), Arg.Is(buyerId)).Returns(shopArticle);

            _shopService.OrderAndSellArticle(articleId, maxExpectedPrice, buyerId);

            _shopLogic.Received(1).OrderArticleForBuyer(Arg.Is(articleId), Arg.Is(maxExpectedPrice), Arg.Is(buyerId));
            _shopLogic.Received(1).SellShopArticle(shopArticle);
            _logger.Received(0).Error(Arg.Any<string>());
        }
        
        [Test]
        public void OrderAndSellArticle_WhenValidationErrorOccursOnOrderingArticle_HandleException()
        {
            int articleId = 1;
            int maxExpectedPrice = 100;
            int buyerId = 10;
            ValidationException exception = new ValidationException("Any message.");
            _shopLogic.OrderArticleForBuyer(Arg.Is(articleId), Arg.Is(maxExpectedPrice), Arg.Is(buyerId)).Throws(exception);
            
            _shopService.OrderAndSellArticle(articleId, maxExpectedPrice, buyerId);
            
            _shopLogic.Received(1).OrderArticleForBuyer(Arg.Is(articleId), Arg.Is(maxExpectedPrice), Arg.Is(buyerId));
            _shopLogic.Received(0).SellShopArticle(Arg.Any<ShopArticle>());
            _logger.Received(1).Error(Arg.Is(exception.Message));
        }
        
        [Test]
        public void OrderAndSellArticle_WhenValidationErrorOccursOnSellingArticle_HandleException()
        {
            int articleId = 1;
            int maxExpectedPrice = 100;
            int buyerId = 10;
            ValidationException exception = new ValidationException("Any message.");
            ShopArticle shopArticle = new ShopArticle
            {
                Id = articleId,
                BuyerId = 10,
                IsSold = true
            };
            _shopLogic.OrderArticleForBuyer(Arg.Is(articleId), Arg.Is(maxExpectedPrice), Arg.Is(buyerId)).Returns(shopArticle);
            _shopLogic.When(x => x.SellShopArticle(Arg.Is(shopArticle))).Throw(exception);

            _shopService.OrderAndSellArticle(articleId, maxExpectedPrice, buyerId);
            
            _shopLogic.Received(1).OrderArticleForBuyer(Arg.Is(articleId), Arg.Is(maxExpectedPrice), Arg.Is(buyerId));
            _shopLogic.Received(1).SellShopArticle(Arg.Is(shopArticle));
            _logger.Received(1).Error(Arg.Is(exception.Message));
        }
        
        [Test]
        public void OrderAndSellArticle_WhenDatabaseErrorOccursOnSellingArticle_HandleException()
        {
            int articleId = 1;
            int maxExpectedPrice = 100;
            int buyerId = 10;
            DatabaseException exception = new DatabaseException("Any message.");
            ShopArticle shopArticle = new ShopArticle
            {
                Id = articleId,
                BuyerId = 10,
                IsSold = true
            };
            _shopLogic.OrderArticleForBuyer(Arg.Is(articleId), Arg.Is(maxExpectedPrice), Arg.Is(buyerId)).Returns(shopArticle);
            _shopLogic.When(x => x.SellShopArticle(Arg.Is(shopArticle))).Throw(exception);

            _shopService.OrderAndSellArticle(articleId, maxExpectedPrice, buyerId);
            
            _shopLogic.Received(1).OrderArticleForBuyer(Arg.Is(articleId), Arg.Is(maxExpectedPrice), Arg.Is(buyerId));
            _shopLogic.Received(1).SellShopArticle(Arg.Is(shopArticle));
            _logger.Received(1).Error(Arg.Is(exception.Message));
        }
        
        [Test]
        public void OrderAndSellArticle_WhenUnhandledErrorOccurs_HandleFatalError()
        {
            int articleId = 1;
            int maxExpectedPrice = 100;
            int buyerId = 10;
            Exception exception = new Exception("Any message.");
            ShopArticle shopArticle = new ShopArticle();
            _shopLogic.OrderArticleForBuyer(Arg.Is(articleId), Arg.Is(maxExpectedPrice), Arg.Is(buyerId)).Throws(exception);
            
            _shopService.OrderAndSellArticle(articleId, maxExpectedPrice, buyerId);
            
            _logger.Received(1).Error(Arg.Is(ErrorConstants.FatalError));
        }

        [Test]
        public void DisplayShopArticle_Successfully()
        {
            int articleId = 1;
            ShopArticle expectedShopArticle = new ShopArticle
            {
                Id = articleId,
                Name = "Article 1"
            };
            _shopLogic.GetShopArticleById(Arg.Is(articleId)).Returns(expectedShopArticle);

            ShopArticle shopArticle = _shopService.DisplayShopArticle(articleId);
            
            Assert.AreEqual(expectedShopArticle, shopArticle);
            _shopLogic.Received(1).GetShopArticleById(Arg.Is(articleId));
        }
        
        [Test]
        public void DisplayShopArticle_WhenValidationErrorOccursOnGettingArticle_HandleException()
        {
            int articleId = 1;
            ValidationException exception = new ValidationException("Any message.");
            _shopLogic.GetShopArticleById(Arg.Is(articleId)).Throws(exception);

            ShopArticle shopArticle = _shopService.DisplayShopArticle(articleId);
            
            _shopLogic.Received(1).GetShopArticleById(Arg.Is(articleId));
            _logger.Received(1).Error(Arg.Is(exception.Message));
        }
        
        [Test]
        public void DisplayShopArticle_WhenUnhandledErrorOccurs_HandleFatalError()
        {
            int articleId = 1;
            Exception exception = new Exception("Any message.");
            _shopLogic.GetShopArticleById(Arg.Is(articleId)).Throws(exception);

            ShopArticle shopArticle = _shopService.DisplayShopArticle(articleId);
            
            _shopLogic.Received(1).GetShopArticleById(Arg.Is(articleId));
            _logger.Received(1).Error(Arg.Is(ErrorConstants.FatalError));
        }
    }
}