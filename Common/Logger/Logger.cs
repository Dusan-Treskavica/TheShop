using System;
using Common.Interfaces.Logger;

namespace Common.Logger
{
    public class Logger : ILogger
	{
		public void Info(string message)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Info: " + message);
			Console.ForegroundColor = ConsoleColor.White;
		}

		public void Error(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Error: " + message);
			Console.ForegroundColor = ConsoleColor.White;
		}

		public void Debug(string message)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Debug: " + message);
		}
	}
}
