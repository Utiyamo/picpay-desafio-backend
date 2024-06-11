using DC.PicpaySim.Domain.Commands;
using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.DTO;
using DC.PicpaySim.Domain.Entities;
using DC.PicpaySim.Infrastructure.Repositories;
using DC.PicpaySim.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Application.Handlers.Transaction
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, BaseResponse<TransactionDTO>>
    {
        private readonly UserRepository _userRepository;
        private readonly UserWalletRepository _userWalletRepository;
        private readonly TransactionRepository _transactionRepository;

        private readonly IValidator<CreateTransactionCommand> _validator;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CreateTransactionHandler(IValidator<CreateTransactionCommand> validator,
            UserRepository userRepository,
            TransactionRepository transactionRepository,
            UserWalletRepository userWalletRepository,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _validator = validator;
            _userRepository = userRepository;
            _userWalletRepository = userWalletRepository;
            _transactionRepository = transactionRepository;

            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<BaseResponse<TransactionDTO>> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
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
                    return BaseResponse<TransactionDTO>.Error(400, errorMessages);
                }

                IList<Guid> walletIDs = new List<Guid>()
                {
                    command.PayerID,
                    command.PayeeID
                };

                var wallets = await _userWalletRepository.FindBy(x => walletIDs.Contains(x.ExternalId));

                var payer = (from wallet in wallets
                             where wallet.ExternalId == command.PayerID
                             select wallet).FirstOrDefault();

                var payee = (from wallet in wallets
                             where wallet.ExternalId == command.PayeeID
                             select wallet).FirstOrDefault();

                if (payer == null)
                    return BaseResponse<TransactionDTO>.Error(400, "Payer not found");

                if (payee == null)
                    return BaseResponse<TransactionDTO>.Error(400, "Payee not found");

                var users = await _userRepository.FindBy(x => payer.UserID == x.Id);
                var user = users.FirstOrDefault();

                if (user == null)
                    return BaseResponse<TransactionDTO>.Error(400, "Payer not found");
                else if (user.TypeUser == Domain.Enums.ETypeUser.shopkeeper)
                    return BaseResponse<TransactionDTO>.Error(400, "Shopkeepers cannot make transactions");

                if (payer.CreditAmmout < command.Value)
                    return BaseResponse<TransactionDTO>.Error(400, "The Payer don't have all the money to transfer");

                var transaction = new Transactions(payer.Id, payee.Id, DateTime.Now, command.Value);
                var result = await _transactionRepository.Create(transaction);

                var TransactionAuth = await GetAutorization();
                if (!TransactionAuth.isSuccess)
                {
                    await _transactionRepository.Rollback();
                    return BaseResponse<TransactionDTO>.Error(500, TransactionAuth.Message);
                }

                await SendNotification();
                var resultDTO = new TransactionDTO(transaction.ExternalId, payer.ExternalId, payee.ExternalId, command.Value, transaction.TransactionDate);
                return BaseResponse<TransactionDTO>.Success(resultDTO);
            }
            catch (Exception ex)
            {
                return BaseResponse<TransactionDTO>.Error(500, ex.Message);
            }
        }

        private async Task<BaseResponse> GetAutorization()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                _httpClient.BaseAddress = new Uri(_configuration["ExternalAPI:Authentication:baseURL"]);

                var response = await _httpClient.GetAsync("api/v2/authorize");
                if (response.IsSuccessStatusCode)
                    return new BaseResponse
                    {
                        isSuccess = true,
                        Message = String.Empty,
                        Status = 200
                    };
                else
                {
                    var stringContent = await response.Content.ReadAsStringAsync();
                    return new BaseResponse
                    {
                        isSuccess = false,
                        Message = stringContent,
                        Status = (int)response.StatusCode,
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    isSuccess = false,
                    Message = ex.Message,
                    Status = 500
                };
            }
        }

        private async Task<BaseResponse> SendNotification()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                _httpClient.BaseAddress = new Uri(_configuration["ExternalAPI:Notification:baseURL"]);

                var response = await _httpClient.PostAsync("api/v1/notify", null);
                if (response.IsSuccessStatusCode)
                    return new BaseResponse
                    {
                        isSuccess = true,
                        Message = String.Empty,
                        Status = 200
                    };
                else
                {
                    var stringContent = await response.Content.ReadAsStringAsync();
                    return new BaseResponse
                    {
                        isSuccess = false,
                        Message = stringContent,
                        Status = (int)response.StatusCode,
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    isSuccess = false,
                    Message = ex.Message,
                    Status = 500
                };
            }
        }
    }
}
