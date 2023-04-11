using AutoMapper;
using InsureityAPI.Controllers;
using InsureityAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;

namespace InsureityAPI.Repository
{
    public class AgentRepo : IAgentRepo<LoginDetails>
    {
        private readonly InsureityContext db;
        private readonly IMapper _mapper;

        public AgentRepo(InsureityContext _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }

        public async void AddAgent(LoginDetails l)
        {
            l.UserSalt = RandomNumberGenerator.GetBytes(128 / 8);
            l.UserPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: l.UserPassword, salt: l.UserSalt, prf: KeyDerivationPrf.HMACSHA256, iterationCount: 1000, numBytesRequested: 256 / 8));
            db.LoginDetailsList.Add(l);
            db.SaveChanges();
        }

        public void DeleteAgent(LoginDetails l)
        {
            db.LoginDetailsList.Remove(l);
            db.SaveChanges();
        }

        public LoginDetails GetAgent(int agentId)
        {
            try
            {
                return db.LoginDetailsList.Where(x => x.UserId == agentId && x.Role == "Agent").SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LoginDetails> GetAllAgents()
        {
            try
            {
                return db.LoginDetailsList.Where(x => x.Role == "Agent").ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateAgent(LoginDetails l)
        {
            try
            {
                db.Entry(l).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LoginDTO VerifyAgentDetails(string email, string password)
        {
            List<LoginDTO> LoginDTO_list = new();
            LoginDTO_list = db.LoginDetailsList.Select(e => _mapper.Map<LoginDTO>(e)).ToList();

            var res = LoginDTO_list.Where(x => x.Role == "Agent" && x.UserEmail == email).SingleOrDefault();
            if(res == null)
            {
                return null;
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: password, salt: res.UserSalt, prf: KeyDerivationPrf.HMACSHA256, iterationCount: 1000, numBytesRequested: 256 / 8));
            if (hashed == res.UserPassword)
            {
                return res;
            }
            return null;
        }
    }
}
