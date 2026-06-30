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
            List<Transaction> monthlyTransactions;
            try
            {
                string json = File.ReadAllText("sample_transactions.json");
                monthlyTransactions = JsonSerializer.Deserialize<List<Transaction>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading transactions: {ex.Message}");
                monthlyTransactions = new List<Transaction>();
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
                        MethodStore.addTransaction(monthlyTransactions);
                        MethodStore.saveTransactions(monthlyTransactions);
                        break;
                    case "2":
                        MethodStore.editTransaction(monthlyTransactions);
                        MethodStore.saveTransactions(monthlyTransactions);
                        break;
                    case "3":
                        MethodStore.viewTransactions(monthlyTransactions);
                        break;
                    case "4":
                        MethodStore.deleteTransaction(monthlyTransactions);
                        MethodStore.saveTransactions(monthlyTransactions);
                        break;
                    case "5":
                        MethodStore.viewTransactionsByDateRange(monthlyTransactions);
                        break;
                    case "6":
                        MethodStore.monthlySummary(monthlyTransactions);
                        break;
                    case "7":
                        MethodStore.categorySummary(monthlyTransactions);
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