using System;
using System.Globalization;
using System.IO;

namespace PersonalFinanceTracker
{
	// This file stores utility methods used by runner.cs
	public static class MethodStore
	{
		public static void AddTransaction(List<Transaction> transactions)
        {
            Console.WriteLine("Enter transaction description:");
            string description = Console.ReadLine();

            Console.WriteLine("Enter transaction amount:");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid amount. Please enter a valid decimal number:");
            }

            Console.WriteLine("Enter transaction date (yyyy-MM-dd):");
            DateTime date;
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format:");
            }

            transactions.Add(new Transaction(description, amount, date));
            Console.WriteLine("Transaction added successfully!");
        }
	}
}
