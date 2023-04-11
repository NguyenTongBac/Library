using Model.Tables;

namespace Controller.IService;
public interface IBorrowerService
{
    List<Borrower> GetListBorrowerByName(string name);

    Borrower GetBorrowerByLibraryCardNumber(string libraryCardNumber);

    bool UpdateBorrower(string libraryCardNumber, Borrower borrower);

    bool DeleteBorrower(string libraryCardNumber);

    bool InsertBorrower(Borrower borrower);
}