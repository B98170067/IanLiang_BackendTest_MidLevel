using backend_interview_mid.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend_interview_mid.Services
{
    public interface IMyofficeAcpdService
    {
        Task<IEnumerable<MyOfficeAcpd>> GetAllMyOfficeAcpdsAsync();
        Task<MyOfficeAcpd?> GetMyOfficeAcpdByIdAsync(string id);
        Task<MyOfficeAcpd> CreateMyOfficeAcpdAsync(MyOfficeAcpd myOfficeAcpd);
        Task<bool> UpdateMyOfficeAcpdAsync(string id, MyOfficeAcpd myOfficeAcpd);
        Task<bool> DeleteMyOfficeAcpdAsync(string id);
        Task<bool> MyOfficeAcpdExistsAsync(string id); // Helper method
    }
}
