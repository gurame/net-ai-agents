using InvoiceApp.Models;

namespace InvoiceApp.Repositories;

internal interface IInvoiceRepository
{
    Task<int> CreateAsync(Invoice invoice);
    Task<Invoice?> GetByIdAsync(int id);
    Task<IEnumerable<Invoice>> GetAllAsync();
    Task<bool> UpdateAsync(Invoice invoice);
    Task<bool> DeleteAsync(int id);
    Task<Invoice?> GetByDescriptionAsync(string description);
}