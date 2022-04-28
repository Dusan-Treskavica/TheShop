using System.Collections.Generic;
using System.Linq;
using Common.Models;
using DataAccess.Interfaces;

namespace DataAccess.Database
{
    //in memory implementation
    public class DatabaseDriver : IDatabaseDriver
	{
		private List<ShopArticle> _shopArticles = new List<ShopArticle>();

		public ShopArticle GetById(int id)
		{
			return _shopArticles.Single(x => x.Id == id);
		}

		public void Save(ShopArticle shopArticle)
		{
			_shopArticles.Add(shopArticle);
		}

	}
}
