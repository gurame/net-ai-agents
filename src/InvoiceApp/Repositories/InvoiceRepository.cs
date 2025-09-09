using Dapper;
using InvoiceApp.Database;
using InvoiceApp.Models;

namespace InvoiceApp.Repositories;

internal class InvoiceRepository(IDbConnectionFactory dbConnectionFactory) : IInvoiceRepository
{
    public async Task<int> CreateAsync(Invoice invoice)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        const string sql = """
            INSERT INTO invoices (description, amount, invoice_date, due_date, is_paid)
            VALUES (@Description, @Amount, @InvoiceDate, @DueDate, @IsPaid);
    
            SELECT last_insert_rowid();
        """;
        var id = await connection.ExecuteScalarAsync<int>(sql, invoice);
        return id;
    }

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        const string sql = """
                           SELECT * FROM invoices WHERE id = @Id
                           """;
        return await connection.QuerySingleOrDefaultAsync<Invoice>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        const string sql = """
                           SELECT * FROM invoices
                           """;
        return await connection.QueryAsync<Invoice>(sql);
    }

    public async Task<bool> UpdateAsync(Invoice invoice)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        const string sql = """
            UPDATE invoices
            SET is_paid = @IsPaid
            WHERE id = @Id
        """;
        return await connection.ExecuteAsync(sql, invoice).ContinueWith(t => t.Result > 0);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        const string sql = """
                           DELETE FROM invoices WHERE id = @Id
                           """;
        return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
    }

    public async Task<Invoice?> GetByDescriptionAsync(string description)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        const string sql = """
                           SELECT * FROM invoices WHERE description = @Description
                           """;
        return await connection.QuerySingleOrDefaultAsync<Invoice>(sql, new { Description = description });
    }
}