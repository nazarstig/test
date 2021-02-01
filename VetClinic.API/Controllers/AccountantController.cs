using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.BLL.Services.Realizations;
using System.Web;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountantController : ControllerBase
    {
        private readonly IFinancialReportService _reportService;

        public AccountantController(IFinancialReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport()
        {
            //var procedures = new List<PerformedProceduresReportModel> {
            //    new PerformedProceduresReportModel{ ProcedureName="Operation", Price = 2000, Count = 2},
            //    new PerformedProceduresReportModel{ ProcedureName="Examination of animal", Price = 50, Count = 3},
            //    new PerformedProceduresReportModel{ ProcedureName="Consultation", Price = 100, Count = 5},
            //};

            var procedures = _reportService.GetPerformedProcedures(new DateTime(2020, 1, 20));

            MonthReportModel model = new MonthReportModel { Month = "January", Year = "2020", Procedures = await procedures, Doctors = await _reportService.GetDoctors(), RentExpense = 900, AdvertisingExpense = 300, UtilitiesExpense = 200 };

            byte[] bin = await _reportService.SaveExcelReportFile(model);

            //clear the buffer stream
            Response.Headers.Clear();
            Response.Clear();
            //set the correct contenttype
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //set the correct length of the data being send
            Response.Headers.Add("content-length", bin.Length.ToString());
            //set the filename for the excel package
            Response.Headers.Add("content-disposition", "attachment; filename=\"MonthReport.xlsx\"");
            //send the byte array to the browser
            await Response.Body.WriteAsync(bin, 0, bin.Length);
            //cleanup
            await Response.CompleteAsync();

            return Ok();
        }
    }
}
