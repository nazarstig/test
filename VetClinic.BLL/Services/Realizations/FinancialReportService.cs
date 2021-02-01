using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.BLL.Services.Realizations
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public FinancialReportService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<byte[]> SaveExcelReportFile(MonthReportModel model)
        {           
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add($"{model.Month} , {model.Year}");

                workSheet.Cells.Style.Font.Size = 14;

                workSheet.Cells["A1"].Value = "VETERINARY CLINIC";
                workSheet.Cells["A1:D1"].Merge = true;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                workSheet.Cells["A2"].Value = "Income statement";
                workSheet.Cells["A2:D2"].Merge = true;
                workSheet.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                workSheet.Cells["A3"].Value = $"For the month ended {model.Month} , {model.Year}";
                workSheet.Cells["A3:D3"].Merge = true;
                workSheet.Row(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["A3:D3"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                workSheet.Cells["A4"].Value = "Revenues";
                workSheet.Cells["A4:D4"].Merge = true;
                workSheet.Row(4).Style.Font.Bold = true;

                int proceduresCount = 0;
                if (model.Procedures != null && model.Procedures.Count > 0)
                {
                    for (int i = 1; i <= model.Procedures.Count; i++)
                    {
                        workSheet.Cells["B" + (4 + i).ToString()].Value = $"{model.Procedures[i - 1].ProcedureName}: ({model.Procedures[i - 1].Count})";
                        workSheet.Cells["C" + (4 + i).ToString()].Value = model.Procedures[i - 1].Price * model.Procedures[i - 1].Count;

                        proceduresCount++;
                    }
                }

                workSheet.Cells["C" + (5 + proceduresCount).ToString()].Value = "Service revenue";
                if (proceduresCount > 0)
                {
                    workSheet.Cells["D" + (5 + proceduresCount).ToString()].Formula = "=SUM(C5:C" + (5 + proceduresCount - 1).ToString() + ")";
                }
                else
                {
                    workSheet.Cells["D" + (5 + proceduresCount).ToString()].Value = 0;
                }
                workSheet.Cells["D" + (5 + proceduresCount).ToString()].Style.Font.Bold = true;

                workSheet.Cells["A" + (6 + proceduresCount).ToString()].Value = "Expenses";
                workSheet.Cells["A" + (6 + proceduresCount).ToString() + ":D" + (6 + proceduresCount).ToString()].Merge = true;
                workSheet.Row(6 + proceduresCount).Style.Font.Bold = true;

                int employeeCount = 0;
                if (model.Doctors != null && model.Doctors.Count > 0)
                {
                    for (int i = 1; i <= model.Doctors.Count; i++)
                    {
                        workSheet.Cells["B" + (6 + (proceduresCount + i)).ToString()].Value = $"{model.Doctors[i - 1].FirstName} {model.Doctors[i - 1].LastName}";
                        workSheet.Cells["C" + (6 + (proceduresCount + i)).ToString()].Value = model.Doctors[i - 1].Salary;

                        employeeCount++;
                    }
                }

                workSheet.Cells["B" + (7 + proceduresCount + employeeCount).ToString()].Value = "Salaries expense";
                if (employeeCount > 0)
                {
                    workSheet.Cells["C" + (7 + proceduresCount + employeeCount).ToString()].Formula = "=SUM(C" + (7 + proceduresCount).ToString() + ":C" + (6 + proceduresCount + employeeCount).ToString() + ")";
                }
                else
                {
                    workSheet.Cells["C" + (7 + proceduresCount + employeeCount).ToString()].Value = 0;
                }
                workSheet.Cells["C" + (7 + proceduresCount + employeeCount).ToString()].Style.Font.Bold = true;

                int totalCount = proceduresCount + employeeCount;

                workSheet.Cells["B" + (8 + totalCount).ToString()].Value = "Rent expense";
                workSheet.Cells["C" + (8 + totalCount).ToString()].Value = model.RentExpense != null ? model.RentExpense : 0;
                workSheet.Cells["C" + (8 + totalCount).ToString()].Style.Font.Bold = true;

                workSheet.Cells["B" + (9 + totalCount).ToString()].Value = "Advertising expense";
                workSheet.Cells["C" + (9 + totalCount).ToString()].Value = model.AdvertisingExpense != null ? model.AdvertisingExpense : 0;
                workSheet.Cells["C" + (9 + totalCount).ToString()].Style.Font.Bold = true;

                workSheet.Cells["B" + (10 + totalCount).ToString()].Value = "Utilities expense";
                workSheet.Cells["C" + (10 + totalCount).ToString()].Value = model.UtilitiesExpense != null ? model.UtilitiesExpense : 0;
                workSheet.Cells["C" + (10 + totalCount).ToString()].Style.Font.Bold = true;

                workSheet.Cells["C" + (11 + totalCount).ToString()].Value = "Total expense";
                workSheet.Cells["D" + (11 + totalCount).ToString()].Formula = "=SUM(C" + (7 + totalCount).ToString() + ":C" + (10 + totalCount).ToString() + ")";
                workSheet.Cells["D" + (11 + totalCount).ToString()].Style.Font.Bold = true;

                workSheet.Cells["A" + (12 + totalCount).ToString()].Value = "Net income";
                workSheet.Cells["A" + (12 + totalCount).ToString() + ":" + "C" + (12 + totalCount).ToString()].Merge = true;
                workSheet.Cells["A" + (12 + totalCount).ToString()].Style.Font.Bold = true;
                workSheet.Cells["D" + (12 + totalCount).ToString()].Formula = "=D" + (5 + proceduresCount).ToString() + "-D" + (11 + totalCount).ToString();
                workSheet.Cells["D" + (12 + totalCount).ToString()].Style.Font.Bold = true;
                workSheet.Cells["D" + (12 + totalCount).ToString()].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                ExcelRange rg = workSheet.Cells[1, 1, workSheet.Dimension.End.Row, workSheet.Dimension.End.Column];
                rg.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);

                double minimumSize = 12;
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize);

                return await package.GetAsByteArrayAsync();
            }
        }


        public async Task<List<DoctorReportModel>> GetDoctors()
        {
            var doctors = await _repositoryWrapper.DoctorRepository.GetAsync(include: doc => doc.Include(d => d.User).Include(d => d.Position));

            var doctorsReportModels = doctors.Select(u => new DoctorReportModel
                        {
                             FirstName = u.User.FirstName,
                             LastName = u.User.LastName,
                             Salary = u.Position.Salary
                        }).ToList();

            return doctorsReportModels;
        }

        public async Task<List<PerformedProceduresReportModel>> GetPerformedProcedures(DateTime date)
        {
            var filteredAppProc = await _repositoryWrapper.AppointmentProceduresRepository
                .GetAsync(include: app => app.Include(a => a.Appointment)
                .Include(a => a.Procedure),filter: app => app.Appointment.AppointmentDate.Month == date.Month && app.Appointment.StatusId == 4);

            var result = filteredAppProc.GroupBy(u => u.Procedure)
                .Select(g => new PerformedProceduresReportModel { ProcedureName = g.Key.ProcedureName, Price = g.Key.Price, Count = g.Count()}).ToList();

            //var filteredAppointments = await _repositoryWrapper.AppointmentRepository
            //    .GetAsync(filter: app => app.AppointmentDate.Month == date.Month && app.StatusId == 4,
            //    include: app => app.Include(a => a.AppointmentProcedures).ThenInclude(ap => ap.Procedure));
            //var procedures = await _repositoryWrapper.ProcedureRepository.GetAsync(include: proc => proc.Include(p => p.AppointmentProcedures).ThenInclude( ap => ap.Appointment));

            //var res = filteredAppointments.GroupBy(u => u.AppointmentProcedures).Select(g => g.Key);

            //var result = filteredAppointments.Select(u => u.AppointmentProcedures
            //            .GroupBy(ap => ap.Procedure)).Select(g => new PerformedProceduresReportModel {ProcedureName = g. })
            return result;
        }

    }

    public class MonthReportModel
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public List<PerformedProceduresReportModel> Procedures { get; set; }
        public List<DoctorReportModel> Doctors { get; set; }
        public decimal? RentExpense { get; set; }
        public decimal? AdvertisingExpense { get; set; }
        public decimal? UtilitiesExpense { get; set; }
    }

    public class DoctorReportModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
    }

    public class PerformedProceduresReportModel
    {
        public string ProcedureName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }


}
