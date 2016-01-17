using System;
using System.IO;
using System.Text;
using HttFluent.Classifiers;
using HttFluent.Implementations;
using HttFluent.Implementations.HttpBrokers;

namespace HttFluent.Examples.BasicExample {

	/// <summary>
	/// Google search example.
	/// </summary>
	/// <remarks>
	/// Perform google search and pass result to console output.
	/// </remarks>
	public class GoogleSearchExample : Example {

		public override void Execute () {
			//Create instance HttFluentRequest and pass into first parameter NetHttpBroker.

			var response = new HttFluentRequest ( new NetHttpBroker () )
					.Url ( "https://www.google.ru/webhp" ) //define full url
					.Method ( RequestMethod.Get ) // define HTTP method
					.Parameter ( "q" , "cats" ) // define parameter q=cats
					.Parameter ( "ie" , "UTF-8" ) // define parameter ie=UTF-8
					.Send (); //send request synchronized

			//output to console some response properties

			Console.WriteLine ( "Content-Type: {0}" , response.Response.ContentType );
			Console.WriteLine ( "Content-Length: {0}" , response.Response.ContentLength );
			Console.WriteLine ( "Status: {0}" , response.Response.StatusCode );
			Console.WriteLine ( "Protocol version: {0}" , response.Response.ProtocolVersion );
			Console.WriteLine ( "Content: {0}" , response.GetContentAsString ( Encoding.UTF8 ) );
		}

	}

}
