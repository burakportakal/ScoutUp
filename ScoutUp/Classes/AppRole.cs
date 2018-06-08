using Microsoft.AspNet.Identity.EntityFramework;
namespace ScoutUp.Classes
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }
        public AppRole(string name) : base(name) { }
    }
}