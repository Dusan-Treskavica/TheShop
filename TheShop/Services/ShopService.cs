using System;
using TheShop.Common;
using TheShop.Database;
using TheShop.Interfaces.Common;
using TheShop.Interfaces.Database;
using TheShop.Interfaces.Services;
using TheShop.Model;

namespace TheShop.Services
{
    public class ShopService : IShopService
	{
		private readonly IDatabaseDriver _databaseDriver;
		private readonly ISupplierService _supplierService;
		private readonly ILogger _logger;
		
		public ShopService()
		{
			_databaseDriver = new DatabaseDriver();
			_supplierService = new SupplierService();
			_logger = new Logger();
		}

		public void OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId)
		{
			#region ordering article

			Article article = this._supplierService.FindArticleByExpectedPrice(id, maxExpectedPrice);
			#endregion

			#region selling article

			if (article == null)
			{
				throw new Exception("Could not order article");
			}

			_logger.Debug("Trying to sell article with id=" + id);

			article.IsSold = true;
			article.SoldDate = DateTime.Now;
			article.BuyerId = buyerId;
			
			try
			{
				_databaseDriver.Save(article);
				_logger.Info("Article with id=" + id + " is sold.");
			}
			catch (ArgumentNullException ex)
			{
				_logger.Error("Could not save article with id=" + id);
				throw new Exception("Could not save article with id");
			}
			catch (Exception)
			{
			}

			#endregion
		}

		public Article GetById(int id)
		{
			return _databaseDriver.GetById(id);
		}
	}

}
