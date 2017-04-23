using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Fuuko.Classifiers;
using Fuuko.Implementations;
using Fuuko.Implementations.HttpBrokers;

namespace Fuuko.Examples.BasicExample {

	/// <summary>
	/// Google search example.
	/// </summary>
	/// <remarks>
	/// Perform google search and pass result to console output.
	/// </remarks>
	public class GoogleSearchExample : Example {

		/// <summary>
		/// Execute request synchronized.
		/// </summary>
		public override void Execute () {
			var response = GetRequest ().Send ();

			//output to console some response properties

			WriteResult ( response );
		}

		/// <summary>
		/// Execute request asynchronized.
		/// </summary>
		public override async Task ExecuteAsync () {
			var response = await GetRequest ().SendAsync ();

			//output to console some response properties

			WriteResult ( response );
		}

		private static IHttFluentRequest GetRequest() {
			//Create instance HttFluentRequest and pass into first parameter NetHttpBroker.

			return new HttpFluentRequest ( new NetHttpBroker () )
				.Url ( "https://www.google.ru/webhp" ) //define full url
				.Method ( RequestMethod.Get ) // define HTTP method
				.Parameter ( "q" , "cats" ) // define parameter q=cats
				.Parameter ( "ie" , "UTF-8" ); // define parameter ie=UTF-8;
		}

		private static void WriteResult ( IHttpResponse response ) {
			Console.WriteLine ( "Content-Type: {0}" , response.Data.ContentType );
			Console.WriteLine ( "Content-Length: {0}" , response.Data.ContentLength );
			Console.WriteLine ( "Status: {0}" , response.Data.StatusCode );
			Console.WriteLine ( "Protocol version: {0}" , response.Data.ProtocolVersion );
			//this encoding only for cyrillic Windows
			Console.WriteLine ( "Content: {0}" , response.GetContentAsString ( Encoding.GetEncoding ( 1251 ) ) );
		}

	}

}
