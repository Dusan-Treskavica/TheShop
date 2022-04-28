using BusinessLogic.Interfaces.Logic;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Logic;
using BusinessLogic.Services;
using Common.Exceptions;
using Common.Interfaces.Logger;
using Common.Logger;
using Common.Models;
using TheShop.Interfaces.Services;

namespace TheShop.Services
{
    public class ShopService : IShopService
	{
		private readonly IShopLogic _shopLogic;
		private readonly IArticleService _articleService;
		private readonly ILogger _logger;
		
		public ShopService()
		{
			_shopLogic = new ShopLogic();
			_articleService = new ArticleService();
			_logger = new Logger();
		}

		public void OrderAndSellArticle(int articleId, int maxExpectedPrice, int buyerId)
		{
			try
			{
				ShopArticle shopArticle = _shopLogic.OrderArticleForBuyer(articleId, maxExpectedPrice, buyerId);
				_shopLogic.SellShopArticle(shopArticle);
			}
			catch (ValidationException ex)
			{
				_logger.Error(ex.Message);
			}
		}

		public ShopArticle DisplayShopArticle(int articleId)
		{
			try
			{
				return _shopLogic.GetShopArticleById(articleId);
			}
			catch (ValidationException ex)
			{
				_logger.Error(ex.Message);
				throw;
			}
		}
	}

}
