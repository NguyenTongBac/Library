using Model.Tables;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        List<Borrower> borrowers = new List<Borrower>()
            {
                new Borrower("Kin", "Dung Si Thanh Khe", "1"),
                new Borrower("Bac", "thien cam", "2")
            };
        List<BorrowingHistory> borrowingHistories = new List<BorrowingHistory>()
            {
                new BorrowingHistory(new Guid("b9287ba5-2ad8-4e65-849c-dee813ddb778"), "1", new DateTime(2022, 12, 22, 12, 00, 00)),
                new BorrowingHistory(new Guid("7e08c8ae-0fef-4bdc-9261-46206efad139"), "2", new DateTime(2022, 12, 22, 12, 00, 00))
            };
        List<BorrowingHistoryDetail> borrowingHistoryDetails = new List<BorrowingHistoryDetail>()
            {
                new BorrowingHistoryDetail(new Guid("38edfb21-0592-4159-81ee-c5199e25b2ba"), new Guid("9da10b15-3a9b-4fe0-9f70-131ee148dfcc"), new Guid("b9287ba5-2ad8-4e65-849c-dee813ddb778"), true),
                new BorrowingHistoryDetail(new Guid("6e3fbc60-30a8-444a-98e0-061d3e3cad36"), new Guid("96664792-a835-4c60-b8ba-763704154210"), new Guid("b9287ba5-2ad8-4e65-849c-dee813ddb778"), true),
                new BorrowingHistoryDetail(new Guid("e08d8be7-0ab6-4ac7-b811-9f92c3ec98f5"), new Guid("be2ab77c-322c-4a60-a9d5-0bcc70a44c22"), new Guid("7e08c8ae-0fef-4bdc-9261-46206efad139"), true),
                new BorrowingHistoryDetail(new Guid("8578ea9b-e1e5-4ccf-b4e4-0746f97b8117"), new Guid("9da10b15-3a9b-4fe0-9f70-131ee148dfcc"), new Guid("7e08c8ae-0fef-4bdc-9261-46206efad139"), true)
            };
        bool isLogin = false;
        List<Item> items = new List<Item>()
        {
            new Book(new Guid("9da10b15-3a9b-4fe0-9f70-131ee148dfcc"), "c# in depth", "Jon Skeet", Convert.ToDateTime("2015/10/20"), 528),
            new Book(new Guid("be2ab77c-322c-4a60-a9d5-0bcc70a44c22"), "Cake php", "Cake Software Foundation", Convert.ToDateTime("11/1/2023"), 943),
            new DVD(new Guid("96664792-a835-4c60-b8ba-763704154210"), "nhac tre", "xuan mai", Convert.ToDateTime("1991/1/1"), new TimeSpan(1,1,1)),
            new DVD(new Guid("93e22c7a-62bf-4c1e-ad8f-6855b0816cc5"), "nhac chu tinh", "bang kieu", Convert.ToDateTime("2001/12/20"), new TimeSpan(1,5,6)),
        };

        Console.Clear();
        Login();

        // Login
        void Login()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the library");
            Console.Write("Please enter your card number: ");
            string? libraryCardNumber = Console.ReadLine();

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
                    var borrower = borrowers.Find(x => x.LibraryCardNumber == libraryCardNumber);
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
        void AdminPage()
        {
            while (isLogin)
            {
                Console.Clear();
                Console.WriteLine("Welcome to admin page");
                Console.WriteLine("Select option: ");
                Console.WriteLine("0. Logout");
                Console.WriteLine("1. Insert your item into the library");
                Console.WriteLine("2. Update your item");
                Console.WriteLine("3. Delete your item");
                Console.WriteLine("4. Get item");
                Console.WriteLine("5. Search your items by name");
                Console.WriteLine("6. Show List information of borrowers with search");
                Console.WriteLine("7. Show history borrowed of borrower with details");
                Console.WriteLine("8. Show item borrowed");
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
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("--------Update item--------");
                        Console.Write("Please enter item id: ");
                        string? id = Console.ReadLine();
                        UpdateItemById(id);
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("--------Delete item--------");
                        Console.Write("Please enter the id of item to delete: ");
                        id = Console.ReadLine();
                        DeleteItemById(id);
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("--------Get item--------");
                        Console.Write("Please enter the id of item to get: ");
                        id = Console.ReadLine();
                        GetItem(id);
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("--------Get list item--------");
                        Console.Write("Please enter keyword to search the item: ");
                        string? keyword = Console.ReadLine();
                        GetListItemBySearch(keyword);
                        break;

                    case "6":
                        Console.Clear();
                        Console.Write("Please enter keyword name to search the borrower: ");
                        keyword = Console.ReadLine();
                        ShowListInforBorrowerWithSearch(keyword);
                        break;

                    case "7":
                        Console.Clear();
                        Console.Write("Please enter the libraryNumberCard: ");
                        GetBorrowingHistoryListByBorrower(Console.ReadLine());
                        break;

                    case "8":
                        Console.Clear();
                        Console.WriteLine("Please choice: ");
                        Console.WriteLine("1. Get all item borrowed");
                        Console.WriteLine("2. Get all item borrowed by borrower");
                        Console.WriteLine("Press any key to go back");
                        choice2 = Console.ReadLine();

                        switch (choice2)
                        {
                            case "1":
                                GetListItemBorrowed();
                                break;
                            case "2":
                                Console.Write("Please enter the libraryNumberCard of borrower: ");
                                string libraryNumberCard = Console.ReadLine();
                                GetListItemBorrowed(libraryNumberCard);
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        // borrow
        void CustomerPage(string libraryCardNumber)
        {
            while (isLogin)
            {
                Console.Clear();
                Console.WriteLine("Please enter your choice:");
                Console.WriteLine("0. Logout");
                Console.WriteLine("1. Borrow item from the library");
                Console.WriteLine("2. Return item to the library");
                Console.WriteLine("3. Search your items by name");
                Console.WriteLine("4. Show information about your account");
                Console.WriteLine("5. Show history borrowed with details of borrower");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        BorrowItems(libraryCardNumber);
                        break;

                    case "2":
                        Console.Clear();
                        ReturnItems(libraryCardNumber);
                        break;

                    case "0":
                        Console.Clear();
                        isLogin = false;
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("--------Get list item--------");
                        Console.Write("Please enter keyword to search the book: ");
                        string? keyword = Console.ReadLine();
                        GetListItemBySearch(keyword);
                        break;

                    case "4":
                        Console.Clear();
                        ShowInforOfBorrower(libraryCardNumber);
                        break;

                    case "5":
                        Console.Clear();
                        GetBorrowingHistoryListByBorrower(libraryCardNumber);
                        break;

                    default:
                        break;
                }
            }
        }

        // borrow service
        void BorrowItems(string libraryCardNumber)
        {
            var borrowingHistory = new BorrowingHistory(libraryCardNumber);
            var borrowingList = new List<BorrowingHistoryDetail>();

            Console.Write("Nhap so item can muon: ");
            var countItem = Console.ReadLine();
            int number;

            while(!int.TryParse(countItem, out number)){
                Console.Write("Please enter the number: ");
                countItem = Console.ReadLine();
            }

            for(int i = 1; i <= number; i++)
            {
                Console.Write("Nhap id Item " + i + ": ");
                string id = Console.ReadLine();
                var item = items.Find(x => x.Id.ToString() == id);

                if(item != null && item.IsBorrowed == false)
                {
                    var borrowingHistoryDetail = new BorrowingHistoryDetail(item.Id, borrowingHistory.Id);
                    item.IsBorrowed = true;
                    borrowingList.Add(borrowingHistoryDetail);
                }
                else
                {
                    Console.WriteLine("Item does not exist in library");
                    Console.WriteLine("Please try another item");
                    Console.WriteLine("Press enter to continue");
                    Console.Read();
                }
            }
            if(borrowingList.Count == 0){
                Console.WriteLine("Borrow fail!");
                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
            else
            {
                borrowingHistories.Add(borrowingHistory);
                borrowingHistoryDetails.AddRange(borrowingList);

                Console.WriteLine("Borrow successfully!");
                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
        }

        void ReturnItems(string libraryCardNumber)
        {
            Console.Write("Please input number item return: ");
            var numberItem = Console.ReadLine();
            int number;
            int countFails = 0;

            while(!int.TryParse(numberItem, out number)){
                Console.Write("Please enter the number: ");
                numberItem = Console.ReadLine();
            }

            for(int i = 1; i <= number; i++)
            {
                Console.Write("Enter id of item " + i + " to return: ");
                string id = Console.ReadLine();
                var item = items.Find(x => x.Id.ToString() == id && x.IsBorrowed == true);
                if(item != null && item.IsBorrowed == true)
                {
                    item.IsBorrowed = false;
                    var borrowing = borrowingHistoryDetails.Find(x => x.ItemId == item.Id && x.HasReturn == false);
                    borrowing.HasReturn = true;
                }
                else
                {
                    Console.WriteLine("Library don't have this item");
                    Console.WriteLine("Please enter to continue");
                    countFails++;
                    Console.Read();
                }
            }
            if(countFails == number){
                Console.WriteLine("Borrow fail!");
                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
            else
            {
                Console.WriteLine("Return successfully!");
                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
        }

        // History borrow service
        // 
        void GetListBorrowingHistoryById(string borrowerId)
        {
            var borrower = borrowers.Find(x=> x.LibraryCardNumber == borrowerId);
            var listBorrowHistory = borrowingHistories.FindAll(x => x.BorrowerId == borrowerId);

            Console.WriteLine("List Borrowing History of customer");
            Console.WriteLine(borrower.Name);
            foreach(var borrowHistory in listBorrowHistory)
            {
                
            }
        }

        void ShowInforOfBorrower(string libraryCardNumber)
        {
            var borrower = borrowers.Find(x => x.LibraryCardNumber == libraryCardNumber);
            borrower.GetInfo();
            Console.WriteLine("Press enter to go back");
            Console.Read();
        }

        void ShowListInforBorrowerWithSearch(string keyword)
        {
            var listSearchBorrower = borrowers.FindAll(x => x.Name.ToLower().Contains(keyword));

            if(listSearchBorrower.Count == 0)
            {
                Console.WriteLine("There are no borrower as search!!!");
                Console.WriteLine("Press any key to go back");
                Console.ReadKey();
            }
            else
            {
                foreach(var borrower in listSearchBorrower)
                {
                        borrower.GetInfo();
                        Console.WriteLine("Press enter to countinue");
                        Console.Read();
                }

                Console.WriteLine("End List!!!");
                Console.WriteLine("Press enter to go back");
                Console.Read();
            }
        }

        void GetBorrowingHistoryById(Guid id)
        {
            var borrowHistory = borrowingHistories.Find(x => x.Id == id);
            var borrowHistoryDetails = borrowingHistoryDetails.FindAll(x => x.BorrowingHistoryId == borrowHistory.Id);
            
            borrowHistory.Borrower = borrowers.Find(x => x.LibraryCardNumber == borrowHistory.BorrowerId);
            borrowHistoryDetails.ForEach(x => x.Item = items.Find(y => y.Id == x.ItemId));
            borrowHistory.BorrowingHistoryDetails = borrowHistoryDetails;
            borrowHistory.GetInfo();

            Console.WriteLine("Do you want see the details? Press y");
            var answer = Console.ReadLine();

            if(answer == "y")
            {
                foreach(var borrowHistoryDetail in borrowHistoryDetails)
                {
                    borrowHistoryDetail.GetInfo();
                    Console.WriteLine("Press enter to countinue");
                    Console.Read();
                }
                Console.WriteLine("End List Details!!!");
            }
        }
    
        void GetBorrowingHistoryListByBorrower(string libraryCardNumber)
        {
            while(libraryCardNumber == "")
            {
                Console.Write("Please enter a library card number: ");
                libraryCardNumber = Console.ReadLine();
            }

            var borrowingHistoriesOfBorrower = borrowingHistories.FindAll(x => x.BorrowerId == libraryCardNumber);

            if(borrowingHistoriesOfBorrower.Count != 0)
            {
                foreach(var borrowHistory in borrowingHistoriesOfBorrower)
                {
                    GetBorrowingHistoryById(borrowHistory.Id);
                    Console.WriteLine("Press enter to countinue");
                    Console.Read();
                }

                Console.WriteLine("End List borrow History!!!");
                Console.WriteLine("Press enter to countinue");
                Console.Read();
            }
            
        }

        // service item
        void GetListItemBySearch(string keyword)
        {
            var listSearchItem = items.FindAll(x => x.Title.Contains(keyword));
            if(listSearchItem.Count == 0)
            {
                Console.WriteLine("There are no item as search!!!");
                Console.WriteLine("Press enter to go back");
                Console.Read();
            }
            else
            {
                foreach (var item in listSearchItem)
                {
                    item.GetInfo();

                    Console.WriteLine("Press enter to countinue...");
                    Console.Read();
                }
                Console.WriteLine("End of list!");
                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
        }

        void GetItem(string id)
        {
            var item = items.Find(x => x.Id.ToString() == id);

            if (item == null)
            {
                Console.WriteLine("There is no book with the id: " + id);
                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
            else
            {
                item.GetInfo();

                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
        }

        void UpdateItemById(string id)
        {
            var item = items.Find(x => x.Id.ToString() == id);

            if (item == null)
            {
                Console.WriteLine("There is no book with the id: " + id);
                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
            else
            {
                item.Update();

                Console.WriteLine("Updated item successfully!");
                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
        }

        void DeleteItemById(string id)
        {
            var item = items.Find(x => x.Id.ToString() == id);

            if (item == null)
            {
                Console.WriteLine("There is no book with the id: " + id);
                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
            else
            {
                items.Remove(item);

                Console.WriteLine("Removed book successfully!");
                Console.WriteLine("Press enter to go back...");
                Console.Read();
            }
        }

        void InsertBook()
        {
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
            items.Add(insertBook);

            Console.WriteLine("Insert successfully");
            Console.WriteLine("Press enter to go back...");
            Console.Read();
        }

        void InsertDVD()
        {
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
            items.Add(dVD);

            Console.WriteLine("Insert successfully");
            Console.WriteLine("Press enter to go back...");
            Console.Read();
        }
    
        void GetListItemBorrowed(string libraryCardNumber = null)
        {
            if (libraryCardNumber == null)
                libraryCardNumber = "";
            var borrowerSearch = borrowers.FindAll(x => x.LibraryCardNumber.Contains(libraryCardNumber));
            if(borrowerSearch.Count == 0)
            {
                Console.WriteLine("There are no borrower with libary card :" + libraryCardNumber);
                Console.WriteLine("Press any key to go back");
                Console.Read();
                return;
            }
            
            foreach(var borrower in borrowerSearch)
            {
                var borrowHistories = borrowingHistories.FindAll(x => x.BorrowerId == borrower.LibraryCardNumber);
                Console.WriteLine("Borrower name: " + borrower.Name);
                if(borrowHistories.Count == 0)
                {
                    Console.WriteLine("There are no item borrowing by this user");
                }
                else
                {
                    foreach (var borrowHistory in borrowHistories)
                    {
                        var borrowedHistoryDetails = borrowingHistoryDetails.FindAll(x => x.BorrowingHistoryId == borrowHistory.Id && x.HasReturn == false);
                        
                        if(borrowedHistoryDetails.Count == 0)
                        {
                            Console.WriteLine("There are no item borrowing by this user");
                        }
                        else
                        {
                            foreach(var borrowedHistoryDetail in borrowedHistoryDetails)
                            {
                                var item = items.Find(x => x.Id == borrowedHistoryDetail.ItemId);
                                Console.WriteLine("----------------------------------------------------------------");
                                Console.WriteLine("Item title: " + item.Title);
                                Console.WriteLine("Date borrow: " + borrowHistory.BorrowDate);
                                Console.WriteLine("is return: " + borrowedHistoryDetail.HasReturn);
                            }
                        }
                    }
                }    
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}