namespace Common.Constants
{
    public static class ErrorConstants
    {
        public const string NotFoundedSupplierArticleValidationMessage = "Article with Id={0} doesn't exist or there is no article with price<={1}.";
        public const string CannotSellNullShopArticleValidationMessage = "ShopArticle that is null can't be sold. Shop article is null.";
        public const string ShopArticleNotExistsValidationMessage = "Article with Id = {0} doesn't exist.";
        public const string FatalError = "System error. Call your administrator.";
    }
}