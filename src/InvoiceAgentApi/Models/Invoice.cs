namespace InvoiceAgentApi.Models;

public class Invoice
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }
}
