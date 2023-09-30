using Assigment.Models;
using System.Threading.Tasks;

namespace Assigment.Repositories
{
    public interface IAssigmentRepository
    {
        Task<ResponseModel> ConversionUnit(string uName, int ConversionValue);
    }
}
