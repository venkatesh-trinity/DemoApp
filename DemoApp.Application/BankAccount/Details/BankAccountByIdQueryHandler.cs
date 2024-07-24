using DemoApp.Application.BankAccount.Common;
using DemoApp.Application.Common.Interfaces.Persistance;
using DemoApp.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace DemoApp.Application.BankAccount.Details
{
    public class BankAccountByIdQueryHandler: IRequestHandler<BankAccountByIdQuery, ErrorOr<BankAccountDetailsResult>>
	{
        private readonly IBankAccountRepository _repository;
        private readonly IMapper _mapper;

        public BankAccountByIdQueryHandler(IBankAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BankAccountDetailsResult>> Handle(BankAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetBankAccount(request.Id);

            if (account == null)
                return Errors.BankAccount.AccountNotFound;

            return _mapper.Map<BankAccountDetailsResult>(account);
        }
    }
}

