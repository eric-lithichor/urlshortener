namespace UrlShortener.Helpers;

/// <summary>
/// This class contains stubbed out methods to represent what the database might return
/// </summary>
public static class DatabaseHelper
{
    public static bool CheckForDuplicates(string? strToCheck)
    {
        bool isDuplicate = true;

        // we're always going to say the string is unique
        isDuplicate = false;

        return isDuplicate;
    }

    public static bool SaveToDatabase(string? longUrl, string? shorturl)
    {
        bool success = false;

        // we're always going to say it was saved successfully
        success = true;

        return success;
    }

    public static void DeleteShortUrl(string? url)
    {
        // this is a stub method that returns nothing
    }

    public static string GetLongUrl(string? shortUrl)
    {
        string? longUrl = "https://www.whateveryoucandoicandobetter.corn?input=neverinamillionyears&output=whoshotjr";

        return longUrl;
    }

    public static int GetStatisticsForShortUrl(string? shortUrl)
    {
        // return a random integer up to 1000 as the statistic
        Random rand = new();
        return rand.Next(1001);
    }
}