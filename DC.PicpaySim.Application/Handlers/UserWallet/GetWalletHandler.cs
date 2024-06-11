using DC.PicpaySim.Domain.Commands;
using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.DTO;
using DC.PicpaySim.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Application.Handlers.UserWallet
{
    public class GetWalletHandler : IRequestHandler<GetWalletQuery, BaseResponse<UserWalletDTO>>
    {

        private readonly UserRepository _userRepository;
        private readonly UserWalletRepository _userWalletRepository;

        public GetWalletHandler(UserWalletRepository userWalletRepository, UserRepository userRepository)
        {
            _userRepository = userRepository;
            _userWalletRepository = userWalletRepository;
        }

        public async Task<BaseResponse<UserWalletDTO>> Handle(GetWalletQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var wallets = await _userWalletRepository.FindBy(x => x.ExternalId == query.Id);
                if (!wallets.Any())
                    return BaseResponse<UserWalletDTO>.NotFound("Wallet not found");

                var wallet = wallets.First();

                var user = await _userRepository.FindById(wallet.UserID);

                var userDTO = new UserDTO(user.ExternalId, user.NomeCompleto, user.Documento, user.Email, user.TypeUser);
                var result = new UserWalletDTO(wallet.ExternalId, wallet.Name, wallet.Primary, wallet.CreditAmmout);

                return BaseResponse<UserWalletDTO>.Success(result);
            }
            catch (Exception ex)
            {
                return BaseResponse<UserWalletDTO>.Error(500, ex.Message);
            }
        }
    }
}
