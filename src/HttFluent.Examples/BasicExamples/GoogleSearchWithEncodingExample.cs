using System;
using System.Threading.Tasks;
using Fuuko.Classifiers;
using Fuuko.Implementations;
using Fuuko.Implementations.HttpBrokers;

namespace Fuuko.Examples.BasicExamples {

	/// <summary>
	/// Google search with encoding example.
	/// </summary>
	/// <remarks>
	/// In example google search has issue relate to response encoding.
	/// In this example will be executed method response.GetContentAsString without parameter encoding.
	/// Instead used Encoding for stream defined within Http response.
	/// </remarks>
	public class GoogleSearchWithoutEncodingExample : Example {

		public override void Execute () {
			//Create instance HttFluentRequest and pass into first parameter NetHttpBroker.

			var response = CreateRequest().Send (); //send request synchronized

			//output to console some response properties

			WriteToConsole ( response );
		}

		/// <summary>
		/// Write information to console.
		/// </summary>
		/// <param name="response"></param>
		private static void WriteToConsole ( IHttpResponse response ) {
			Console.WriteLine ( "Content-Type: {0}" , response.Data.ContentType );
			Console.WriteLine ( "Content-Length: {0}" , response.Data.ContentLength );
			Console.WriteLine ( "Status: {0}" , response.Data.StatusCode );
			Console.WriteLine ( "Protocol version: {0}" , response.Data.ProtocolVersion );
			Console.WriteLine ( "Content: {0}" , response.GetContentAsString () );//encoding do not need
		}

		public override async Task ExecuteAsync () {
			//Create instance HttFluentRequest and pass into first parameter NetHttpBroker.

			var response = await CreateRequest ().SendAsync();

			//output to console some response properties
			WriteToConsole ( response );
		}

		/// <summary>
		/// Create full request settings.
		/// </summary>
		/// <returns></returns>
		private static IHttFluentRequest CreateRequest () {
			var response = new HttFluentRequest ( new NetHttpBroker () )
				.Url ( "https://www.google.ru/webhp" ) //define full url
				.Method ( RequestMethod.Get ) // define HTTP method
				.Parameter ( "q" , "cats" ) // define parameter q=cats
				.Parameter ( "ie" , "UTF-8" ); // define parameter ie=UTF-8
			return response;
		}

	}

}
