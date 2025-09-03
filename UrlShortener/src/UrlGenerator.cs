using System.ComponentModel;
using UrlShortener.Exceptions;

namespace UrlShortener.Helpers;

public class UrlGenerator
{
    static readonly string url = "http://urlshortener.corn";

    /// <summary>
    /// This creates a unique eight character string for a url
    /// An exception is thrown if we can't create a unique short URL in ten attempts
    /// An exception is thrown if there's a problem saving the URL
    /// </summary>
    /// <param name="longUrl"></param>
    /// <exception cref="UrlShortenerException"></exception>
    /// <returns>A shortened URL</returns>
    public static string CreateUrl(string? longUrl)
    {
        string shortUrlId = "";
        bool duplicate = true;
        int counter = 0;

        if (string.IsNullOrEmpty(longUrl))
        {
            throw new UrlShortenerException("Can't shorten a null or empty URL.");
        }

        // Try to get a unique guid for the shortened URL
        // If it takes more than ten tries, throw an exception
        while (duplicate)
        {
            shortUrlId = Guid.NewGuid().ToString("N")[..8];
            // check db for uniqueness - we need this because GUIDs aren't necessarily unique, and using
            // only eight characters (instead of 32) makes it more probable there will be dupes
            duplicate = DatabaseHelper.CheckForDuplicates(shortUrlId);

            counter++;
            if (counter >= 10)
            {
                throw new UrlShortenerException("Could not get a uniquely shortened URL");
            }
        }

        // save to db
        bool saved = DatabaseHelper.SaveToDatabase(longUrl, shortUrlId);
        if (!saved)
        {
            throw new UrlShortenerException("There was a problem saving the short URL");
        }
        return shortUrlId;
    }

    /// <summary>
    /// This associates a long URL with a custom URL the user inputs
    /// An exception is thrown if the URL already exists
    /// An exception is thrown if there's a problem saving the URL
    /// </summary>
    /// <param name="customUrlIn"></param>
    /// <param name="longUrl"></param>
    /// <exception cref="UrlShortenerException"></exception>
    public static void CreateCustomUrl(string? customUrlIn, string? longUrl)
    {
        bool saved;
        string? customUrl = customUrlIn;

        if (string.IsNullOrEmpty(longUrl))
        {
            throw new UrlShortenerException("Can't shorten a null or empty URL.");
        }
        if (string.IsNullOrEmpty(customUrlIn))
        {
            throw new UrlShortenerException("Can't use a null or empty string as a custom URL.");
        }

        bool exists = DatabaseHelper.CheckForDuplicates(customUrl);
        if (exists)
        {
            throw new UrlShortenerException($"The URL {url}/{customUrl} already exists.");
        }

        saved = DatabaseHelper.SaveToDatabase(longUrl, customUrl);
        if (!saved)
        {
            throw new UrlShortenerException("There was a problem saving the short URL");
        }
    }

    /// <summary>
    /// Deletes a shortened URL from storage
    /// This doesn't check to see if the short URL exists or not, because it
    /// doesn't matter - the database helper will handle any issues with
    /// a URL that isn't in the db, and the end result will be that the db
    /// won't contain the short URL
    /// </summary>
    /// <param name="shortUrl"></param>
    /// <exception cref="UrlShortenerException"></exception>
    public static void DeleteShortenedUrl(string? shortUrl)
    {
        if (string.IsNullOrEmpty(shortUrl))
        {
            throw new UrlShortenerException("Can't delete a null or empty URL.");
        }
        // strip out the url - we only save the id part
        string id = shortUrl.Replace(url, "");

        DatabaseHelper.DeleteShortUrl(shortUrl);
    }

    /// <summary>
    /// This returns the original URL that was shortened
    /// An exception is thrown if the short URL isn't found in the db
    /// </summary>
    /// <param name="shortUrl"></param>
    /// <returns></returns>
    /// <exception cref="UrlShortenerException"></exception>
    public static string GetLongUrl(string? shortUrl)
    {
        string longUrl;

        if (string.IsNullOrEmpty(shortUrl))
        {
            throw new UrlShortenerException("Can't check for a null or empty URL.");
        }

        longUrl = DatabaseHelper.GetLongUrl(shortUrl);
        if (string.IsNullOrEmpty(longUrl))
        {
            throw new UrlShortenerException($"There is no long URL associated with {shortUrl}");
        }

        return longUrl;
    }

    /// <summary>
    /// This returns the number of times the shortUrl was clicked
    /// An exception is thrown if the short URL doesn't exist
    /// </summary>
    /// <param name="shortUrl"></param>
    /// <returns></returns>
    /// <exception cref="UrlShortenerException"></exception>
    public static int GetStatisticsForShortUrl(string? shortUrl)
    {
        if (string.IsNullOrEmpty(shortUrl))
        {
            throw new UrlShortenerException("Can't get stats for a null or empty URL.");
        }

        bool duplicate = DatabaseHelper.CheckForDuplicates(shortUrl);
        if (duplicate)
        {
            throw new UrlShortenerException("The URL you asked for doesn't exist");
        }

        int numberOfClicks = DatabaseHelper.GetStatisticsForShortUrl(shortUrl);
        return numberOfClicks;
    }
}