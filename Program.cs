using Model.Tables;
using Controller.IService;
using Controller.Service;

internal class Program
{
    private static readonly IBorrowerService _borrowerService = new BorrowerService();

    private static readonly IItemService _itemService = new ItemService();

    private static readonly IBorrowHistoryService _borrowHistoryService = new BorrowHistoryService();

    static bool isLogin = false;

    static void Login()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the library");
        Console.Write("Please enter your card number: ");
        string libraryCardNumber = Console.ReadLine();
        while (!isLogin)
        {
            if (libraryCardNumber == "" || libraryCardNumber == null)
            {
                Console.Write("Please enter a card number: ");
                libraryCardNumber = Console.ReadLine();
            }
            else if (libraryCardNumber == "admin")
            {
                isLogin = true;
                Console.Clear();
                AdminPage();
                libraryCardNumber = null;
                continue;
            }
            else
            {
                var borrower = _borrowerService.GetBorrowerByLibraryCardNumber(libraryCardNumber);
                if (borrower != null)
                {
                    isLogin = true;
                    Console.Clear();
                    CustomerPage(libraryCardNumber);
                    libraryCardNumber = null;
                    continue;
                }
                else
                {
                    Console.Write("Card Number wrong!!! \nPlease enter a card number again: ");
                    libraryCardNumber = Console.ReadLine();
                }
            }
        }
    }
    
    // admin
    static void AdminPage()
    {
        while (isLogin)
        {
            Console.Clear();
            Console.WriteLine("Welcome to admin page");
            Console.WriteLine("Select option: ");
            Console.WriteLine("0. Logout");
            Console.WriteLine("1. Manager Item");
            Console.WriteLine("2. Manager Borrower");
            Console.WriteLine("3. Manager BorrowHistory");
            Console.Write("Please enter your number choice: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    isLogin = false;
                    Console.Clear();
                    break;
                
                case "1":
                    Console.Clear();
                    ManagerItem();
                    break;
                case "2":
                    Console.Clear();
                    ManagerBorrower();
                    break;

                case "3":
                    Console.Clear();
                    ManagerBorrowHistory();
                    break;

                default:
                    break;
            }
        }

        // Manager Item
        void ManagerItem()
        {
            while(isLogin)
            {
                Console.Clear();
                Console.WriteLine("Welcome to manager Item page");
                Console.WriteLine("Select option: ");
                Console.WriteLine("0. Go back");
                Console.WriteLine("1. Get Item");
                Console.WriteLine("2. Get List Item by name");
                Console.WriteLine("3. Update/Delete Item");
                Console.WriteLine("4. Insert Item");
                Console.WriteLine("5. Get Item is book");
                Console.WriteLine("6. Count DVD in library and search by year");
                Console.Write("Please enter your number choice: ");
                string? choice = Console.ReadLine();

                switch(choice)
                {
                    case "0":
                        return;

                    case "1":
                        Console.Clear();
                        GetItemById();
                        break;

                    case "2":
                        Console.Clear();
                        GetListItemByName();
                        break;
                    
                    case "3":
                        Console.Clear();
                        UDItem();
                        break;

                    case "4":
                        Console.Clear();
                        InsertItem();
                        break;

                    case "5":
                        Console.Clear();
                        Console.Write("Please enter name of book: ");
                        GetBook(Console.ReadLine());
                        break;

                    case "6":
                        Console.Clear();
                        Console.Write("Please enter the year: ");
                        CountDVD(Console.ReadLine());
                        break;

                    default:
                        break;
                }
            }
            void CountDVD(string input)
            {
                if (!int.TryParse(input, out int year) || input.Length != 4) 
                {
                    Console.WriteLine("The input is not year");
                    Console.WriteLine("Press any key to continue");
                    Console.Read();
                    return;
                }
                Console.WriteLine("The number of DVD in " + year + " is: " + _itemService.CountDVD(year));
                Console.WriteLine("Press any key to continue");
                Console.Read();
            }

            void GetBook(string name)
            {
                var books = _itemService.GetListBookWithSearch(name);
                if(books.Count == 0)
                {
                    Console.WriteLine("There is no book in library");
                    Console.WriteLine("Press any key to continue");
                    Console.Read();
                    return;
                }

                books.ForEach(x => x.GetInfo());
                Console.WriteLine("Press any key to continue");
                Console.Read();
            }

            void GetItemById()
            {
                Console.Write("Please enter your id item: ");
                string id = Console.ReadLine();
                var item = _itemService.GetItemById(new Guid(id));

                item.GetInfo();
                Console.WriteLine("Press any key to continue!");
                Console.Read();
            }
            
            void GetListItemByName()
            {
                Console.Write("Please enter the name: ");
                var items = _itemService.GetListItemByName(Console.ReadLine());

                if(items.Count == 0)
                    Console.WriteLine("There is no item with key search!");
                else
                {
                    foreach(var item in items)
                    {
                        item.GetInfo();
                    }
                }

                Console.WriteLine("Press any key to continue!");
                Console.Read();
            }
        
            void UDItem()
            {
                var items = _itemService.GetListItemNotBorrow();
                
                if(items.Count == 0)
                {
                    Console.WriteLine("There is no item in library");
                    Console.WriteLine("Press any key to continue:");
                    Console.Read();
                    return;
                }

                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + items[i].Title);
                }

                Console.WriteLine("Choice your option:");
                Console.WriteLine("1. Update item");
                Console.WriteLine("2. Delete Item");
                Console.WriteLine("Press any key to go back!");
                string option = Console.ReadLine();

                if(option == "1")
                {
                    Console.WriteLine("Enter the number item of list: ");
                    string numberItem = Console.ReadLine();
                    int number;
                    while(!int.TryParse(numberItem, out number))
                    {
                        Console.Write("Please enter the number: ");
                    }

                    var item = items[number-1];
                    if(_itemService.UpdateItemById(item.Id, item.Update()))
                    {
                        Console.WriteLine("Update successfully!");
                        Console.WriteLine("Press any key to continue");
                        Console.Read();
                    }
                    else
                    {
                        Console.WriteLine("Errors!");
                    }
                }
                else if(option == "2")
                {
                    Console.WriteLine("Enter the number item of list: ");
                    string numberItem = Console.ReadLine();
                    int number;
                    while(!int.TryParse(numberItem, out number))
                    {
                        Console.Write("Please enter the number: ");
                    }

                    if(_itemService.DeleteItemById(items[number-1].Id))
                    {
                        Console.WriteLine("Delete successfully!");
                        Console.WriteLine("Press any key to continue");
                        Console.Read();
                    }
                    else
                    {
                        Console.WriteLine("Errors");
                        Console.WriteLine("Press any key to continue");
                        Console.Read();
                    }
                }
            }

            void InsertItem()
            {
                Console.WriteLine("--------Insert item--------");
                Console.WriteLine("Please enter your choice:");
                Console.WriteLine("1. Insert your Book into the library");
                Console.WriteLine("2. Insert your DVD into the library");
                Console.WriteLine("Press any key to go back");
                string? choice2 = Console.ReadLine();
                switch (choice2)
                {
                    case "1":
                        InsertBook();
                        break;

                    case "2":
                        InsertDVD();
                        break;

                    default:
                        break;
                }
                
                void InsertDVD()
                {
                    Console.Clear();
                    Console.Write("Nhap ten DVD: ");
                    string? title = Console.ReadLine();
                    Console.Write("Nhap ten tac gia: ");
                    string? author = Console.ReadLine();
                    Console.Write("Nhap ngay phat hanh (Format: mm/dd/yyyy): ");
                    string? published = Console.ReadLine();
                    DateTime date = new DateTime();
                    while(published == "" || !DateTime.TryParse(published, out date))
                    {
                        Console.Write("Please enter right format day (Format: mm/dd/yyyy): ");
                        published = Console.ReadLine();
                    }
                    Console.Write("Nhap do dai cua dia (Format: hh:mm:ss): ");
                    string? runTime = Console.ReadLine();
                    TimeSpan timeSpan = new TimeSpan();
                    while(runTime == "" || !TimeSpan.TryParse(runTime, out timeSpan))
                    {
                        Console.Write("Please enter right format time (Format: hh:mm:ss): ");
                        runTime = Console.ReadLine();
                    }
                    var dVD = new DVD(title, author, date, timeSpan);
                    _itemService.InsertItem(dVD);
                    Console.WriteLine("Insert successfully");
                    Console.WriteLine("Press enter to go back...");
                    Console.Read();
                }

                void InsertBook()
                {
                    Console.Clear();
                    Console.Write("Nhap ten sach: ");
                    string title = Console.ReadLine();
                    Console.Write("Nhap ten tac gia: ");
                    string author = Console.ReadLine();
                    Console.Write("Nhap ngay phat hanh (Format: mm/dd/yyyy): ");
                    string published = Console.ReadLine();
                    DateTime date = new DateTime();
                    
                    while (published == "" || !DateTime.TryParse(published, out date))
                    {
                        Console.Write("Please enter right format day (Format: mm/dd/yyyy): ");
                        published = Console.ReadLine();
                    }

                    Console.Write("Nhap so trang: ");
                    string numberOfPages = Console.ReadLine();
                    int number;
                    while(!int.TryParse(numberOfPages, out number)){
                        Console.Write("Please enter the number: ");
                        numberOfPages = Console.ReadLine();
                    }
                    var insertBook = new Book(title, author, date, number);
                    _itemService.InsertItem(insertBook);
                    Console.WriteLine("Insert successfully");
                    Console.WriteLine("Press enter to go back...");
                    Console.Read();
                }
            }
        }
    
        // Manager Borrower
        void ManagerBorrower()
        {
            while(isLogin)
            {
                Console.Clear();
                Console.WriteLine("Welcome to manager Item page");
                Console.WriteLine("Select option: ");
                Console.WriteLine("0. Go back");
                Console.WriteLine("1. Get borrower");
                Console.WriteLine("2. Get List borrower by name");
                Console.WriteLine("3. Update/Delete borrower");
                Console.WriteLine("4. Insert borrower");
                Console.WriteLine("5. Get all borrwer who borrow books and DVDs at the same borrow history");
                Console.Write("Please enter your number choice: ");
                string? choice = Console.ReadLine();

                switch(choice)
                {
                    case "0":
                        return;

                    case "1":
                        Console.Clear();
                        GetBorrowerById();
                        break;

                    case "2":
                        Console.Clear();
                        GetListBorrowerByName();
                        break;
                    
                    case "3":
                        Console.Clear();
                        UDBorrower();
                        break;

                    case "4":
                        Console.Clear();
                        InsertBorrower();
                        break;

                    case "5":
                        Console.Clear();
                        GetListBorrowerBorrow2TypeItem();
                        break;

                    default:
                        break;
                }
            }

            void GetListBorrowerBorrow2TypeItem()
            {
                var borrowHistories = _borrowHistoryService.GetListBorrowHistory();
                borrowHistories = borrowHistories.Where(x => x.BorrowHistoryDetails.Any(y => y.Item is Book) && x.BorrowHistoryDetails.Any(y => y.Item is DVD)).ToList();
                foreach(var borrowHistory in borrowHistories)
                {
                    _borrowerService.GetBorrowerByLibraryCardNumber(borrowHistory.BorrowerId).GetInfo();
                    
                    borrowHistory.GetInfo();
                    Console.WriteLine("Do you want to see the details? (y/n)");

                    if(Console.ReadLine() == "y")
                    {
                        borrowHistory.BorrowHistoryDetails.ToList().ForEach(x => {
                            x.GetInfo();
                        });

                        Console.WriteLine("Press any key to continue!!");
                        Console.Read();
                    }
                }
            }

            void GetBorrowerById()
            {
                Console.Write("Please enter libary card number of borrower: ");
                var borrower = _borrowerService.GetBorrowerByLibraryCardNumber(Console.ReadLine());
                
                if(borrower == null)
                    Console.WriteLine("There is no borrower!");
                else
                    borrower.GetInfo();
                Console.WriteLine("Press any key to continue");
                Console.Read();
            }
        
            void GetListBorrowerByName()
            {
                Console.Write("Please enter the name: ");
                string name = Console.ReadLine();
                var borrowers = _borrowerService.GetListBorrowerByName(name);

                if(borrowers.Count == 0)
                    Console.WriteLine("There is no borrower with key search!");

                else
                {
                    foreach(var borrower in borrowers)
                    {
                        borrower.GetInfo();
                    }
                }

                Console.WriteLine("Press any key to continue!");
                Console.Read();
            }
        
            void UDBorrower()
            {
                var borrowers = _borrowerService.GetListBorrowerByName("");
                
                if(borrowers.Count == 0)
                {
                    Console.WriteLine("There is no borrower in library");
                    Console.WriteLine("Press any key to continue:");
                    Console.Read();
                    return;
                }

                for (int i = 0; i < borrowers.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + "Name: " + borrowers[i].Name + " Address: " + borrowers[i].Address);
                }

                Console.WriteLine("Choice your option:");
                Console.WriteLine("1. Update item");
                Console.WriteLine("2. Delete Item");
                Console.WriteLine("Press any key to go back!");
                string option = Console.ReadLine();

                if(option == "1")
                {
                    Console.WriteLine("Enter the number borrower of list: ");
                    string numberItem = Console.ReadLine();
                    int number;
                    while(!int.TryParse(numberItem, out number))
                    {
                        Console.Write("Please enter the number: ");
                    }

                    var borrower = borrowers[number-1];
                    if(_borrowerService.UpdateBorrower(borrower.LibraryCardNumber, borrower.Update()))
                        Console.WriteLine("Update successfully!");
                    else
                        Console.WriteLine("Errors!");
                    Console.WriteLine("Press any key to continue");
                    Console.Read();
                }
                else if(option == "2")
                {
                    Console.WriteLine("Enter the number item of list: ");
                    string numberItem = Console.ReadLine();
                    int number;
                    while(!int.TryParse(numberItem, out number))
                    {
                        Console.Write("Please enter the number: ");
                    }

                    if(_borrowerService.DeleteBorrower(borrowers[number-1].LibraryCardNumber))
                        Console.WriteLine("Delete successfully!");
                    else
                        Console.WriteLine("Errors");
                    Console.WriteLine("Press any key to continue");
                    Console.Read();
                }
            }
        
            void InsertBorrower()
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();
                Console.Write("Enter you address: ");
                string address = Console.ReadLine();
                var borrowers = _borrowerService.GetListBorrowerByName("");
                var borrower = new Borrower(name, address, (borrowers.Count + 1).ToString());

                if(_borrowerService.InsertBorrower(borrower))
                    Console.WriteLine("Insert borrower successfully");
                else
                    Console.WriteLine("Errors");
                Console.WriteLine("Press any key to go back");
                Console.ReadLine();
            }
        }
        
        // Manager History Borrow
        void ManagerBorrowHistory()
        {
            while(isLogin)
            {
                Console.Clear();
                Console.WriteLine("Welcome to manager Item page");
                Console.WriteLine("Select option: ");
                Console.WriteLine("0. Go back");
                Console.WriteLine("1. Get borrow history of borrower");
                Console.WriteLine("2. Get all history borrow group by borrower");
                Console.Write("Please enter your number choice: ");
                string? choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Please enter the number of library card: ");
                        GetListBorrowHistoryByBorrowerId(Console.ReadLine());
                        break;
                    
                    case "2":
                        Console.Clear();
                        GetAllHistoryBorrowGroupByBorrower();
                        break;
                    
                    case "0":
                        Console.Clear();
                        return;
                    
                    default:
                        break;
                }
            }

            void GetListBorrowHistoryByBorrowerId(string libaryCardNumber = null)
            {
                var borrower = _borrowerService.GetBorrowerByLibraryCardNumber(libaryCardNumber);

                while(libaryCardNumber == null || libaryCardNumber == "" || borrower == null)
                {
                    Console.Write("Please enter the number of library card: ");
                    libaryCardNumber = Console.ReadLine();
                }
                
                var borrowHistories = _borrowHistoryService.GetListBorrowHistoryByIdBorrower(libaryCardNumber);

                if(borrowHistories.Count == 0)
                {
                    Console.WriteLine("There is no borrower history with this library card number");
                    Console.WriteLine("Press any key to return");
                    Console.Read();
                    return;
                }
                
                Console.Write(borrower.Name);
                foreach(var borrowHistory in borrowHistories)
                {
                    borrowHistory.GetInfo();
                    Console.WriteLine("Do you want to see the details? (y/n)");

                    if(Console.ReadLine() == "y")
                    {
                        borrowHistory.BorrowHistoryDetails.ToList().ForEach(x => {
                            x.GetInfo();
                        });

                        Console.WriteLine("Press any key to continue!!");
                        Console.Read();
                    }
                }
            }
        
            void GetAllHistoryBorrowGroupByBorrower()
            {
                var borrowers = _borrowerService.GetListBorrowerByName("");

                foreach(var borrower in borrowers)
                {
                    GetListBorrowHistoryByBorrowerId(borrower.LibraryCardNumber);
                }

                Console.WriteLine("End List");
                Console.WriteLine("Press any key to continue");
                Console.Read();
            }
        }  
    }

    static void CustomerPage(string libraryCardNumber)
    {
        while (isLogin)
        {
            Console.Clear();
            Console.WriteLine("Please enter your choice:");
            Console.WriteLine("0. Logout");
            Console.WriteLine("1. Show information about your account");
            Console.WriteLine("2. Borrow item from the library");
            Console.WriteLine("3. Return item to the library");
            Console.WriteLine("4. Search items by name");
            Console.WriteLine("5. Search item borrow by you");
            Console.WriteLine("6. Show history borrowed with details of borrower");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "0":
                    Console.Clear();
                    isLogin = false;
                    break;

                case "1":
                    Console.Clear();
                    _borrowerService.GetBorrowerByLibraryCardNumber(libraryCardNumber).GetInfo();
                    Console.WriteLine("Press any key to continue!");
                    Console.Read();
                    break;

                case "2":
                    Console.Clear();
                    BorrowItems(libraryCardNumber);
                    break;

                case "3":
                    Console.Clear();
                    ReturnItems(libraryCardNumber);
                    break;

                case "4":
                    Console.Clear();
                    Console.Write("Please enter keyword to search the book: ");
                    var items = _itemService.GetListItemByName(Console.ReadLine());
                    items.ForEach(x => x.GetInfo());
                    Console.Read();
                    break;

                case "5":
                    Console.Clear();
                    Console.WriteLine("Enter keyword to search");
                    GetListItemBorrowByBorrower(libraryCardNumber, Console.ReadLine());
                    break;

                case "6":
                    Console.Clear();
                    GetListBorrowHistoryByBorrowerId(libraryCardNumber);
                    break;

                default:
                    break;
            }
        }
        
        // customer service
        void BorrowItems(string libraryCardNumber)
        {
            // show item
            var items = _itemService.GetListItemByName("").Where(x => x.IsBorrowed == false).ToList();
            
            for(int i = 0; i < items.Count; i++)
            {
                Console.WriteLine((i+1) + ". " + items[i].Title);
            }

            // Kiem tra cu phap so
            Console.Write("Enter item you want borrow: ");
            string choice = Console.ReadLine();
            if(choice == "")
                return;

            string[] arg = choice.Split(",");

            if(arg.Length == 0)
            {
                Console.WriteLine("khong nhan dang duoc input");
            }

            List<int> numbers = new List<int>();
            foreach (string part in arg)
            {
                if (int.TryParse(part.Trim(), out int number))
                {
                    numbers.Add(number);
                }
            }
            
            if(numbers.Count == 0)
            {
                Console.WriteLine("There no number was found!");
                Console.Read();
                return;
            }

            var borrowHistory = new BorrowHistory(libraryCardNumber);
            foreach(var number in numbers)
            {
                var item = items[number-1];
                if(item == null)
                {
                    Console.WriteLine("There is no book map with number: " + number);
                    Console.WriteLine("Press any key to continue");
                    Console.Read();
                }
                else
                {
                    borrowHistory.BorrowHistoryDetails.Add(new BorrowHistoryDetail(item.Id, borrowHistory.Id));
                }
            }
            if(_borrowHistoryService.InsertBorrowHistory(borrowHistory))
            {
                Console.WriteLine("Borrow successfully");
                Console.WriteLine("Press any key to continue");
                Console.Read();
            }
            else
            {
                Console.WriteLine("Borrow faild");
                Console.WriteLine("Press any key to continue");
                Console.Read();
            }
        }
        
        void ReturnItems(string libraryCardNumber)
        {
            List<Item> items = new List<Item>();
            var borrowHistories = _borrowHistoryService.GetListBorrowHistoryByIdBorrower(libraryCardNumber).Where(x => x.BorrowHistoryDetails.Any(y => !y.HasReturn)).ToList();

            borrowHistories.ForEach(x => {
                items.AddRange(x.BorrowHistoryDetails.Where(y => !y.HasReturn).Select(y => y.Item));
            });
            
            if(items.Count == 0)
            {
                Console.WriteLine("you did not borrow any item");
                Console.WriteLine("Press any key to return");
                Console.Read();
                return;
            }

            // show item
            for(int i = 0; i < items.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + items[i].Title);
            }

            // Kiem tra cu phap so
            Console.Write("Enter item you want to return: ");
            string choice = Console.ReadLine();
            if(choice == "")
                return;

            string[] arg = choice.Split(",");

            if(arg.Length == 0)
            {
                Console.WriteLine("khong nhan dang duoc input");
            }

            List<int> numbers = new List<int>();
            foreach (string part in arg)
            {
                if (int.TryParse(part.Trim(), out int number))
                {
                    numbers.Add(number);
                }
            }
            
            if(numbers.Count == 0)
            {
                Console.WriteLine("There no number was found!");
                Console.Read();
                return;
            }
            
            List<Guid> IdItems = new();
            numbers.ForEach(x => IdItems.Add(items[x-1].Id));

            if(_itemService.ReturnItem(IdItems))
            {
                Console.WriteLine("Return item successfully!");
                Console.WriteLine("Press any key to go back");
                Console.Read();
                return;
            }
            else
            {
                Console.WriteLine("Errors");
                Console.WriteLine("Press any key to go back");
                Console.Read();
                return;
            }
        }
    
        void GetListBorrowHistoryByBorrowerId(string libaryCardNumber)
            {
                while(libaryCardNumber == null || libaryCardNumber == "")
                {
                    Console.Write("Please enter the number of library card: ");
                    libaryCardNumber = Console.ReadLine();
                }
                
                var borrowHistories = _borrowHistoryService.GetListBorrowHistoryByIdBorrower(libaryCardNumber);

                if(borrowHistories.Count == 0)
                {
                    Console.WriteLine("There is no borrower history with this library card number");
                    Console.WriteLine("Press any key to return");
                    Console.Read();
                    return;
                }
                
                foreach(var borrowHistory in borrowHistories)
                {
                    borrowHistory.GetInfo();
                    Console.WriteLine("Do you want to see the details? (y/n)");

                    if(Console.ReadLine() == "y")
                    {
                        borrowHistory.BorrowHistoryDetails.ToList().ForEach(x => {
                            x.GetInfo();
                        });

                        Console.WriteLine("Press any key to continue!!");
                        Console.Read();
                    }
                }
            }
    
        void GetListItemBorrowByBorrower(string libaryCardNumber, string name)
        {
            List<Item> items = new List<Item>();
            var borrowHistories = _borrowHistoryService.GetListBorrowHistoryByIdBorrower(libraryCardNumber)
            .Where(x => x.BorrowHistoryDetails.Any(y => !y.HasReturn)).ToList();

            if(borrowHistories.Count == 0)
            {
                Console.WriteLine("You didn't borrow any item");
                Console.WriteLine("Press any key to continue");
                Console.Read();
                return;
            }
            
            if(borrowHistories == null)
                Console.WriteLine("you didn't borrow yet");
            else
            {
                borrowHistories.ForEach(x => {
                    items.AddRange(x.BorrowHistoryDetails.Where(y => !y.HasReturn).Select(y => y.Item));
                });

                items = items.Where(x => x.Title.Contains(name)).ToList();

                if(items == null)
                    Console.WriteLine("There is no item with search");
                else
                    items.ForEach(x => x.GetInfo());
            }

            Console.WriteLine("Press any key to continue");
            Console.Read();
        }
    }

    private static void Main(string[] args)
    {
        Console.Clear();
        Login();
    }
}