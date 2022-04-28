namespace Common.Interfaces.Logger
{
    public interface ILogger
    {
	    /// <summary>
	    /// Logs the message as Info message.
	    /// </summary>
	    /// <param name="message">The message to be logged.</param>
		void Info(string message);
	    
	    /// <summary>
	    /// Logs the message as Error message.
	    /// </summary>
	    /// <param name="message">The message to be logged.</param>
		void Error(string message);
	    
	    /// <summary>
	    /// Logs the message as Debug message.
	    /// </summary>
	    /// <param name="message">The message to be logged.</param>
		void Debug(string message);
	}
}
