using Common.Models;

namespace BusinessLogic.Interfaces.Services
{
    public interface IArticleService
    {
        /// <summary>
        /// Retrieves Shop article with provided articleId.
        /// </summary>
        /// <param name="articleId">The shop article Id.</param>
        /// <returns>ShopArticle</returns>
        ShopArticle GetById(int articleId);
        
        /// <summary>
        /// Saves provided shop article.
        /// </summary>
        /// <param name="shopArticle">The shop article. </param>
        void Save(ShopArticle shopArticle);
    }
}
