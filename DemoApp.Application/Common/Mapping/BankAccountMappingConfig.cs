using DemoApp.Application.BankAccount.Common;
using Mapster;
using Entities = DemoApp.Domain.Entities;

namespace DemoApp.Application.Common.Mapping
{
    public class BankAccountMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            var detailsConfig = config.NewConfig<Entities.BankAccount, BankAccountDetailsResult>()
                .Map(dest => dest.AccountType, source => source.AccountType.ToString());
        }
    }
}

