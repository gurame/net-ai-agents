using InvoiceApp.Models;
using InvoiceApp.Repositories;

namespace InvoiceApp.Services;

internal class InvoiceService(IInvoiceRepository repository) : IInvoiceService
{
    public async Task<Invoice?> CreateAsync(Invoice invoice)
    {
        var id = await repository.CreateAsync(invoice);
        return await repository.GetByIdAsync(id);
    }
    public Task<Invoice?> GetByIdAsync(int id)
    {
        return repository.GetByIdAsync(id);
    }
    public Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return repository.GetAllAsync();
    }
    public async Task<bool> UpdateAsync(int id)
    {
        var existingInvoice = await repository.GetByIdAsync(id);
        existingInvoice!.MarkAsPaid();
        
        return await repository.UpdateAsync(existingInvoice);
    }
    public Task<bool> DeleteAsync(int id)
    {
        return repository.DeleteAsync(id);
    }
    public Task<Invoice?> GetByDescriptionAsync(string description)
    {
        return repository.GetByDescriptionAsync(description);
    }
}