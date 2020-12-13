using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<(bool, string)> CreateUser(User inputUser);
        public Task<bool> UpdateUser(User inputUser);
    }
}
