using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Models;
namespace WebApplication1.Controllers;
using ClosedXML.Excel;
using System.Linq;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Context _context;

    public HomeController(ILogger<HomeController> logger, Context context)
    {
        _logger = logger;
        _context = context;

    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var dataInformation = await _context.Information.ToListAsync();

            return View(dataInformation);
    }

    public async Task<FileResult> GenerateXLSXAsync()
    {

        List<Information> dataInformationDB = await _context.Information.ToListAsync();

        var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Sheet1");

        foreach (var data in dataInformationDB)
        {
            worksheet.Cell(1, 1).Value = "Nombre";
            worksheet.Cell(1, 2).Value = "Email";
            worksheet.Cell(1, 3).Value = "Telefono";
            worksheet.Cell(1, 4).Value = "Estatus";
            worksheet.Cell(1, 5).Value = "Aseguradora";
            worksheet.Cell(1, 6).Value = "Detalles";

            worksheet.Cell(2, 1).Value = data.Name;
            worksheet.Cell(2, 2).Value = data.Email;
            worksheet.Cell(2, 3).Value = data.Phone;
            worksheet.Cell(2, 4).Value = data.Status;
            worksheet.Cell(2, 5).Value = data.Insurance;
            worksheet.Cell(2, 6).Value = data.Detail;
        }

            

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "document.xlsx");
            } 
    }
}