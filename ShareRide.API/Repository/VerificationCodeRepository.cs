using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using ShareRide.API.DataContext;
using ShareRide.API.Exception;
using ShareRide.API.Models;

namespace ShareRide.API.Repository
{
    public class VerificationCodeRepository
    {
        private readonly ShareRideDbContext _context;

        public VerificationCodeRepository(ShareRideDbContext context)
        {
            _context = context;
        }

        public async Task<VerificationCode> FindByCode(string code)
        {
            try
            {
                return await _context.VerificationCodes.FirstAsync(c => c.Code == code);
            }
            catch
            {
                throw new VerificationCodeNotExistException("Verification code:" + code + " does not exist");
            }
        }

        internal async void DeleteByCode(VerificationCode verification)
        {
            try
            {
                _context.VerificationCodes.Remove(verification);
                await _context.SaveChangesAsync();
            }
            catch {
                throw new VerificationCodeNotExistException("Verification code:" + verification.Code + " does not exist");
            }
        }
    }
}
