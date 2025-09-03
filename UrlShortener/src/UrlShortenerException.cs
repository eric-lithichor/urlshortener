namespace UrlShortener.Exceptions;

public class UrlShortenerException : ApplicationException
{
    public UrlShortenerException() { }

    public UrlShortenerException(string message) : base(message) { }

    public UrlShortenerException(string message, Exception inner) : base(message, inner) { }
}