using InsureityAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace InsureityAPI.Repository
{
    public interface IAgentRepo<LoginDetails>
    {
        public LoginDTO VerifyAgentDetails(string email, string password);
        public void AddAgent(LoginDetails l);
        public void UpdateAgent(LoginDetails l);
        public void DeleteAgent(LoginDetails l);
        public LoginDetails GetAgent(int agentId);
        public List<LoginDetails> GetAllAgents();

    }
}
