using System;
using TheShop.Services;

namespace Client
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var shopService = new ShopService();

			try
			{
				//order and sell
				shopService.OrderAndSellArticle(1, 460, 10);
				
				//print article on console
				var article1 = shopService.DisplayShopArticle(1);
				Console.WriteLine("Found article with ID: " + article1.Id);
				
				//print article on console				
				var article12 = shopService.DisplayShopArticle(12);
				Console.WriteLine("Found article with ID: " + article12.Id);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			
			Console.ReadKey();
		}
	}
}