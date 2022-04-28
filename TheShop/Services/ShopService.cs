using BusinessLogic.Interfaces.Logic;
using BusinessLogic.Interfaces.Mapper;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Logic;
using BusinessLogic.Mapper;
using BusinessLogic.Services;
using Common.Interfaces.Logger;
using Common.Logger;
using Common.Models;
using TheShop.Interfaces.Services;

namespace TheShop.Services
{
    public class ShopService : IShopService
	{
		private readonly IShopLogic _shopLogic;
		private readonly IShopMapper _shopMapper;
		private readonly IArticleService _articleService;
		private readonly ILogger _logger;
		
		public ShopService()
		{
			_shopLogic = new ShopLogic();
			_shopMapper = new ShopMapper();
			_articleService = new ArticleService();
			_logger = new Logger();
		}

		public void OrderAndSellArticle(int articleId, int maxExpectedPrice, int buyerId)
		{
			SupplierArticle supplierArticle = _shopLogic.FindArticleByExpectedPrice(articleId, maxExpectedPrice);
			ShopArticle shopArticle = _shopMapper.MapToShopArticle(supplierArticle);
			
			_shopLogic.OrderArticleForBuyer(shopArticle, buyerId);
			_shopLogic.SellArticle(shopArticle);

		}

		public ShopArticle GetById(int articleId)
		{
			return _articleService.GetById(articleId);
		}
	}

}
