using Milestone.Models;

namespace Milestone.Services
{
    public class SecurityService
    {
        SecurityDAO securityDAO = new SecurityDAO();        
        public bool IsValid(UserModel user)
        {
            return securityDAO.FindUserByNameAndPassword(user);
        }

        public bool AddUser(UserModel user)
        {
            return securityDAO.AddUser(user);
        }
        
    
    }
}
