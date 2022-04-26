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
		private readonly ISupplierService _supplierService;
		private readonly IArticleService _articleService;
		private readonly ILogger _logger;
		
		public ShopService()
		{
			_supplierService = new SupplierService();
			_articleService = new ArticleService();
			_logger = new Logger();
		}

		public void OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId)
		{
			Article article = _supplierService.FindArticleByExpectedPrice(id, maxExpectedPrice);

			_articleService.OrderArticleForBuyer(article, buyerId);
			_articleService.SellArticle(article);
			
			
		}

		public Article GetById(int id)
		{
			return _articleService.GetById(id);
		}
	}

}
