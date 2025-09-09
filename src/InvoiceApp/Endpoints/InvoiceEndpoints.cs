using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Endpoints;

public static class InvoiceEndpoints
{
    public static void MapInvoiceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/invoices").WithTags("Invoices");
        
        group.MapGet("/", (IInvoiceService service) => service.GetAllAsync());
        group.MapGet("/{id:int}", (int id, IInvoiceService service) => service.GetByIdAsync(id));
        group.MapGet("/by-description", ([FromQuery]string description, IInvoiceService service) => service.GetByDescriptionAsync(description));
        group.MapPost("/", ([FromBody]Invoice invoice, IInvoiceService service) => service.CreateAsync(invoice));
        group.MapPut("/{id:int}/pay", (int id, IInvoiceService service) => service.UpdateAsync(id));
        group.MapDelete("/{id:int}", (int id, IInvoiceService service) => service.DeleteAsync(id));
    }
}