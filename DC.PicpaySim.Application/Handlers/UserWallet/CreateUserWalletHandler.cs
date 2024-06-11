using DC.PicpaySim.Domain.Commands;
using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.DTO;
using DC.PicpaySim.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Application.Handlers.UserWallet
{
    public class CreateUserWalletHandler : IRequestHandler<CreateWalletCommand, BaseResponse<UserWalletWithUserDTO>>
    {
        private readonly UserRepository _userRepository;
        private readonly UserWalletRepository _userWalletRepository;

        private readonly IValidator<CreateWalletCommand> _validator;

        public CreateUserWalletHandler(UserRepository userRepository, UserWalletRepository userWalletRepository,
            IValidator<CreateWalletCommand> validator)
        {
            _userRepository = userRepository;
            _userWalletRepository = userWalletRepository;

            _validator = validator;
        }

        public async Task<BaseResponse<UserWalletWithUserDTO>> Handle(CreateWalletCommand command, CancellationToken cancellationToken)
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
                    return BaseResponse<UserWalletWithUserDTO>.Error(400, errorMessages);
                }

                var users = await _userRepository.FindBy(x => x.ExternalId == command.UserID);
                if (!users.Any())
                    return BaseResponse<UserWalletWithUserDTO>.Error(400, $"User not found. Cannot create a Wallet");

                var user = users.FirstOrDefault();
                if(user == null)
                    return BaseResponse<UserWalletWithUserDTO>.Error(400, $"User not found. Cannot create a Wallet");

                var wallets = await _userWalletRepository.FindBy(x => x.UserID == user.Id);

                if (user.TypeUser == Domain.Enums.ETypeUser.shopkeeper && wallets.Any())
                    return BaseResponse<UserWalletWithUserDTO>.Error(400, $"Shopkeepers cannot have more than one wallet");

                if (command.IsPrimary)
                {
                    if (wallets.Any())
                    {
                        var primaryWallet = (from a in wallets
                                             where a.Active && a.Primary
                                             select a).FirstOrDefault();

                        if (primaryWallet != null)
                            return BaseResponse<UserWalletWithUserDTO>.Error(400, $"User arready have a Primary Wallet, creation cancellated");
                    }
                }
                
                var newWallet = new Domain.Entities.UserWallet(command.Name, user.Id, true, command.IsPrimary, command.CreditValue);

                newWallet = await _userWalletRepository.Create(newWallet);
                await _userWalletRepository.Commit();

                if (newWallet.Id == 0)
                    return BaseResponse<UserWalletWithUserDTO>.Error(500, $"Internal Error when create Wallet");

                var userDTO = new UserDTO(user.ExternalId, user.NomeCompleto, user.Documento, user.Email, user.TypeUser);
                var resultDTO = new UserWalletWithUserDTO(newWallet.ExternalId, newWallet.Name, newWallet.Primary, newWallet.CreditAmmout, userDTO);
                return BaseResponse<UserWalletWithUserDTO>.Success(resultDTO);
            }
            catch(Exception ex)
            {
                await _userWalletRepository.Rollback();
                return BaseResponse<UserWalletWithUserDTO>.Error(500, ex.Message);
            }
        }
    }
}
