using DC.PicpaySim.Domain.Commands;
using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.DTO;
using DC.PicpaySim.Domain.Enums;
using DC.PicpaySim.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Application.Handlers.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse<UserDTO>>
    {
        private readonly UserRepository _userRepository;
        private readonly IValidator<CreateUserCommand> _validator;

        public CreateUserHandler(UserRepository userRepository,
            IValidator<CreateUserCommand> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<BaseResponse<UserDTO>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = _validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    string errorMessages = "";
                    foreach(var error in validationResult.Errors)
                    {
                        errorMessages += $"{error} / ";
                    }
                    return BaseResponse<UserDTO>.Error(400, errorMessages);
                }

                var users = await _userRepository.FindBy(x => x.Documento == command.Document || x.Email == command.Email);
                if (users.Any())
                    return BaseResponse<UserDTO>.Error(400, $"User arready exists.");

                var user = users.FirstOrDefault();
                if(user != null)
                    return BaseResponse<UserDTO>.Error(400, $"User arready exists.");

                var newUser = new Domain.Entities.User(command.Name, command.Document, command.Email, command.Password, command.TypeUser);
                newUser = await _userRepository.Create(newUser);
                await _userRepository.Commit();
                 
                if (newUser.Id == 0)
                    return BaseResponse<UserDTO>.Error(500, $"Internal error to create User.");

                var resultDTO = new UserDTO(newUser.ExternalId, newUser.NomeCompleto, newUser.Documento, newUser.Email, newUser.TypeUser);
                return BaseResponse<UserDTO>.Success(resultDTO);
            }
            catch (Exception ex)
            {
                await _userRepository.Rollback();
                throw ex;
            }
        }
    }
}
