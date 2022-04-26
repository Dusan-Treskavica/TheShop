using System;
using TheShop.Common;
using TheShop.Database;
using TheShop.Interfaces.Common;
using TheShop.Interfaces.Services;
using TheShop.Model;

namespace TheShop.Services
{
    public class ShopService : IShopService
	{
		private DatabaseDriver DatabaseDriver;
		private ISupplierService supplierService;
		private ILogger logger;

		private Supplier1 Supplier1;
		private Supplier2 Supplier2;
		private Supplier3 Supplier3;
		
		public ShopService()
		{
			DatabaseDriver = new DatabaseDriver();
			supplierService = new SupplierService();
			logger = new Logger();
			Supplier1 = new Supplier1();
			Supplier2 = new Supplier2();
			Supplier3 = new Supplier3();
		}

		public void OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId)
		{
			#region ordering article

			Article article = null;
			Article tempArticle = null;
			var articleExists = Supplier1.ArticleInInventory(id);
			if (articleExists)
			{
				tempArticle = Supplier1.GetArticle(id);
				if (maxExpectedPrice < tempArticle.Price)
				{
					articleExists = Supplier2.ArticleInInventory(id);
					if (articleExists)
					{
						tempArticle = Supplier2.GetArticle(id);
						if (maxExpectedPrice < tempArticle.Price)
						{
							articleExists = Supplier3.ArticleInInventory(id);
							if (articleExists)
							{
								tempArticle = Supplier3.GetArticle(id);
								if (maxExpectedPrice < tempArticle.Price)
								{
									article = tempArticle;
								}
							}
						}
					}
				}
			}
			
			article = tempArticle;
			#endregion

			#region selling article

			if (article == null)
			{
				throw new Exception("Could not order article");
			}

			logger.Debug("Trying to sell article with id=" + id);

			article.IsSold = true;
			article.SoldDate = DateTime.Now;
			article.BuyerId = buyerId;
			
			try
			{
				DatabaseDriver.Save(article);
				logger.Info("Article with id=" + id + " is sold.");
			}
			catch (ArgumentNullException ex)
			{
				logger.Error("Could not save article with id=" + id);
				throw new Exception("Could not save article with id");
			}
			catch (Exception)
			{
			}

			#endregion
		}

		public Article GetById(int id)
		{
			return DatabaseDriver.GetById(id);
		}
	}

}
