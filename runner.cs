using System;
using System.Collections.Generic;
using PersonalFinanceTracker;
using System.Text.Json;

namespace PersonalFinanceTracker
{
    class Runner
    {
        static void Main()
        {
            List<Transaction> monthly_transactions;
            try
            {
                string json = File.ReadAllText("sample_transactions.json");
                monthly_transactions = JsonSerializer.Deserialize<List<Transaction>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading transactions: {ex.Message}");
                monthly_transactions = new List<Transaction>();
            }
            
            bool isRunning = true;

            Console.WriteLine("\nWelcome to the monthly transaction tracker!");
            Console.WriteLine("1. Add a transaction");
            Console.WriteLine("2. Edit a transaction");
            Console.WriteLine("3. View all transactions");
            Console.WriteLine("4. Delete Transaction");
            Console.WriteLine("5. View transactions by date range");
            Console.WriteLine("6. Monthly Summary");
            Console.WriteLine("7. Category Summary");
            Console.WriteLine("8. Exit");
            while (isRunning)
            {
                
                Console.WriteLine("Please select an option (1-8):");

                switch (Console.ReadLine())
                {
                    case "1":
                        MethodStore.AddTransaction(monthly_transactions);
                        MethodStore.SaveTransactions(monthly_transactions);
                        break;
                    case "2":
                        MethodStore.EditTransaction(monthly_transactions);
                        MethodStore.SaveTransactions(monthly_transactions);
                        break;
                    case "3":
                        MethodStore.ViewTransactions(monthly_transactions);
                        break;
                    case "4":
                        MethodStore.DeleteTransaction(monthly_transactions);
                        MethodStore.SaveTransactions(monthly_transactions);
                        break;
                    case "5":
                        MethodStore.ViewTransactionsByDateRange(monthly_transactions);
                        break;
                    case "6":
                        MethodStore.MonthlySummary(monthly_transactions);
                        break;
                    case "7":
                        MethodStore.CategorySummary(monthly_transactions);
                        break;
                    case "8":
                        isRunning = false;
                        Console.WriteLine("Exiting the program. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}