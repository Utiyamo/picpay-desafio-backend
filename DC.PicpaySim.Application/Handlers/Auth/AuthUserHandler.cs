using DC.PicpaySim.Domain.Commands;
using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.DTO;
using DC.PicpaySim.Domain.Interfaces;
using DC.PicpaySim.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Application.Handlers.Auth
{
    public class AuthUserHandler : IRequestHandler<AuthUserCommand, BaseResponse<UserAuthDTO>>
    {
        private readonly UserRepository _userRepository;
        private readonly IValidator<AuthUserCommand> _validator;
        private readonly IAuthenticationService _authService;

        public AuthUserHandler(UserRepository userRepository,
            IValidator<AuthUserCommand> validator,
            IAuthenticationService authService)
        {
            _userRepository = userRepository;
            _validator = validator;
            _authService = authService;
        }

        public async Task<BaseResponse<UserAuthDTO>> Handle(AuthUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = _validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    string errorMessages = "";
                    foreach (var error in validationResult.Errors)
                    {
                        errorMessages += $"{error} / ";
                    }
                    return BaseResponse<UserAuthDTO>.Error(400, errorMessages);
                }

                var users = await _userRepository.FindBy(x => x.Email == command.Email);
                if (!users.Any())
                    return BaseResponse<UserAuthDTO>.Unauthorized($"Email/Password invalid");

                var user = users.FirstOrDefault();
                if (user == null)
                    return BaseResponse<UserAuthDTO>.Unauthorized($"Email/Password invalid");

                var passwordEncrpted = EncryptHelper.sha256(command.Password);
                if(user.Senha != passwordEncrpted)
                    return BaseResponse<UserAuthDTO>.Unauthorized($"Email/Password invalid");

                var userDto = new UserDTO(user.ExternalId, user.NomeCompleto, user.Documento, user.Email, user.TypeUser);
                var bearerInfo = _authService.GenerateToken(userDto);

                var result = new UserAuthDTO(user.ExternalId, user.NomeCompleto, user.Email, bearerInfo.BearerToken, bearerInfo.Expiration);
                return BaseResponse<UserAuthDTO>.Success(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
