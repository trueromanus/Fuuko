using System;
using HttFluent.Classifiers;
using HttFluent.Implementations;
using HttFluent.Implementations.HttpBrokers;

namespace HttFluent.Examples.BasicExamples {

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
			Console.WriteLine ( "Content: {0}" , response.GetContentAsString () );//encoding do not need
		}

	}

}
