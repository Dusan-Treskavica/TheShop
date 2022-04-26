using System.Collections.Generic;
using System.Linq;
using TheShop.Interfaces.Database;
using TheShop.Model;

namespace TheShop.Database
{
    //in memory implementation
    public class DatabaseDriver : IDatabaseDriver
	{
		private List<Article> _articles = new List<Article>();

		public Article GetById(int id)
		{
			return _articles.Single(x => x.Id == id);
		}

		public void Save(Article article)
		{
			_articles.Add(article);
		}

		public IList<Supplier> GetSuppliers()
		{
			return new List<Supplier>
			{
				new Supplier()
				{
					Id = 1, 
					Name = "Supplier1", 
					Articles = new List<Article>
					{
						new Article()
						{
							Id = 1,
							Name = "Article from supplier1",
							Price = 458
						}
					}
				},
				new Supplier()
				{
					Id = 2, 
					Name = "Supplier2", 
					Articles = new List<Article>
					{
						new Article()
						{
							Id = 1,
							Name = "Article from supplier2",
							Price = 459
						}
					}
				},
				new Supplier()
				{
					Id = 3, 
					Name = "Supplier3", 
					Articles = new List<Article>
					{
						new Article()
						{
							Id = 1,
							Name = "Article from supplier3",
							Price = 460
						}
					}
				}
			};
		}
	}
}
