using DemoApp.Application.BankAccount.Common;
using DemoApp.Application.Common.Interfaces.Persistance;
using DemoApp.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace DemoApp.Application.BankAccount.List
{
    public class BankAccountsByTypeQueryHandler:
		IRequestHandler<BankAccountsByTypeQuery, ErrorOr<List<BankAccountDetailsResult>>>
    {
        private readonly IBankAccountRepository _repository;
        private readonly IMapper _mapper;

        public BankAccountsByTypeQueryHandler(IBankAccountRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<BankAccountDetailsResult>>> Handle(BankAccountsByTypeQuery request,
            CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetBankAccounts(request.AccountType);

            if (accounts == null || accounts.Count == 0)
                return Errors.BankAccount.AccountNoData;

            return _mapper.Map<List<BankAccountDetailsResult>>(accounts);
        }
    }
}

