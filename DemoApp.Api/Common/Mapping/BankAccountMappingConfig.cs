using DemoApp.Api.Contracts;
using DemoApp.Application.BankAccount.Common;
using DemoApp.Application.BankAccount.Update;
using Mapster;

namespace DemoApp.Api.Common.Mapping
{
    public class BankAccountMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<BankAccountDetailsResult, BankAccountDetailsResponse>();

            config.NewConfig<(UpdateBankAccountRequest, Guid), BankAccountUpdateCommand>()
               .Map(dest => dest.Id, source => source.Item2)
               .Map(dest => dest, source => source.Item1);
        }
    }
}

