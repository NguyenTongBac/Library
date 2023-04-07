namespace Model.Tables;
public class Book : Item
{
    public Book(Guid id, string title, string author, DateTime published, bool isBorrowed, int numberOfPages): base(id, title, author, published, isBorrowed)
    {
        this.NumberOfPages = numberOfPages;
    }

    public Book(Guid id, string title, string author, DateTime published, int numberOfPages) : base(id, title, author, published)
    {
        this.NumberOfPages = numberOfPages;
    }

    public Book(string title, string author, DateTime published, int numberOfPages) : base(title, author, published)
    {
        this.NumberOfPages = numberOfPages;
    }

    public Book(string title, string author, DateTime published, int numberOfPages, bool isBorrowed) : base(title, author, published, isBorrowed)
    {
        this.NumberOfPages = numberOfPages;
        this.IsBorrowed = isBorrowed;
    } 

    public int NumberOfPages { get; set; }

    public override void Update()
    {
        base.Update();
        Console.Write("NumberOfPage: " + this.NumberOfPages + " -> ");
        var numberOfPages = Console.ReadLine();
        this.NumberOfPages = numberOfPages == "" ? this.NumberOfPages : Convert.ToInt32(numberOfPages);
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine("NumberOfPage: " + this.NumberOfPages);
    }
}