namespace Model.Tables;
public class Book : Item
{
    public Book(){}
    public Book(Guid id, string title, string author, DateTime published, int numberOfPages, bool isBorrowed = false): base(id, title, author, published, isBorrowed)
    {
        this.NumberOfPages = numberOfPages;
    }

    public Book(string title, string author, DateTime published, int numberOfPages, bool isBorrowed = false) : base(title, author, published, isBorrowed)
    {
        this.NumberOfPages = numberOfPages;
    }

    public int NumberOfPages { get; set; }

    public override Book Update()
    {
        base.Update();

        Console.Write("NumberOfPage: " + this.NumberOfPages + " -> ");
        var numberOfPages = Console.ReadLine();

        this.NumberOfPages = numberOfPages == "" ? this.NumberOfPages : Convert.ToInt32(numberOfPages);

        return this;
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine("NumberOfPage: " + this.NumberOfPages);
    }
}