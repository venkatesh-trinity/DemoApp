using DemoApp.Application.BankAccount.Common;
using DemoApp.Application.Common.Interfaces.Persistance;
using DemoApp.Domain.Common.Errors;
using DemoApp.Domain.Entities;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace DemoApp.Application.BankAccount.Update
{
    public class BankAccountUpdateCommandHandler:
		IRequestHandler<BankAccountUpdateCommand, ErrorOr<BankAccountDetailsResult>>
	{
		private readonly IBankAccountRepository _repository;
		private readonly IMapper _mapper;

        public BankAccountUpdateCommandHandler(IBankAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BankAccountDetailsResult>> Handle(BankAccountUpdateCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var account = await _repository.GetBankAccount(request.Id);

                if(account == null)
                {
                    return Errors.BankAccount.AccountNotFound;
                }

                account.Update(request.AccountNo,
                    request.AccountType,
                    request.AccountName,
                    request.BankName);

                await _repository.Update(account);

                return _mapper.Map<BankAccountDetailsResult>(account);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(ex.Message, ex.Message!);
            }
        }
    }
}

