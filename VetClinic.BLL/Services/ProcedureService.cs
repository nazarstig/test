
using System.Collections.Generic;
using VetClinic.DAL.Repositories.Interfaces;
using VetClinic.DAL.Repositories.Realizations;
using VetClinic.DAL.Entities;
using System;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.BLL.Services
{
    public class ProcedureService
    {
        private IRepositoryWrapper _repositoryWrapper;
              
        public ProcedureService(IRepositoryWrapper repositoryWrapper)
        {           
            _repositoryWrapper = repositoryWrapper;
        }

        public async void AddProcedure(Procedure procedure)
        {
              _repositoryWrapper.ProcedureRepository.Add(procedure);
               await _repositoryWrapper.SaveAsync();
        }

        public async Task<ICollection<Procedure>> GetAllProcedures()
        {
            return await _repositoryWrapper.ProcedureRepository.GetAsync();
        }

        public  ICollection<string> GetAllProceduresNames()
        {
            ICollection<Procedure> procedures = GetAllProcedures().Result;
            ICollection<string> names = new List<string>();
            string name;
            foreach(Procedure procedure in procedures)
            {
                name = procedure.ProcedureName;
                names.Add(name);
            }
            return names;
        }

        //public async Task<ICollection<Procedure>> GetProcedure(int id)
        //{
        //    Func<Procedure, bool> searchById = (p => p.Id == id);
        //    Expression<Func<Procedure, bool>> filter = (obj) => searchById(obj);
        //    var procedures = await _procedureRepository.GetAsync(filter, null);
        //    return procedures;
        //}

        public async Task<Procedure> GetProcedure(int id)
        {
            return await _repositoryWrapper.ProcedureRepository.GetFirstOrDefaultAsync(
                filter: p => p.Id == id,
                include: source => source.Include(s => s.AppointmentProcedures)
                );
        }

        public async Task<bool> PutProcedure(Procedure procedure)
        {
            var foundProcedure = await _repositoryWrapper.ProcedureRepository.GetFirstOrDefaultAsync(filter: p => p.Id == procedure.Id);
            if (foundProcedure == null) return false;
            foundProcedure.Id = procedure.Id;
            foundProcedure.Description = procedure.Description;
            foundProcedure.IsSelectable = procedure.IsSelectable;
            foundProcedure.Price = procedure.Price;
            foundProcedure.ProcedureName = procedure.ProcedureName;
            _repositoryWrapper.ProcedureRepository.Update(foundProcedure);
            await _repositoryWrapper.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteProcedure(Procedure procedure)
        {
            var foundProcedure = await _procedureRepository.GetFirstOrDefaultAsync(filter: p => p.Id == procedure.Id);
            if (foundProcedure == null)
                return false;
            _repositoryWrapper.ProcedureRepository.Remove(foundProcedure);
            await _repositoryWrapper.SaveAsync();
            return true;
        }

        //public int GetIdForNewProcedure()
        //{
        //    int maxId = GetMaxIdFromProcedures();
        //    int id = maxId + 1;
        //    return id;
        //}

        //public int GetMaxIdFromProcedures()
        //{
        //    ICollection<Procedure> procedures = _procedureRepository.GetAsync().Result;
        //    Procedure procedure = procedures.OrderByDescending(p => p.Id).FirstOrDefault();
        //    if (procedure == null) return 0;
        //    else return procedure.Id;
        //}
    }
}
