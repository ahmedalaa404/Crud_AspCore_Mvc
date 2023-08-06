
using CrudOperations.ConfigAppSetting;
using Dal_CrudOperations.DomainModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;



namespace CrudOperations.Helper
{
	public class EmailSettings : ImailSettings
	{
		private readonly mailSetting _options;
		#region Old Functionality
		//public static  void SendEmail(Email Model)
		//{
		//	var client = new SmtpClient("smtp.gmail.com", 587);   // LeagcyEmail   //host and Port;
		//	client.EnableSsl = true;
		//	client.Credentials = new NetworkCredential("ahmedalaayassin6@gmail.com", "dunlftgznketuptb");
		//	 client.Send("ahmedalaayassin6@gmail.com",Model.To,Model.Subject,Model.Body);





		//}
		#endregion

		public EmailSettings(IOptions<mailSetting> options)
		{
			_options = options.Value;
		}

		public void SendMail(Email model)
		{

			var Mail = new MimeMessage
			{
				Sender = MailboxAddress.Parse(_options.Email),
				Subject = model.Subject,



			};
			Mail.To.Add(MailboxAddress.Parse(model.To));
			var Builder = new BodyBuilder();
			Builder.TextBody = model.Body;
			Mail.Body = Builder.ToMessageBody();
			Mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

			using var smtpProvieder = new SmtpClient();
			smtpProvieder.Connect(_options.Host, _options.Port, SecureSocketOptions.StartTls);
			smtpProvieder.Authenticate(_options.Email, _options.password);
			smtpProvieder.Disconnect ( true);
		}



	}
}
