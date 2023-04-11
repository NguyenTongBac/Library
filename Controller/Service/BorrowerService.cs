using Model.Tables;
using Model.DataSeed;
using Controller.IService;

namespace Controller.Service;

public class BorrowerService : IBorrowerService
{
    public bool DeleteBorrower(string libraryCardNumber)
    {
        var borrower = Data.borrowers.FirstOrDefault(x => x.LibraryCardNumber == libraryCardNumber);

        if(borrower == null)
            return false;

        Data.borrowers.Remove(borrower);
        return true;
    }

    public Borrower GetBorrowerByLibraryCardNumber(string libraryCardNumber)
    {
        return Data.borrowers.FirstOrDefault(x => x.LibraryCardNumber == libraryCardNumber);
    }

    public List<Borrower> GetListBorrowerByName(string name)
    {
        return Data.borrowers.Where(x => x.Name.Contains(name)).ToList();
    }

    public bool InsertBorrower(Borrower borrower)
    {
        Data.borrowers.Add(borrower);

        return true;
    }

    public bool UpdateBorrower(string libraryCardNumber, Borrower borrowerUpdate)
    {
        var borrower = Data.borrowers.FirstOrDefault(x => x.LibraryCardNumber == libraryCardNumber);

        if(borrower == null)
            return false;
        
        borrower = borrowerUpdate;

        return true;
    }
}