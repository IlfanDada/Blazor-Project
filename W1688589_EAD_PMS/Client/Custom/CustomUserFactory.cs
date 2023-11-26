using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Custom
{
    public class CustomUserFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        /*
         * Identity Server sends multiple roles as a JSON array in a single role claim. A single role 
         * is sent as a string value in the claim. The factory creates an individual role claim for each of the user's roles.
         */
        public CustomUserFactory(IAccessTokenProviderAccessor accessor)
            : base(accessor)
        {
        }
        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(
            RemoteUserAccount account,
            RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);
            var claimsIdentity = (ClaimsIdentity)user.Identity;
            if (account != null)
            {
                MapArrayClaimsToMultipleSeparateClaims(account, claimsIdentity);
            }
            return user;
        }
        private void MapArrayClaimsToMultipleSeparateClaims(RemoteUserAccount account, ClaimsIdentity claimsIdentity)
        {
            foreach (var prop in account.AdditionalProperties)
            {
                var key = prop.Key;
                var value = prop.Value;
                if (value != null &&
                    (value is JsonElement element && element.ValueKind == JsonValueKind.Array))
                {
                    claimsIdentity.RemoveClaim(claimsIdentity.FindFirst(prop.Key));
                    var claims = element.EnumerateArray()
                        .Select(x => new Claim(prop.Key, x.ToString()));
                    claimsIdentity.AddClaims(claims);
                }
            }
        }
    }
}
