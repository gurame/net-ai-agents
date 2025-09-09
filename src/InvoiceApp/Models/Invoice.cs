namespace InvoiceApp.Models;

internal class Invoice
{
    public int Id { get; init; }
    public required string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }
    
    public void MarkAsPaid()
    {
        IsPaid = true;
    }
}