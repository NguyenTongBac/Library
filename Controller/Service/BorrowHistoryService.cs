using Model.DataSeed;
using Model.Tables;
using Controller.IService;

namespace Controller.Service;

public class BorrowHistoryService : IBorrowHistoryService
{
    public BorrowHistory GetBorrowHistoryById(Guid id)
    {
        return Data.borrowHistories.GroupJoin(Data.borrowHistoryDetails.Join(Data.items, borrowHistoryDetail => borrowHistoryDetail.ItemId, item => item.Id, (borrowHistoryDetail, Item) => new BorrowHistoryDetail{
            Id = borrowHistoryDetail.Id,
            BorrowHistoryId = borrowHistoryDetail.BorrowHistoryId,
            ItemId = borrowHistoryDetail.ItemId,
            Item = Item
        }), borrowHistory => borrowHistory.Id, borrowHistoryDetail => borrowHistoryDetail.BorrowHistoryId, 
        (borrowHistory, borrowHistoryDetails) => new BorrowHistory{
            Id = borrowHistory.Id,
            BorrowDate = borrowHistory.BorrowDate,
            BorrowerId = borrowHistory.BorrowerId,
            BorrowHistoryDetails = borrowHistoryDetails.ToHashSet()
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<BorrowHistory> GetListBorrowHistoryByIdBorrower(string libaryCardNumber)
    {
        return Data.borrowHistories.Where(x => x.BorrowerId == libaryCardNumber)
        .GroupJoin(Data.borrowHistoryDetails.Join(Data.items, borrowHistoryDetail => borrowHistoryDetail.ItemId, 
        item => item.Id, (borrowHistoryDetail, Item) => new BorrowHistoryDetail{
            Id = borrowHistoryDetail.Id,
            BorrowHistoryId = borrowHistoryDetail.BorrowHistoryId,
            ItemId = borrowHistoryDetail.ItemId,
            HasReturn = borrowHistoryDetail.HasReturn,
            Item = Item
        }), borrowHistory => borrowHistory.Id, borrowHistoryDetail => borrowHistoryDetail.BorrowHistoryId, 
        (borrowHistory, borrowHistoryDetails) => new BorrowHistory{
            Id = borrowHistory.Id,
            BorrowDate = borrowHistory.BorrowDate,
            BorrowerId = borrowHistory.BorrowerId,
            BorrowHistoryDetails = borrowHistoryDetails.ToHashSet()
        }).ToList();
    }

    public List<BorrowHistory> GetListBorrowHistory()
    {
        return Data.borrowHistories
        .GroupJoin(Data.borrowHistoryDetails.Join(Data.items, borrowHistoryDetail => borrowHistoryDetail.ItemId, 
        item => item.Id, (borrowHistoryDetail, Item) => new BorrowHistoryDetail{
            Id = borrowHistoryDetail.Id,
            BorrowHistoryId = borrowHistoryDetail.BorrowHistoryId,
            ItemId = borrowHistoryDetail.ItemId,
            HasReturn = borrowHistoryDetail.HasReturn,
            Item = Item
        }), borrowHistory => borrowHistory.Id, borrowHistoryDetail => borrowHistoryDetail.BorrowHistoryId, 
        (borrowHistory, borrowHistoryDetails) => new BorrowHistory{
            Id = borrowHistory.Id,
            BorrowDate = borrowHistory.BorrowDate,
            BorrowerId = borrowHistory.BorrowerId,
            BorrowHistoryDetails = borrowHistoryDetails.ToHashSet()
        }).ToList();
    }

    public bool InsertBorrowHistory(BorrowHistory borrowHistory)
    {
        if(borrowHistory.BorrowHistoryDetails.Count == 0 || borrowHistory.BorrowHistoryDetails.Any(x => x.ItemId == null))
            return false;
        
        Data.borrowHistories.Add(borrowHistory);
        Data.borrowHistoryDetails.AddRange(borrowHistory.BorrowHistoryDetails);
        Data.items.Where(x => borrowHistory.BorrowHistoryDetails.Any(y => y.ItemId == x.Id)).ToList().ForEach(x => x.IsBorrowed = true);

        return true;
    }
}