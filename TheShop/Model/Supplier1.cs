namespace TheShop.Model
{
    public class Supplier1
	{
		public bool ArticleInInventory(int id)
		{
			return true;
		}

		public Article GetArticle(int id)
		{
			return new Article()
			{
				Id = 1,
				ArticleName = "Article from supplier1",
				ArticlePrice = 458
			};
		}
	}
}
