using System.ComponentModel.DataAnnotations;

namespace Model.Tables;

public class Borrower
{
    public string LibraryCardNumber { get; set; }

    public string Name { get; set; }
    
    public string Address { get; set; }

    public ICollection<BorrowHistory> BorrowingHistories { get; set; }

    public Borrower(string name, string address, string libraryCardNumber)
    {
        this.Name = name;
        this.Address = address;
        this.LibraryCardNumber = libraryCardNumber;
        BorrowingHistories = new HashSet<BorrowHistory>();
    }

    public Borrower()
    {
        BorrowingHistories = new HashSet<BorrowHistory>();
    }

    public void GetInfo()
    {
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Address: " + Address);
        Console.WriteLine("LibraryCardNumber: " + LibraryCardNumber);
        Console.WriteLine("----------------------------------------------------------------");
    }

    public Borrower Update()
    {
        Console.Write("Name " + this.Name + " -> ");
        string name = Console.ReadLine();
        this.Name = name == "" ? this.Name : name;
        Console.Write("Address " + this.Address + " -> ");
        string address = Console.ReadLine();
        this.Address = address == "" ? this.Address : address;

        return this;
    }
}