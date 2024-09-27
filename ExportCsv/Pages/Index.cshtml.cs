using ExportCsv.Data;
using ExportCsv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text;

namespace ExportCsv.Pages;

public class IndexModel : PageModel
{
    private readonly DataContext _dataContext;

    public IndexModel(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<Customer> Customers { get; set; }

    public void OnGet()
    {
        Customers = _dataContext.Customers.Take(10).ToList();
    }

    public FileResult OnPostExport()
    {
        string[] columnNames = new string[] { "CustomerId", "ContactName", "City", "Country" };
        var customers = _dataContext.Customers.Take(10).ToList();

        //Build the CSV file data as a Comma separated string.
        string csv = string.Empty;

        foreach (string columnName in columnNames)
        {
            //Add the Header row for CSV file.
            csv += columnName + ',';
        }

        //Add new line.
        csv += "\r\n";

        foreach (var customer in customers)
        {
            //Add the Data rows.
            csv += customer.CustomerID.Replace(",", ";") + ',';
            csv += customer.ContactName.Replace(",", ";") + ',';
            csv += customer.City.Replace(",", ";") + ',';
            csv += customer.Country.Replace(",", ";") + ',';

            //Add new line.
            csv += "\r\n";
        }

        //Download the CSV file.
        byte[] bytes = Encoding.ASCII.GetBytes(csv);
        return File(bytes, "text/csv", "Grid.csv");
    }
}