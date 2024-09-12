using IcecreamApp.Api.Data;
using IcecreamApp.Api.Data.Entities;
using IcecreamApp.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace IcecreamApp.Api.Services
{
    public class AuthServices(DataContext context, TokenServices tokenServices, PasswordServices passwordServices)
    {
        private readonly DataContext _context = context;
        private readonly TokenServices _tokenServices = tokenServices;
        private readonly PasswordServices _passwordServices = passwordServices;
        public async Task<ResultWithDataDto<AuthResponseDto>> SignupAsync(SignupRequestDto dto)
        {
            if (await _context.Users.AsNoTracking().AnyAsync(u => u.Email == dto.Email))
            {
                return ResultWithDataDto<AuthResponseDto>.Failure("Email already exist");
            }
            var user = new User
            {
                Email = dto.Email,
                Address = dto.Address,
                Name = dto.Name
            };
            (user.Salt, user.Hash) = _passwordServices.GenerateSaltAndHash(dto.Password);
            try
            {

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return GenerateAuthResponse(user);
            }
            catch (Exception ex)
            {
                return ResultWithDataDto<AuthResponseDto>.Failure(ex.Message);
            }
        }



        public async Task<ResultWithDataDto<AuthResponseDto>> SigninAsync(SigninRequestDto dto)
        {
            var dbUser = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (dbUser is null)
                return ResultWithDataDto<AuthResponseDto>.Failure("user doesn't exist");

             if (!_passwordServices.AreEqual(dto.Password, dbUser.Salt, dbUser.Hash))
                return ResultWithDataDto<AuthResponseDto>.Failure("incorrect password");

            return GenerateAuthResponse(dbUser);
        }

        private ResultWithDataDto<AuthResponseDto> GenerateAuthResponse(User user)
        {
            var loggedInUser = new LoggedInUser(user.Id, user.Name, user.Address, user.Email);
            var token = _tokenServices.GenerateJwt(loggedInUser);
            var authResponse = new AuthResponseDto(loggedInUser, token);
            return ResultWithDataDto<AuthResponseDto>.Success(authResponse);
        }
    }
}