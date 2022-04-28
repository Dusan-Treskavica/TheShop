using Common.Models;

namespace TheShop.Interfaces.Services
{
    public interface IShopService
    {
        /// <summary>
        /// Creates an order for requested article and sells it to buyer.
        /// </summary>
        /// <param name="articleId">The shop article Id.</param>
        /// <param name="maxExpectedPrice">Max expected price to be on the order.</param>
        /// <param name="buyerId">The buyer Id.</param>
        void OrderAndSellArticle(int articleId, int maxExpectedPrice, int buyerId);
        
        /// <summary>
        /// Getting shop article to be displayed by articleId.
        /// </summary>
        /// <param name="articleId">The shop article Id.</param>
        /// <returns>ShopArticle</returns>
        ShopArticle DisplayShopArticle(int articleId);
    }
}
