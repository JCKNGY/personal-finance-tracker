using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace PersonalFinanceTracker
{
    public static class MethodStore
    {
        public static void AddTransaction(List<Transaction> transactions)
        {
            Console.WriteLine("Enter ID:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer:");
            }

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

        public static void ViewTransactions(List<Transaction> transactions)
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }
            
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine($"ID: {transaction.Id}, Date: {transaction.Date}, Amount: {transaction.Amount:C}, Type: {transaction.Type}, Category: {transaction.Category}, Note: {transaction.Note}");
            }
        }

        public static void DeleteTransaction(List<Transaction> transactions)
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

        public static void SaveTransactions(List<Transaction> transactions)
        {
            JsonSerializerOptions options = new JsonSerializerOptions 
            { 
                WriteIndented = true 
            };
            string json = JsonSerializer.Serialize(transactions, options);
            File.WriteAllText("sample_transactions.json", json);
        }

        public static void MonthlySummary(List<Transaction> transactions)
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

        public static void CategorySummary(List<Transaction> transactions)
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