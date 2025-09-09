using Dapper;

namespace InvoiceApp.Database;
internal class DbInitializer(IDbConnectionFactory dbConnectionFactory)
{
    public async Task InitializeAsync()
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS invoices (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            description TEXT NOT NULL,
            amount REAL NOT NULL CHECK(amount >= 0),
            invoice_date TEXT NOT NULL,
            due_date TEXT NOT NULL,
            is_paid INTEGER NOT NULL CHECK(is_paid IN (0,1))
            );
        """);
    }
}