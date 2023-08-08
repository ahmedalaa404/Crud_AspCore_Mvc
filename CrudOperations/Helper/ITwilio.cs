using Dal_CrudOperations.DomainModel;
using Twilio.Rest.Api.V2010.Account;

namespace CrudOperations.Helper
{
	public interface ITwilio
	{
		MessageResource send(SmsMessage Message);


	}
}
