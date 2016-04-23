using System;
using System.Text;
using System.Threading.Tasks;
using HttFluent.Classifiers;
using HttFluent.Implementations;
using HttFluent.Implementations.HttpBrokers;

namespace HttFluent.Examples.MultipleRequests {

	/// <summary>
	/// Simple multiple request example.
	/// </summary>
	/// <remarks>
	/// If you need to do the http request of two times or more, you may easy to do it.
	/// This example show you that made 33 requests in loop, but changing only extra parameter URI.
	/// </remarks>
	public class SimpleMultipleRequestExample : Example {

		public override void Execute () {
			//Create instance HttFluentRequest and pass into first parameter NetHttpBroker.

			var request = GetRequest ();

			for ( var i = 0 ; i <= 33 ; i++ ) {
				var response = request
					.ExtraParameterUri ( string.Format ( "/{0}/" , i ) )
					.Send (); //send request synchronized

				//output to console content for path of multiple request
				Console.WriteLine ( "Part: {0}" , i );
				Console.WriteLine ( response.GetContentAsString ( Encoding.UTF8 ) );
			}
		}

		/// <summary>
		/// Create request.
		/// </summary>
		private static IHttFluentRequest GetRequest () {
			var request = new HttFluentRequest ( new NetHttpBroker () )
					.Url ( "http://www.thomas-bayer.com/sqlrest/CUSTOMER/" ) //define basic url
					.Method ( RequestMethod.Get ); // define HTTP method
			return request;
		}

		public override async Task ExecuteAsync () {
			var request = GetRequest ();

			for ( var i = 0 ; i <= 33 ; i++ ) {
				var response = await request
					.ExtraParameterUri ( string.Format ( "/{0}/" , i ) )
					.SendAsync (); //send request synchronized

				//output to console content for path of multiple request
				Console.WriteLine ( "Part: {0}" , i );
				Console.WriteLine ( response.GetContentAsString ( Encoding.UTF8 ) );
			}

		}

	}

}
