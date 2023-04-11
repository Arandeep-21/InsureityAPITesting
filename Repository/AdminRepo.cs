using AutoMapper;
using InsureityAPI.Controllers;
using InsureityAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InsureityAPI.Repository
{
    public class AdminRepo : IAdminRepo<LoginDetails>
    {
        private readonly InsureityContext db;
        private readonly IMapper _mapper;

        public AdminRepo(InsureityContext _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }

        public LoginDTO VerifyAdminDetails(string email, string password)
        {
            var res = db.LoginDetailsList.Where(x => x.Role == "Admin"&& x.UserEmail == email).SingleOrDefault();
            if(res == null)
            {
                return null;
            }
            LoginDTO loginDTO = new();
            if (res == null) {
                return null;
            }
            loginDTO = _mapper.Map<LoginDTO>(res);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: password, salt: res.UserSalt, prf: KeyDerivationPrf.HMACSHA256, iterationCount: 1000, numBytesRequested: 256 / 8));
            if (hashed == res.UserPassword)
            {
               return loginDTO;
            }
            return null;
        }

        public async void AddAdmin(LoginDetails l) {
            l.UserSalt = RandomNumberGenerator.GetBytes(128 / 8);
            l.UserPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: l.UserPassword,salt:l.UserSalt,prf:KeyDerivationPrf.HMACSHA256,iterationCount:1000,numBytesRequested:256/8));
            db.LoginDetailsList.Add(l);
            db.SaveChangesAsync();
        }

    }
}
