using System.Collections.Generic;
using System.Linq;
using Common.Models;
using DataAccess.Interfaces;

namespace DataAccess.Database
{
    //in memory implementation
    public class DatabaseDriver : IDatabaseDriver
    {
	    private static IDatabaseDriver _instance;
	    private List<ShopArticle> _shopArticles;

	    private DatabaseDriver()
	    {
		    _shopArticles = new List<ShopArticle>();
	    }
	    
	    public static IDatabaseDriver Instance
	    {
		    get
		    {
			    if (_instance == null)
			    {
				    _instance = new DatabaseDriver();
			    }

			    return _instance;
		    }
	    }

	    public ShopArticle GetById(int articleId)
		{
			return _shopArticles.FirstOrDefault(x => x.Id == articleId);
		}

		public void Save(ShopArticle shopArticle)
		{
			_shopArticles.Add(shopArticle);
		}

	}
}
