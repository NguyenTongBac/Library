namespace Model.Tables;

public class BorrowingHistory
{
    public Guid Id { get; set; }

    public DateTime BorrowDate { get; set; }

    public string BorrowerId { get; set; }

    public Borrower Borrower { get; set; }

    public ICollection<BorrowingHistoryDetail> BorrowingHistoryDetails { get; set; }

    public BorrowingHistory(DateTime borrowDate)
    {
        this.BorrowDate = borrowDate;
    }

    public BorrowingHistory()
    {
        this.Id = Guid.NewGuid();
        this.BorrowDate = DateTime.Now;
        BorrowingHistoryDetails = new HashSet<BorrowingHistoryDetail>();
    }

    public BorrowingHistory(DateTime borrowDate, string BorrowerId)
    {
        this.Id = Guid.NewGuid();
        this.BorrowDate = borrowDate;
        this.BorrowerId = BorrowerId;
    }

    public BorrowingHistory(Guid id, string BorrowerId, DateTime borrowDate)
    {
        this.Id = id;
        this.BorrowDate = borrowDate;
        this.BorrowerId = BorrowerId;
    }

    public BorrowingHistory(string BorrowerId)
    {
        this.BorrowDate = DateTime.Now;
        this.Id = Guid.NewGuid();
        this.BorrowerId = BorrowerId;
    }

    public void GetInfo()
    {
        Console.WriteLine("Borrower Id: " + this.BorrowerId);
        Console.WriteLine("Borrower Name: " + this.Borrower.Name);
        Console.WriteLine("Borrow Date: " + this.BorrowDate);
        Console.WriteLine("Number of item borrowed: " + this.BorrowingHistoryDetails.Count);
    }
}