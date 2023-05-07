using System.Security.Claims;
using Autofac;

namespace StackOverflow.Web.Models
{
    public class BaseModel
    {

        private IHttpContextAccessor _httpContextAccessor;

        protected BaseModel()
        {

        }

        protected BaseModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual void ResolveDependency(ILifetimeScope scope)
        {
            _httpContextAccessor = scope.Resolve<IHttpContextAccessor>();
        }

        protected Guid GetCurrentUserId()
        {
            var id = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(id, out var userId) ? userId : Guid.Empty;
        }
       
        public void ClearSession()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }
      
    }
}
