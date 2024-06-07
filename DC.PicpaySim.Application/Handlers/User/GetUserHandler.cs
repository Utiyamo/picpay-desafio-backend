using DC.PicpaySim.Domain.Commands;
using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.DTO;
using DC.PicpaySim.Infrastructure.Repositories;
using DC.PicpaySim.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Application.Handlers.User
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, BaseResponse<UserDTO>>
    {
        private readonly UserRepository _userRepository;

        public GetUserHandler(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<UserDTO>> Handle(GetUserQuery command, CancellationToken cancellationToken)
        {
            var userResponse = await _userRepository.FindBy(x => x.ExternalId == command.Id);

            if (userResponse == null)
                return BaseResponse<UserDTO>.NotFound($"User not found");

            var user = userResponse.FirstOrDefault();
            if (user == null)
                return BaseResponse<UserDTO>.NotFound($"User not found");

            var resultDTO = new UserDTO(user.ExternalId, user.NomeCompleto, user.Documento, user.Email, user.TypeUser);
            return BaseResponse<UserDTO>.Success(resultDTO);
        }
    }
}
