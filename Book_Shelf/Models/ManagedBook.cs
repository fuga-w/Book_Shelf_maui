namespace Book_Shelf.Models;
public class ManagedBook
{
    public string Isbn { get; set; }
    public string CoverImage { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}

public class CoverImage
{
    public static string NotFound => "cover_not_found.png";
}