namespace Model.Tables;

public class BorrowHistory
{
    public Guid Id { get; set; }

    public DateTime BorrowDate { get; set; }

    public string BorrowerId { get; set; }

    public Borrower Borrower { get; set; }

    public ICollection<BorrowHistoryDetail> BorrowHistoryDetails { get; set; }

    public BorrowHistory()
    {
        BorrowHistoryDetails = new HashSet<BorrowHistoryDetail>();
    }
    public BorrowHistory(string borrowerId)
    {
        this.Id = Guid.NewGuid();
        this.BorrowerId = borrowerId;
        this.BorrowDate = DateTime.Now;
        BorrowHistoryDetails = new HashSet<BorrowHistoryDetail>();
    }

    public BorrowHistory(Guid id, string borrowerId, DateTime borrowDate)
    {
        this.Id = id;
        this.BorrowDate = borrowDate;
        this.BorrowerId = borrowerId;
        BorrowHistoryDetails = new HashSet<BorrowHistoryDetail>();
    }

    public void GetInfo()
    {
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine("Borrow History id: " +this.Id);
        Console.WriteLine("Borrow Date: " + this.BorrowDate.ToString("dd/MM/yyyy"));
        Console.WriteLine("Number Item Borrow: " + BorrowHistoryDetails.Count);
        Console.WriteLine("----------------------------------------------------------------");
    }
}