namespace  Model.Tables;

public class Item
{
    public Item(Guid id, string title, string author, DateTime published, bool isBorrowed)
    {
        this.Id = id;
        this.Title = title;
        this.Author = author;
        this.Published = published;
        this.IsBorrowed = isBorrowed;
    }

    public Item(Guid id, string title, string author, DateTime published)
    {
        this.Id = id;
        this.Title = title;
        this.Author = author;
        this.Published = published;
    }
    
    public Item(string title, string author, DateTime published)
    {
        this.Title = title;
        this.Author = author;
        this.Published = published;
        this.Id = Guid.NewGuid();
        this.IsBorrowed = false;
    }

    public Item(string title, string author, DateTime published, bool isBorrowed)
    {
        this.Title = title;
        this.Author = author;
        this.Published = published;
        this.Id = Guid.NewGuid();
        this.IsBorrowed = isBorrowed;
    }

    public Guid Id { get; set;}

    public string Title { get; set; }

    public string Author { get; set; }

    public DateTime Published { get; set; }

    public bool IsBorrowed { get; set; }

    public ICollection<BorrowingHistoryDetail> borrowingHistoryDetails { get; set; }

    public virtual void Update()
    {
        string input;
        var date = new DateTime();

        Console.Write("Title: " + this.Title + " -> ");
        input = Console.ReadLine();
        this.Title = input == "" ? this.Title : input;
        Console.Write("Author: " + this.Author + " -> ");
        input = Console.ReadLine();
        this.Author = input == "" ? this.Author : input;
        Console.Write("Published: " + this.Published.ToString("MM/dd/yyyy") + " -(Format: MM/dd/yyyy)> ");
        input = Console.ReadLine();
        
        while (input != "" && !DateTime.TryParse(input, out date))
        {
            Console.WriteLine("Please enter right format day: ");
            input = Console.ReadLine();
        }   

        this.Published = input == "" ? this.Published : date;
    }

    public virtual void GetInfo()
    {
        Console.WriteLine("---------" + this.Title + "---------");
        Console.WriteLine("Id: " + this.Id);
        Console.WriteLine("Author: " + this.Author);
        Console.WriteLine("Published: " + this.Published.ToString("MM/dd/yyyy"));
        Console.WriteLine("IsBorrowed: " + this.IsBorrowed); 
    }

    public Item()
    {
        borrowingHistoryDetails = new HashSet<BorrowingHistoryDetail>();
    }

}