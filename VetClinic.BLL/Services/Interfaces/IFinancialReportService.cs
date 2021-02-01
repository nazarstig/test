using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Realizations;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IFinancialReportService
    {
        public Task<List<DoctorReportModel>> GetDoctors();

        public Task<byte[]> SaveExcelReportFile(MonthReportModel model);

        public Task<List<PerformedProceduresReportModel>> GetPerformedProcedures(DateTime date);
    }
}
