using System;
using BusinessLogic.Interfaces.Logic;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Logic;
using BusinessLogic.Services;
using Common.Constants;
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
		private readonly ILogger _logger;

		public ShopService() : this(new ShopLogic(), new Logger())
		{ }
		
		public ShopService(IShopLogic shopLogic, ILogger logger)
		{
			_shopLogic = shopLogic;
			_logger = logger;
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
			catch (DatabaseException ex)
			{
				_logger.Error(ex.Message);
			}
			catch (Exception)
			{
				_logger.Error(ErrorConstants.FatalError);
			}
		}

		public ShopArticle DisplayShopArticle(int articleId)
		{
			ShopArticle shopArticle = null;
			try
			{
				shopArticle = _shopLogic.GetShopArticleById(articleId);
			}
			catch (ValidationException ex)
			{
				_logger.Error(ex.Message);
			}
			catch (Exception)
			{
				_logger.Error(ErrorConstants.FatalError);
			}

			return shopArticle;
		}
	}

}
