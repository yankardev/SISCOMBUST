using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SISCOMBUST.Filters
{
    public class RolAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public RolAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var rolUsuario = context.HttpContext.Session.GetString("Rol");

            if (rolUsuario == null)
            {
                // Si no ha iniciado sesión, lo envía al login
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
            else if (!_roles.Contains(rolUsuario))
            {
                // Si no tiene el rol necesario, muestra acceso denegado
                context.Result = new ViewResult { ViewName = "~/Views/Shared/AccessDenied.cshtml" };
            }

            base.OnActionExecuting(context);
        }
    }
}
