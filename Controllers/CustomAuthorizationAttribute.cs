using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Activity_2_RegisterAndLoginApp.Controllers
{
	internal class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			string username = context.HttpContext.Session.GetString("username");

			if(username == null)
			{
				context.Result = new RedirectResult("/login");
			}
			else
			{
				// do nothing
			}
		
		}
	}
}