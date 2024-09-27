using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using ExportPdf.Data;
using ExportPdf.Models;

namespace ExportPdf.Pages;

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
        List<object> customers = (from customer in _dataContext.Customers.Take(10)
                                  select new[] {
                                  customer.CustomerID,
                                  customer.ContactName,
                                  customer.City,
                                  customer.Country
                             }).ToList<object>();

        //Building an HTML string.
        StringBuilder sb = new StringBuilder();

        //Table start.
        sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>");

        //Building the Header row.
        sb.Append("<tr>");
        sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>CustomerID</th>");
        sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>ContactName</th>");
        sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>City</th>");
        sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Country</th>");
        sb.Append("</tr>");

        //Building the Data rows.
        for (int i = 0; i < customers.Count; i++)
        {
            string[] customer = (string[])customers[i];
            sb.Append("<tr>");
            for (int j = 0; j < customer.Length; j++)
            {
                //Append data.
                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(customer[j]);
                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }

        //Table end.
        sb.Append("</table>");

        using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(sb.ToString())))
        {
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            PdfWriter writer = new PdfWriter(byteArrayOutputStream);
            PdfDocument pdfDocument = new PdfDocument(writer);
            pdfDocument.SetDefaultPageSize(PageSize.A4);
            HtmlConverter.ConvertToPdf(stream, pdfDocument);
            pdfDocument.Close();
            return File(byteArrayOutputStream.ToArray(), "application/pdf", "Grid.pdf");
        }
    }
}