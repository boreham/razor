using ExportPdf.Models;
using Microsoft.EntityFrameworkCore;

namespace ExportPdf.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<Customer> Customers { get; set; }
}
