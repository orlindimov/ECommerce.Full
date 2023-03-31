using Core.Configuration;
using Core.Dtos.Tokens;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TokenService:ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> options, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenOption = options.Value;

        }


        //random bi string deger uretir
        public string CreateRefreshToken()
        {
            var numberByte = new Byte[32];

            //random bi deger uretir
            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }
        //Claim=payload da tuttugumus herseyi temsil eder
        //uyelik sistemi oldugu zaman bu token calisacak
        private async Task<IEnumerable<Claim>> GetClaims(UserApp userApp, List<string> audiences)
        {
            var userRoles = await _userManager.GetRolesAsync(userApp);

            // claimler kullanici hakkinda tuttugumuz bilgilerdir(key-value ciftiridr).
            var userList = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,userApp.Id),
                new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
                new Claim(ClaimTypes.Name,userApp.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            userList.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            return userList;
        }

        //uyelik sistemi gerek olmadigi zaman bu calisacak
        //private static IEnumerable<Claim> GetClaimsByClient(Client client)
        //{
        //    var claims = new List<Claim>();
        //    claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
        //    new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());
        //    return claims;
        //}


        public TokenDto CreateToken(UserApp userApp)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);

            var securityKey = SignServic.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            //token olustururken burayi kullanir
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(userApp, _tokenOption.Audience).Result,
                signingCredentials: signingCredentials);

            //handler token olusturur
            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration,
            };

            return tokenDto;

        }


        //public ClientTokenDto CreateTokenByClient(Client client)
        //{
        //    var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);


        //    var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

        //    //token olustururken burayi kullanir
        //    SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        //    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
        //        issuer: _tokenOption.Issuer,
        //        expires: accessTokenExpiration,
        //        notBefore: DateTime.Now,
        //        claims: GetClaimsByClient(client),
        //        signingCredentials: signingCredentials);

        //    var handler = new JwtSecurityTokenHandler();

        //    var token = handler.WriteToken(jwtSecurityToken);

        //    var tokenDto = new ClientTokenDto
        //    {
        //        AccessToken = token,

        //        AccessTokenExpiration = accessTokenExpiration

        //    };

        //    return tokenDto;
        //}
    }
}
