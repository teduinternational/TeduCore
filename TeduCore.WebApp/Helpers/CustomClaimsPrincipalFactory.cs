using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduCore.Data.Entities;

namespace TeduCore.WebApp.Helpers
{
    public class CustomClaimsPrincipalFactory :
    UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        private readonly UserManager<AppUser> _userManager;

        public CustomClaimsPrincipalFactory(
           UserManager<AppUser> userManager,
           RoleManager<AppRole> roleManager,
           IOptions<IdentityOptions> optionsAccessor) :
              base(userManager, roleManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        public override async Task<ClaimsPrincipal>
           CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            // Add your claims here
            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim("UserId",user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("FullName",user.FullName),
                new Claim("Avatar",string.IsNullOrEmpty(user.Avatar)?string.Empty:user.Avatar),
                new Claim("Roles",string.Join(";",roles))
            });

            return principal;
        }
    }
}