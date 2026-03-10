using Microsoft.EntityFrameworkCore;
using backend_interview_mid.Contexts;
using backend_interview_mid.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_interview_mid.Services
{
    public class MyofficeAcpdService : IMyofficeAcpdService
    {
        private readonly BackendExamHubDbContext _context;

        public MyofficeAcpdService(BackendExamHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MyOfficeAcpd>> GetAllMyOfficeAcpdsAsync()
        {
            return await _context.MyOfficeAcpds.ToListAsync();
        }

        public async Task<MyOfficeAcpd?> GetMyOfficeAcpdByIdAsync(string id)
        {
            return await _context.MyOfficeAcpds.FindAsync(id);
        }

        public async Task<MyOfficeAcpd> CreateMyOfficeAcpdAsync(MyOfficeAcpd myOfficeAcpd)
        {
            // Generate primary key (AcpdSid) - Example: using GUID
            myOfficeAcpd.AcpdSid = Guid.NewGuid().ToString("N").Substring(0, 20).ToUpper(); // Ensure it fits varchar(20)

            // Set default values for DateTime fields if they are null in the input
            myOfficeAcpd.AcpdNowDateTime = DateTime.Now;
            myOfficeAcpd.AcpdUpddateTime = DateTime.Now;

            _context.MyOfficeAcpds.Add(myOfficeAcpd);
            await _context.SaveChangesAsync();
            return myOfficeAcpd;
        }

        public async Task<bool> UpdateMyOfficeAcpdAsync(string id, MyOfficeAcpd myOfficeAcpd)
        {
            if (id != myOfficeAcpd.AcpdSid)
            {
                return false; // ID mismatch
            }

            // Ensure AcpdSid is not null before using it
            if (string.IsNullOrEmpty(myOfficeAcpd.AcpdSid))
            {
                return false; // ACPD_SID cannot be null or empty.
            }

            // Update UpdatedAt timestamp
            myOfficeAcpd.AcpdUpddateTime = DateTime.Now;

            _context.Entry(myOfficeAcpd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MyOfficeAcpdExistsAsync(id))
                {
                    return false; // Not Found
                }
                else
                {
                    throw; // Re-throw other concurrency exceptions
                }
            }
            return true;
        }

        public async Task<bool> DeleteMyOfficeAcpdAsync(string id)
        {
            var myOfficeAcpd = await _context.MyOfficeAcpds.FindAsync(id);
            if (myOfficeAcpd == null)
            {
                return false; // Not Found
            }

            _context.MyOfficeAcpds.Remove(myOfficeAcpd);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MyOfficeAcpdExistsAsync(string id)
        {
            return await _context.MyOfficeAcpds.AnyAsync(e => e.AcpdSid == id);
        }
    }
}
