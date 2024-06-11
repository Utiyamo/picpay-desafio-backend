using DC.PicpaySim.Domain.Commands;
using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.DTO;
using DC.PicpaySim.Domain.Entities;
using DC.PicpaySim.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Application.Handlers.UserWallet
{
    public class GetWalletByUserHandler : IRequestHandler<GetWalletByUserQuery, BaseResponse<UserWithWalletsDTO>>
    {
        private readonly UserRepository _userRepository;
        private readonly UserWalletRepository _userWalletRepository;

        public GetWalletByUserHandler(UserWalletRepository userWalletRepository, UserRepository userRepository)
        {
            _userRepository = userRepository;
            _userWalletRepository = userWalletRepository;
        }

        public async Task<BaseResponse<UserWithWalletsDTO>> Handle(GetWalletByUserQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userRepository.FindBy(x => x.ExternalId == query.UserID);
                if(users == null)
                    return BaseResponse<UserWithWalletsDTO>.NotFound("User not found");
                else if(!users.Any())
                    return BaseResponse<UserWithWalletsDTO>.NotFound("User not found");

                var user = users.FirstOrDefault();

                var wallets = await _userWalletRepository.FindBy(x => x.UserID == user.Id);
                if(wallets == null)
                    return BaseResponse<UserWithWalletsDTO>.NotFound("User have no wallets");
                else if(!wallets.Any())
                    return BaseResponse<UserWithWalletsDTO>.NotFound("User have no wallets");

                var listwalletsDTO = new List<UserWalletDTO>();
                foreach(var item in wallets)
                {
                    listwalletsDTO.Add(new UserWalletDTO(item.ExternalId, item.Name, item.Primary, item.CreditAmmout));
                }

                var result = new UserWithWalletsDTO(user.ExternalId, user.NomeCompleto, user.Documento, user.Email, user.TypeUser, listwalletsDTO);

                return BaseResponse<UserWithWalletsDTO>.Success(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<UserWithWalletsDTO>.Error(500, ex.Message);
            }
        }
    }
}
