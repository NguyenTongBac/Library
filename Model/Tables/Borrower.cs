using System.ComponentModel.DataAnnotations;

namespace Model.Tables;

public class Borrower
{
    public string LibraryCardNumber { get; set; }

    public string Name { get; set; }
    
    public string Address { get; set; }

    public ICollection<BorrowingHistory> BorrowingHistories { get; set; }

    public Borrower(string name, string address, string libraryCardNumber)
    {
        this.Name = name;
        this.Address = address;
        this.LibraryCardNumber = libraryCardNumber;
    }

    public Borrower()
    {
        BorrowingHistories = new HashSet<BorrowingHistory>();
    }

    public void GetInfo()
    {
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Address: " + Address);
        Console.WriteLine("LibraryCardNumber: " + LibraryCardNumber);
        Console.WriteLine("----------------------------------------------------------------");
    }
}