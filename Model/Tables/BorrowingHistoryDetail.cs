using System.ComponentModel.DataAnnotations;

namespace Model.Tables;

public class BorrowingHistoryDetail
{
    public Guid Id { get; set; }

    public Guid ItemId { get; set; }

    public Item Item { get; set; }

    public Guid BorrowingHistoryId { get; set; }

    public BorrowingHistory BorrowingHistory { get; set; }

    public bool HasReturn { get; set; }

    public BorrowingHistoryDetail(Guid itemId, Guid borrowingHistoryId)
    {
        this.Id = Guid.NewGuid();
        this.ItemId = itemId;
        this.BorrowingHistoryId = borrowingHistoryId;
    }

    public BorrowingHistoryDetail(Guid id, Guid itemId, Guid borrowingHistoryId)
    {
        this.Id = id;
        this.ItemId = itemId;
        this.BorrowingHistoryId = borrowingHistoryId;
    }

    public BorrowingHistoryDetail(Guid id, Guid itemId, Guid borrowingHistoryId, bool hasReturn)
    {
        this.Id = id;
        this.ItemId = itemId;
        this.BorrowingHistoryId = borrowingHistoryId;
        this.HasReturn = hasReturn;
    }

    public void GetInfo()
    {
        Console.WriteLine("Borrowing History id: " + this.BorrowingHistoryId);
        Console.WriteLine("Item Id: " + this.ItemId);
        Console.WriteLine("Item Title: " + this.Item.Title);
        Console.WriteLine("Is return: " + this.HasReturn);
    }
}