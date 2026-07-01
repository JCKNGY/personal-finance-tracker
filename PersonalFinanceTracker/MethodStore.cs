using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace PersonalFinanceTracker
{
    public static class MethodStore
    {
        // Testable logic
        public static void AddTransaction(List<Transaction> transactions, string date, decimal amount, string type, string category, string note)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");
            int Id = transactions.Count > 0 ? transactions[transactions.Count - 1].Id + 1 : 1;
            transactions.Add(new Transaction(Id, date, amount, type, category, note));
        }

// Console wrapper
    public static void AddTransactionFromConsole(List<Transaction> transactions)
        {
            Console.WriteLine("Enter transaction date (yyyy-MM-dd):");
            DateTime Date;
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out Date))
            {
                Console.WriteLine("Invalid date format. Please enter yyyy-MM-dd:");
            }

            Console.WriteLine("Enter transaction amount:");
            decimal Amount;
            while (!decimal.TryParse(Console.ReadLine(), out Amount) || Amount <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a positive number:");
            }       

            Console.WriteLine("Enter transaction type (Income/Expense):");
            string Type = Console.ReadLine() ?? "";

            Console.WriteLine("Enter transaction category:");
            string Category = Console.ReadLine() ?? "";

            Console.WriteLine("Enter transaction note:");
            string note = Console.ReadLine() ?? "";

            AddTransaction(transactions, Date.ToString("yyyy-MM-dd"), Amount, Type, Category, note);
        }
        public static void EditTransaction(List<Transaction> Transactions)
        {
            Console.WriteLine("Enter the ID (Placement ID) of the transaction to edit:");
            int Id;
            while (!int.TryParse(Console.ReadLine(), out Id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer:");
            }
            Transaction transactionToEdit = Transactions.Find(t => t.Id == Id);
            if (transactionToEdit == null)
            {
                Console.WriteLine($"No transaction found with ID {Id}.");
                return;
            }
            Console.WriteLine("What would you like to edit? (1. Date, 2. Amount, 3. Type, 4. Category, 5. Note)");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter new date (yyyy-MM-dd):");
                    DateTime newDate;
                    while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out newDate))
                    {
                        Console.WriteLine("Invalid date format. Please enter yyyy-MM-dd:");
                    }
                    Transactions.Find(t => t.Id == Id).Date = newDate.ToString("yyyy-MM-dd");
                    break;
                case "2":
                    Console.WriteLine("Enter new amount:");
                    decimal newAmount;
                    while (!decimal.TryParse(Console.ReadLine(), out newAmount) || newAmount <= 0)
                    {
                        Console.WriteLine("Invalid amount. Please enter a positive number:");
                    }
                    Transactions.Find(t => t.Id == Id).Amount = newAmount;
                    break;
                case "3":
                    Console.WriteLine("Enter new type (Income/Expense):");
                    string newType = Console.ReadLine() ?? "";
                    Transactions.Find(t => t.Id == Id).Type = newType;
                    break;
                case "4":
                    Console.WriteLine("Enter new category:");
                    string newCategory = Console.ReadLine() ?? "";
                    Transactions.Find(t => t.Id == Id).Category = newCategory;
                    break;
                case "5":
                    Console.WriteLine("Enter new note:");
                    string newNote = Console.ReadLine() ?? "";
                    Transactions.Find(t => t.Id == Id).Note = newNote;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
        }
        public static void ViewTransactions(List<Transaction> Transactions)
        {
            if (Transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            
            Console.WriteLine("What Category would you like to view? (Type 'All' to view all categories)");
            string CategoryFilter = Console.ReadLine() ?? "";
            
            if (!CategoryFilter.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                Transactions = Transactions.FindAll(t => t.Category.Equals(CategoryFilter, StringComparison.OrdinalIgnoreCase));
                if (Transactions.Count == 0)
                {
                    Console.WriteLine($"No transactions found for category '{CategoryFilter}'.");
                    return;
                }
                else
                {
                    Console.WriteLine($"Displaying transactions for category '{CategoryFilter}':");
                    foreach (Transaction transaction in Transactions)
                    {
                        if(transaction.Category.Equals(CategoryFilter, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"ID: {transaction.Id}, Date: {transaction.Date}, Amount: {transaction.Amount:C}, Type: {transaction.Type}, Category: {transaction.Category}, Note: {transaction.Note}");
                        }
                        else
                        {
                            Console.WriteLine($"No transactions found for category '{CategoryFilter}'.");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Displaying all transactions:");
                foreach (Transaction transaction in Transactions)
                {
                Console.WriteLine($"ID: {transaction.Id}, Date: {transaction.Date}, Amount: {transaction.Amount:C}, Type: {transaction.Type}, Category: {transaction.Category}, Note: {transaction.Note}");
                }
            }
            
        }
        public static void ViewTransactionsByDateRange(List<Transaction> Transactions)
        {
            if (Transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }   
 
            Console.WriteLine("Enter start date (yyyy-MM-dd):");
            DateTime startDate;
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                Console.WriteLine("Invalid date format. Please enter yyyy-MM-dd:");
            }
 
            Console.WriteLine("Enter end date (yyyy-MM-dd):");
            DateTime endDate;
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                Console.WriteLine("Invalid date format. Please enter yyyy-MM-dd:");
            }
 
            if (startDate > endDate)
            {
                Console.WriteLine("Start date cannot be after end date. Please try again.");
                return;
            }
 
            List<Transaction> filteredTransactions = new List<Transaction>();
            foreach (Transaction transaction in Transactions)
            {
                if (DateTime.TryParseExact(transaction.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime transactionDate))
                {
                    if (transactionDate >= startDate && transactionDate <= endDate)
            {
                filteredTransactions.Add(transaction);
            }
            }
        }
 
            if (filteredTransactions.Count == 0)
            {
                Console.WriteLine($"No transactions found between {startDate:yyyy-MM-dd} and {endDate:yyyy-MM-dd}.");
                return;
            }
 
            Console.WriteLine($"Displaying transactions between {startDate:yyyy-MM-dd} and {endDate:yyyy-MM-dd}:");
            foreach (Transaction transaction in filteredTransactions)
            {
                Console.WriteLine($"ID: {transaction.Id}, Date: {transaction.Date}, Amount: {transaction.Amount:C}, Type: {transaction.Type}, Category: {transaction.Category}, Note: {transaction.Note}");
            }
        }

        public static void DeleteTransaction(List<Transaction> Transactions)
        {
            Console.WriteLine("Enter the ID of the transaction to delete:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer:");
            }

            if (Transactions.RemoveAll(t => t.Id == id) > 0)
            {
                Console.WriteLine($"Transaction with ID {id} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"No transaction found with ID {id}.");
            }
        }

        public static void SaveTransactions(List<Transaction> Transactions)
        {
            Console.WriteLine("What path do you want to save the transactions to? (ex., C:\\Users\\YourUsername\\Documents\\transactions.json)");
            string filePath = Console.ReadLine() ?? "sample_transactions.json";
            
            JsonSerializerOptions options = new JsonSerializerOptions 
            { 
                WriteIndented = true 
            };
            string json = JsonSerializer.Serialize(Transactions, options);
            File.WriteAllText(filePath, json);
        }

        public static void MonthlySummary(List<Transaction> Transactions)
        {
            if (Transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            var grouped = Transactions.GroupBy(t => t.Date.Substring(0, 7));
            
            foreach (var group in grouped)
            {
                decimal income = 0;
                decimal expense = 0;

                foreach (var transaction in group)
                {
                    if (transaction.Type.Equals("Income", StringComparison.OrdinalIgnoreCase))
                    {
                        income += transaction.Amount;
                    }
                    else if (transaction.Type.Equals("Expense", StringComparison.OrdinalIgnoreCase))
                    {
                        expense += transaction.Amount;
                    }
                }

                Console.WriteLine($"\n{group.Key}:");
                Console.WriteLine($"  Income: {income:C}");
                Console.WriteLine($"  Expense: {expense:C}");
                Console.WriteLine($"  Net: {(income - expense):C}");
            }
        }

        public static void CategorySummary(List<Transaction> Transactions)
        {
            if (Transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            Dictionary<string, decimal> categoryTotals = new Dictionary<string, decimal>();

            foreach (Transaction transaction in Transactions)
            {
                if (!categoryTotals.ContainsKey(transaction.Category))
                {
                    categoryTotals[transaction.Category] = 0;
                }

                categoryTotals[transaction.Category] += transaction.Amount;
            }

            Console.WriteLine("\nCategory Summary:");
            foreach (var category in categoryTotals)
            {
                Console.WriteLine($"  {category.Key}: {category.Value:C}");
            }
        }
    }
}