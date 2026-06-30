using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace PersonalFinanceTracker
{
    public static class MethodStore
    {
        public static void addTransaction(List<Transaction> transactions)
        {
            
            int id = transactions.Count > 0 ? transactions[transactions.Count - 1].Id + 1 : 1;
        
            Console.WriteLine("Enter transaction date (yyyy-MM-dd):");
            DateTime date;
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                Console.WriteLine("Invalid date format. Please enter yyyy-MM-dd:");
            }

            Console.WriteLine("Enter transaction amount:");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a positive number:");
            }

            Console.WriteLine("Enter transaction type (Income/Expense):");
            string type = Console.ReadLine() ?? "";

            Console.WriteLine("Enter transaction category:");
            string category = Console.ReadLine() ?? "";

            Console.WriteLine("Enter transaction note:");
            string note = Console.ReadLine() ?? "";

            transactions.Add(new Transaction(id, date.ToString("yyyy-MM-dd"), amount, type, category, note));
            Console.WriteLine("Transaction added successfully!");
        }
        public static void editTransaction(List<Transaction> transactions)
        {
            Console.WriteLine("Enter the ID (Placement ID) of the transaction to edit:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer:");
            }
            Transaction transactionToEdit = transactions.Find(t => t.Id == id);
            if (transactionToEdit == null)
            {
                Console.WriteLine($"No transaction found with ID {id}.");
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
                    transactions.Find(t => t.Id == id).Date = newDate.ToString("yyyy-MM-dd");
                    break;
                case "2":
                    Console.WriteLine("Enter new amount:");
                    decimal newAmount;
                    while (!decimal.TryParse(Console.ReadLine(), out newAmount) || newAmount <= 0)
                    {
                        Console.WriteLine("Invalid amount. Please enter a positive number:");
                    }
                    transactions.Find(t => t.Id == id).Amount = newAmount;
                    break;
                case "3":
                    Console.WriteLine("Enter new type (Income/Expense):");
                    string newType = Console.ReadLine() ?? "";
                    transactions.Find(t => t.Id == id).Type = newType;
                    break;
                case "4":
                    Console.WriteLine("Enter new category:");
                    string newCategory = Console.ReadLine() ?? "";
                    transactions.Find(t => t.Id == id).Category = newCategory;
                    break;
                case "5":
                    Console.WriteLine("Enter new note:");
                    string newNote = Console.ReadLine() ?? "";
                    transactions.Find(t => t.Id == id).Note = newNote;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
        }
        public static void viewTransactions(List<Transaction> transactions)
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            
            Console.WriteLine("What Category would you like to view? (Type 'All' to view all categories)");
            string categoryFilter = Console.ReadLine() ?? "";
            
            if (!categoryFilter.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                transactions = transactions.FindAll(t => t.Category.Equals(categoryFilter, StringComparison.OrdinalIgnoreCase));
                if (transactions.Count == 0)
                {
                    Console.WriteLine($"No transactions found for category '{categoryFilter}'.");
                    return;
                }
                else
                {
                    Console.WriteLine($"Displaying transactions for category '{categoryFilter}':");
                    foreach (Transaction transaction in transactions)
                    {
                        if(transaction.Category.Equals(categoryFilter, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"ID: {transaction.Id}, Date: {transaction.Date}, Amount: {transaction.Amount:C}, Type: {transaction.Type}, Category: {transaction.Category}, Note: {transaction.Note}");
                        }
                        else
                        {
                            Console.WriteLine($"No transactions found for category '{categoryFilter}'.");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Displaying all transactions:");
                foreach (Transaction transaction in transactions)
                {
                Console.WriteLine($"ID: {transaction.Id}, Date: {transaction.Date}, Amount: {transaction.Amount:C}, Type: {transaction.Type}, Category: {transaction.Category}, Note: {transaction.Note}");
                }
            }
            
        }
        public static void viewTransactionsByDateRange(List<Transaction> transactions)
        {
            if (transactions.Count == 0)
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
            foreach (Transaction transaction in transactions)
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

        public static void deleteTransaction(List<Transaction> transactions)
        {
            Console.WriteLine("Enter the ID of the transaction to delete:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer:");
            }

            if (transactions.RemoveAll(t => t.Id == id) > 0)
            {
                Console.WriteLine($"Transaction with ID {id} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"No transaction found with ID {id}.");
            }
        }

        public static void saveTransactions(List<Transaction> transactions)
        {
            Console.WriteLine("What path do you want to save the transactions to? (ex., C:\\Users\\YourUsername\\Documents\\transactions.json)");
            string filePath = Console.ReadLine();
            
            JsonSerializerOptions options = new JsonSerializerOptions 
            { 
                WriteIndented = true 
            };
            string json = JsonSerializer.Serialize(transactions, options);
            File.WriteAllText(filePath, json);
        }

        public static void monthlySummary(List<Transaction> transactions)
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            var grouped = transactions.GroupBy(t => t.Date.Substring(0, 7));
            
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

        public static void categorySummary(List<Transaction> transactions)
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            Dictionary<string, decimal> categoryTotals = new Dictionary<string, decimal>();

            foreach (Transaction transaction in transactions)
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