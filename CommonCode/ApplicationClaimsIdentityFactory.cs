using BuildingMaintenance.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuildingMaintenance.CommonCode
{
    public class ApplicationClaimsIdentityFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        //UserManager<ApplicationUser> _userManager;
        public ApplicationClaimsIdentityFactory(UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        { }
        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            if (user.IsCPUser)
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim("IsDeveloper", "true")
            });
            }
            return principal;
        }
    }
}
