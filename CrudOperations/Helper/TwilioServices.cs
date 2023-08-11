using CrudOperations.Settings;
using Dal_CrudOperations.DomainModel;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CrudOperations.Helper
{
	public class TwilioServices : ITwilio
	{
		private readonly TwilioSettings _twilioManager;

		public TwilioServices(IOptions<TwilioSettings> TwilioManager)
		{
			_twilioManager = TwilioManager.Value;
		}
		public MessageResource send(SmsMessage Message)
		{
			TwilioClient.Init(_twilioManager.AccountSID, _twilioManager.AuthToken);
			var Resulte = MessageResource.Create(
				  body: Message.Body,
				  from: new Twilio.Types.PhoneNumber(_twilioManager.TwiliophonenumberSID),
				  to: Message.NumberPhone
				);
			return Resulte;

		}
	}
}
