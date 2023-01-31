using Activity_2_RegisterAndLoginApp.Models;

namespace Activity_2_RegisterAndLoginApp.Services
{
    public class SecurityService
    {
        SecurityDAO securityDAO = new SecurityDAO();        
        public bool IsValid(UserModel user)
        {
            return securityDAO.FindUserByNameAndPassword(user);
        }
        
    
    }
}
