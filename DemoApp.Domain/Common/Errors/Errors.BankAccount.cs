using ErrorOr;

namespace DemoApp.Domain.Common.Errors
{
	public static partial class Errors
	{
		public static class BankAccount
		{
            public static Error AccountNotFound => Error.NotFound(
                code: "BankAccount.NotFound",
                description: "Account Not Found"
                );

            public static Error DuplicateAccount => Error.Conflict(
                code: "BankAccount.Duplicate",
                description: "Account Number already in use"
                );

            public static Error AccountNoData => Error.NotFound(
                code: "BankAccount.NoData",
                description: "No Data"
                );
        }
	}
}

