namespace MyFinanceBlazor.UI.Models
{
    public class Expense
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public DateTime ExpenseDate { get; set; }

        public decimal Cost { get; set; }
    }
}
