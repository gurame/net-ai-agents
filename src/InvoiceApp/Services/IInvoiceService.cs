using InvoiceApp.Models;

namespace InvoiceApp.Services;

internal interface IInvoiceService
{
    Task<Invoice?> CreateAsync(Invoice invoice);
    Task<Invoice?> GetByIdAsync(int id);
    Task<IEnumerable<Invoice>> GetAllAsync();
    Task<bool> UpdateAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<Invoice?> GetByDescriptionAsync(string description);
}