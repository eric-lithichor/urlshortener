using UrlShortener.Exceptions;
using UrlShortener.Helpers;

string url = "http://urlshortener.corn";

string? selectionStr;
bool looping = true;
string? longUrl;
string? customUrl;
string? shortUrl;
int? clicks;

while (looping)
{
    Console.WriteLine("Select an option (1-6):");
    Console.WriteLine("-----------------------");
    Console.WriteLine("1. Create a short URL");
    Console.WriteLine("2. Create a custom short URL");
    Console.WriteLine("3. Delete a short URL");
    Console.WriteLine("4. Get a long URL with a short URL");
    Console.WriteLine("5. Get clicks for a short URL");
    Console.WriteLine("6. Exit");

    selectionStr = Console.ReadLine();
    bool success = int.TryParse(selectionStr, out int selection);

    if (success)
    {
        looping = false;
    }
    else
    {
        Console.WriteLine("\n\nOnly enter a digit from 1 to 6");
    }

    try
    {
        switch (selection)
        {
            case 1:
                Console.WriteLine("Enter a URL you want to shorten:");
                longUrl = Console.ReadLine();

                shortUrl = UrlGenerator.CreateUrl(longUrl);
                
                Console.WriteLine($"\nYour shortened URL is: {url}/{shortUrl}.\n");
                break;
            case 2:
                Console.WriteLine("Enter the URL you want to shorten:");
                longUrl = Console.ReadLine();
                Console.WriteLine("Enter the string you want to use as a custom URL:");
                customUrl = Console.ReadLine();

                UrlGenerator.CreateCustomUrl(customUrl, longUrl);

                Console.WriteLine($"\nYour shortened URL is: {url}/{customUrl}.\n");
                break;
            case 3:
                Console.WriteLine("Enter the shortened URL you want to delete:");
                shortUrl = Console.ReadLine();

                UrlGenerator.DeleteShortenedUrl(shortUrl);

                Console.WriteLine($"\n{url}/{shortUrl} was deleted.\n");
                break;
            case 4:
                Console.WriteLine("Enter the shortened URL:");
                shortUrl = Console.ReadLine();

                longUrl = UrlGenerator.GetLongUrl(shortUrl);

                Console.WriteLine($"\nYour long URL is {longUrl}.\n");
                break;
            case 5:
                Console.WriteLine("Enter the short URL you want to see statistics for:");
                shortUrl = Console.ReadLine();

                clicks = UrlGenerator.GetStatisticsForShortUrl(shortUrl);
                Console.WriteLine($"\nThe {url}/{shortUrl} URL was clicked {clicks} times.\n");
                break;
            case 6:
                Console.WriteLine("\nGood bye.\n");
                break;
            default:
                Console.WriteLine("\nNot a valid selection\n");
                break;
        }
    }
    // this outputs any errors that might have happened in the helper methods
    catch (UrlShortenerException urlEx)
    {
        Console.WriteLine(urlEx.Message);
    }
}
