using Common.Models;

namespace DataAccess.Interfaces
{
    public interface IDatabaseDriver
    {
	    /// <summary>
	    /// Retrieves the shop article from database by provided articleId.
	    /// </summary>
	    /// <param name="articleId">The shop article Id.</param>
	    /// <returns>ShopArticle</returns>
		ShopArticle GetById(int articleId);
	    
	    /// <summary>
	    /// Stores provided shop article in database.
	    /// </summary>
	    /// <param name="shopArticle">The shop article.</param>
		void Save(ShopArticle shopArticle);
    }
}
