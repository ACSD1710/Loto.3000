using Loto3000.Application;
using System.Security.Claims;

namespace Loto3000
{
    public class ClaimsPrincipalWrapper //  Facade pattern
        : IRolePrincipal
    {
        public readonly ClaimsPrincipal claimsPrincipal;

        public ClaimsPrincipalWrapper(ClaimsPrincipal claimsPrincipal)
        {
            this.claimsPrincipal = claimsPrincipal;
        }

        public string Name => claimsPrincipal.FindFirstValue(ClaimTypes.Name);

        public int Id => int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));

        public bool IsInRole(string roleName)
        {
            return claimsPrincipal.IsInRole(roleName);
        }
    }
}
