using IcecreamApp.Shared.Dtos;
using Refit;

namespace IcecreamApp.Services
{
    public interface IAuthApi
    {
        [Post("/api/auth/signup")]
        Task<ResultWithDataDto<AuthResponseDto>> SignupAsync(SignupRequestDto dto);

        [Post("/api/auth/signin")]
        Task<ResultWithDataDto<AuthResponseDto>> SigninAsync(SigninRequestDto dto);

        [Headers("Authorization: Bearer")]
        [Post("/api/auth/change-password")]
        Task<ResultDto> ChangePasswordAsync(ChangePasswordDto dto);

    }

}