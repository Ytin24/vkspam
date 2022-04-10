using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.SafetyEnums;
using Microsoft.Extensions.DependencyInjection;
//@ytin24

namespace Vkspam
{
	class Program
	{
		public static void Main()
		{
			Vk vk = new Vk();
			vk.Auth();

		}
	}
	class Vk 
	{
		
		public void Auth()
        {
			var services = new ServiceCollection();
			services.AddAudioBypass();
			VkApi api = new VkApi(services);
			string login, password;
			Console.WriteLine("Введите Логин");
			login = Console.ReadLine();
			Console.WriteLine("Введите Пароль");
			password = Console.ReadLine();

			{
				

				api.Authorize(new ApiAuthParams
				{
					Login = login,
					Password = password,
					Settings = Settings.All,
					 TwoFactorAuthorization = () =>
					 {
						 Console.WriteLine("Enter Code:");
						 return Console.ReadLine();
					 }
				});
				Console.Clear();
				StartSpam(api);
			}
		}
		private void StartSpam(VkApi api)
        {

			Console.WriteLine("Введите UserID Жертвы");
			int userid = int.Parse(Console.ReadLine());
			Console.WriteLine("Введите сообщение");
			string msg = Console.ReadLine();
			Console.WriteLine("Введите количество повторов");
			int tries = int.Parse(Console.ReadLine());
			for (int i = 1; i < tries++; i++)
			{
				Console.WriteLine(i);
				api.Messages.Send(new MessagesSendParams() 
				{ 
					UserId = userid,
					RandomId = 0,
					Message = msg
				});
			}
        }
    }
}