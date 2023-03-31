using Core.Dtos.Tokens;
using Core.Dtos.Users;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAuthenticationService
    {
        //kullanici giris yapmissa yeniden bi token doner
        Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);

        //refreshtoken la yeniden bir token olusturulur
        Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

        //kullanici logout olunca kullanilir
        Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken);

        //uyelik sistemi olmadan bir token uretilir
        //CustomResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);

       // Task<CustomResponseDto<TokenDto>> PasswordResetAsync(string email);
    }
}
