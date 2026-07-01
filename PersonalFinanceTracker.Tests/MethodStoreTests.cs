using Xunit;
using System;
using System.Collections.Generic;
using PersonalFinanceTracker;

public class MethodStoreTests
{
        [Fact]
        public void AddTransactionShouldIncreaseCount()
        {
            var Transactions = new List<Transaction>();
            MethodStore.AddTransaction(Transactions, "2026-06-01", 100m, "Income", "Salary", "Test");
    
            Assert.Single(Transactions);
        }

        [Fact]
        public void AddTransactionShouldThrowOnNegativeAmount()
        {
            var Transactions = new List<Transaction>();
            Assert.Throws<ArgumentException>(() => MethodStore.AddTransaction(Transactions, "2026-06-01", -100m, "Income", "Salary", "Test"));
        }
        [Fact]
        public void AddTransactionShouldThrowOnInvalidType()
        {
            var Transactions = new List<Transaction>();
            Assert.Throws<ArgumentException>(() => MethodStore.AddTransaction(Transactions, "2026-06-01", 100m, "InvalidType", "Salary", "Test"));
        }
        [Fact]
        public void AddTransactionShouldThrowOnEmptyCategory()
        {
            var Transactions = new List<Transaction>();
            Assert.Throws<ArgumentException>(() => MethodStore.AddTransaction(Transactions, "2026-06-01", 100m, "Income", "", "Test"));
        }
        [Fact]
        public void AddTransactionShouldThrowOnEmptyDate()
        {
            var Transactions = new List<Transaction>();
            Assert.Throws<ArgumentException>(() => MethodStore.AddTransaction(Transactions, "", 100m, "Income", "Salary", "Test"));
        }
        [Fact]
        public void AddTransactionShouldThrowOnEmptyNote()
        {
            var Transactions = new List<Transaction>();
            Assert.Throws<ArgumentException>(() => MethodStore.AddTransaction(Transactions, "2026-06-01", 100m, "Income", "Salary", ""));
        }
        [Fact]
        public void AddTransactionShouldThrowOnInvalidDateFormat()
        {
            var Transactions = new List<Transaction>();
            Assert.Throws<ArgumentException>(() => MethodStore.AddTransaction(Transactions, "06-01-2026", 100m, "Income", "Salary", "Test"));
        }
}