using Common.Models;

namespace BusinessLogic.Interfaces.Logic
{
    public interface IShopLogic
    {
        /// <summary>
        /// Create an order for requested shop article.
        /// If article cannot be founded in suppliers stock
        /// or there is no article with price lower than maxExpectedPrice,
        /// throws ValidationException 
        /// </summary>
        /// <param name="articleId">Requested shop article Id.</param>
        /// <param name="maxExpectedPrice">The max expected price.</param>
        /// <param name="buyerId">The buyer Id.</param>
        /// <returns>ShopArticle</returns>
        ShopArticle OrderArticleForBuyer(int articleId, int maxExpectedPrice, int buyerId);
        
        /// <summary>
        /// Sells provided shop article.
        /// </summary>
        /// <param name="shopArticle">The shop article. </param>
        void SellShopArticle(ShopArticle shopArticle);
        
        /// <summary>
        /// Retrieves Shop article with provided articleId.
        /// </summary>
        /// <param name="articleId">The shop article Id.</param>
        /// <returns>ShopArticle</returns>
        ShopArticle GetShopArticleById(int articleId);
    }
}