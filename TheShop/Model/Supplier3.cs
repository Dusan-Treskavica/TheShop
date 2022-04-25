namespace TheShop.Model
{
    public class Supplier3
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
				ArticleName = "Article from supplier3",
				ArticlePrice = 460
			};
		}
	}
}
