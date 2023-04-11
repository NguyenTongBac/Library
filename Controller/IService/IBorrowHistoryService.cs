using Model.Tables;

namespace Controller.IService;

public interface IBorrowHistoryService
{
    List<BorrowHistory> GetListBorrowHistoryByIdBorrower(string libaryCardNumber);

    bool InsertBorrowHistory(BorrowHistory borrowHistory);

    BorrowHistory GetBorrowHistoryById(Guid id);

    List<BorrowHistory> GetListBorrowHistory();
}