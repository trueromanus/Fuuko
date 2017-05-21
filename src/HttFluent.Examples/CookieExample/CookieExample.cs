using System;
using System.Threading.Tasks;
using Fuuko.Classifiers;
using Fuuko.Implementations;
using Fuuko.Implementations.HttpBrokers;

namespace Fuuko.Examples.CookieExample {

	/// <summary>
	/// Cookie example.
	/// </summary>
	public class CookieExample : Example {

		/// <summary>
		/// Execute asynchronize.
		/// </summary>
		public override void Execute () {
			var response = GetRequest ().Send ();

			Console.WriteLine ( $"Status code {response.Data.StatusCode} from server posttestserver.com" );
		}

		/// <summary>
		/// Execute asynchronized.
		/// </summary>
		public override async Task ExecuteAsync () {
			var response = await GetRequest ().SendAsync ();

			Console.WriteLine ( $"Status code {response.Data.StatusCode} from server posttestserver.com" );
		}

		/// <summary>
		/// Get request.
		/// </summary>
		/// <returns></returns>
		private static IHttpFluentRequest GetRequest () {
			//Create instance HttFluentRequest and pass into first parameter NetHttpBroker.

			return new HttpFluentRequest ( new NetHttpBroker () )
				.Url ( "http://google.ru/" ) //define full url
				.Method ( RequestMethod.Get ) // define HTTP method
				.Cookie ( "test" , "somevalue" ); //define cookie parameter

		}
	}
}
