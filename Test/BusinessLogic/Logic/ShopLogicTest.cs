using System;
using System.Collections.Generic;
using BusinessLogic.Interfaces.Logic;
using BusinessLogic.Interfaces.Mapper;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Logic;
using Common.Constants;
using Common.Exceptions;
using Common.Interfaces.Logger;
using Common.Models;
using NSubstitute;
using NUnit.Framework;

namespace Test.BusinessLogic.Logic
{
    [TestFixture]
    public class ShopLogicTest
    {
        private IShopLogic _shopLogic;
        private ISupplierService _supplierService;
        private IArticleService _articleService;
        private IShopMapper _shopMapper;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _supplierService = Substitute.For<ISupplierService>();
            _articleService = Substitute.For<IArticleService>();
            _shopMapper = Substitute.For<IShopMapper>();
            _logger = Substitute.For<ILogger>();

            _shopLogic = new ShopLogic(_supplierService, _articleService, _shopMapper, _logger);
        }

        [Test]
        public void OrderArticleForBuyer_Successfully()
        {
            int articleId = 1;
            int articlePrice = 50;
            int maxExpectedPrice = 100;
            int buyerId = 10;
            SupplierArticle supplierArticle = new SupplierArticle()
            {
                Id = articleId,
                Name = "Article1 from supplier1",
                Price = articlePrice
            };
            ShopArticle shopArticle = new ShopArticle()
            {
                Id = articleId,
                Name = "Article1 from supplier1",
                Price = articlePrice
            };
            ShopArticle expectedShopArticle = new ShopArticle()
            {
                Id = articleId,
                Name = "Article1 from supplier1",
                Price = articlePrice,
                BuyerId = buyerId,
                IsSold = true,
                SoldDate = DateTime.Now
            };
            _supplierService.GetSuppliers().Returns(GetSuppliersData(supplierArticle));
            _shopMapper.MapToShopArticle(Arg.Is(supplierArticle)).Returns(shopArticle);

            ShopArticle receivedShopArticle = _shopLogic.OrderArticleForBuyer(articleId, maxExpectedPrice, buyerId);

            Assert.AreEqual(expectedShopArticle.Id, receivedShopArticle.Id);
            Assert.AreEqual(expectedShopArticle.Name, receivedShopArticle.Name);
            Assert.AreEqual(expectedShopArticle.Price, receivedShopArticle.Price);
            Assert.AreEqual(expectedShopArticle.BuyerId, receivedShopArticle.BuyerId);
            Assert.AreEqual(expectedShopArticle.IsSold, receivedShopArticle.IsSold);
            Assert.AreEqual(expectedShopArticle.SoldDate.ToShortDateString(), receivedShopArticle.SoldDate.ToShortDateString());
            _supplierService.Received(1).GetSuppliers();
            _shopMapper.Received(1).MapToShopArticle(Arg.Is(supplierArticle));
        }

        [Test]
        public void OrderArticleForBuyer_WhenCouldNotFindSupplierArticle_ThenValidationException()
        {
            int articleId = 1;
            int maxExpectedPrice = 100;
            int buyerId = 10;
            _supplierService.GetSuppliers().Returns(new List<Supplier>());

            ValidationException exception = Assert.Throws<ValidationException>(() => _shopLogic.OrderArticleForBuyer(articleId, maxExpectedPrice, buyerId));
            string expectedMessage = $"Validation message: {string.Format(ErrorConstants.NotFoundedSupplierArticleValidationMessage, articleId, maxExpectedPrice)}";
            Assert.AreEqual(expectedMessage, exception.Message);
        }
        
        [Test]
        public void SellShopArticle_Successfully()
        {
            ShopArticle shopArticle = new ShopArticle();
            
            _shopLogic.SellShopArticle(shopArticle);
            
            _articleService.Received(1).Save(Arg.Is(shopArticle));
        }
        
        [Test]
        public void SellShopArticle_WhenProvidedNullShopArticle_ThenValidationException()
        {
            ValidationException exception = Assert.Throws<ValidationException>(() => _shopLogic.SellShopArticle(null));
            string expectedMessage = $"Validation message: {ErrorConstants.CannotSellNullShopArticleValidationMessage}";
            Assert.AreEqual(expectedMessage, exception.Message);
        }
        
        [Test]
        public void GetShopArticleById_Successfully()
        {
            int articleId = 1;
            ShopArticle shopArticle = new ShopArticle
            {
                Id = articleId,
                Name = "Article1",
                Price = 100
            };
            _articleService.GetById(Arg.Is(articleId)).Returns(shopArticle);
            
            ShopArticle receivedShopArticle = _shopLogic.GetShopArticleById(articleId);
            
            Assert.AreEqual(shopArticle, receivedShopArticle);
            _articleService.Received(1).GetById(Arg.Is(articleId));
        }
        
        [Test]
        public void GetShopArticleById_WhenShopArticleNotExists_ThenValidationException()
        {
            int articleId = 1;
            _articleService.GetById(Arg.Is(articleId)).Returns(x => null);
            
            ValidationException exception = Assert.Throws<ValidationException>(() => _shopLogic.GetShopArticleById(articleId));
            string expectedMessage = $"Validation message: {string.Format(ErrorConstants.ShopArticleNotExistsValidationMessage, articleId)}";
            Assert.AreEqual(expectedMessage, exception.Message);
            _articleService.Received(1).GetById(Arg.Is(articleId));
        }

        private IList<Supplier> GetSuppliersData(SupplierArticle expectedSupplierArticle)
        {
            return new List<Supplier>
            {
                new Supplier()
                {
                    Id = 1, 
                    Name = "Supplier1", 
                    SupplierArticles = new List<SupplierArticle>
                    {
                    	expectedSupplierArticle,
                        new SupplierArticle()
                        {
                            Id = 2,
                            Name = "Article1 from supplier1",
                            Price = 150
                        }
                    }
                }
            };
        }
    }
}