
namespace IwMetrics.Application.Services
{
    public class IdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly byte[] _key;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSecurityTokenHandler TokenHandler = new();

        public IdentityService(IOptions<JwtSettings> jwtOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _jwtSettings = jwtOptions.Value;
            _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AddDefaultClaim(IdentityUser user, UserProfile profile)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("IdentityId", user.Id),
                new Claim("UserProfileId", profile.UserProfileId.ToString()),
                new Claim("ExternalRole", "Client") 
            };

            await _userManager.AddClaimsAsync(user, claims);
        }

        // Get all valid claims, including role claims
        public async Task<List<Claim>> GetAllValidClaims(IdentityUser user)
        {
            var claims = new List<Claim>();

            // Get user-specific claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            // Get role claims
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var roleName in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));

                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }
            }

            return claims;
        }

        public JwtTokenResponse GenerateJwtToken(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims);
            var expirationTime = DateTime.UtcNow.AddHours(2);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = expirationTime,
                Audience = _jwtSettings.Audiences[0],
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = TokenHandler.CreateToken(tokenDescriptor);

            return new JwtTokenResponse
            {
                Token = TokenHandler.WriteToken(token),
                Expiration = expirationTime
            };
            
        }

    }
}

