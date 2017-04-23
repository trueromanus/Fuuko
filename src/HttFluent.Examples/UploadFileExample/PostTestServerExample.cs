using System;
using System.Threading.Tasks;
using Fuuko.Classifiers;
using Fuuko.Implementations;
using Fuuko.Implementations.HttpBrokers;

namespace Fuuko.Examples.UploadFileExample {

	/// <summary>
	/// Test upload file to http://posttestserver.com/.
	/// Upload single file with parameter "file".
	/// </summary>
	public class PostTestServerExample : Example {

		public override void Execute () {
			var response = GetRequest ().Send ();

			Console.WriteLine ( $"Status code {response.Data.StatusCode} from server posttestserver.com" );
		}

		public override async Task ExecuteAsync () {
			var response = await GetRequest ().SendAsync ();

			Console.WriteLine ( $"Status code {response.Data.StatusCode} from server posttestserver.com" );
		}

		/// <summary>
		/// Get request.
		/// </summary>
		/// <returns></returns>
		private static IHttFluentRequest GetRequest () {
			//Create instance HttFluentRequest and pass into first parameter NetHttpBroker.

			return new HttFluentRequest ( new NetHttpBroker () )
				.Url ( "http://posttestserver.com/post.php?dir=example" ) //define full url
				.Method ( RequestMethod.Post ) // define HTTP method
				.Parameter ( "file" , @"Assets\testimage.jpg" , "testimage.jpg" ) // define file parameter from file on disk
				.Parameter ( "someParam" , "someValue" ); // define parameter someParam=someValue
		}

	}

}
