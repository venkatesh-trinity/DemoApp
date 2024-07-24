using DemoApp.Domain.Events;

namespace DemoApp.Domain.Entities
{
	public class BankAccount: Entity
	{
        public AccountType AccountType { get; set; } 
        public string AccountNo { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;

        public BankAccount() : base()
		{
		}

        public void Update(string accountNo,
            string accountType,
            string accountName,
            string bankName)
        {
            AccountType = (AccountType)Enum.Parse(typeof(AccountType), accountType);
            AccountNo = accountNo;
            AccountName = accountName;
            BankName = bankName;
            ModifedOn = DateTime.UtcNow;

            AddDomainEvent(new BankAccountUpdated(this));
        }

        public static BankAccount Create(string accountNo,
            string accountType,
			string accountName,
			string bankName)
		{
			return new BankAccount()
			{
				AccountType = (AccountType)Enum.Parse(typeof(AccountType), accountType),
				AccountNo = accountNo,
                AccountName = accountName,
				BankName = bankName
			};
		}
	}
}

