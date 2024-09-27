using ExportXls.Data;
using ExportXls.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace ExportXls.Pages;

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

    public FileResult OnPostExport(string GridHtml)
    {
        return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Grid.xls");
    }
}