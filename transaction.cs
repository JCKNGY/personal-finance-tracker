using System;

namespace PersonalFinanceTracker
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Note { get; set; }

        public Transaction(int id, string date, decimal amount, string type, string category, string note)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Type = type;
            Category = category;
            Note = note;
        }

        public Transaction() { }
    }
}