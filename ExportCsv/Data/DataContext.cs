using ExportCsv.Models;
using Microsoft.EntityFrameworkCore;

namespace ExportCsv.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<Customer> Customers { get; set; }
}
