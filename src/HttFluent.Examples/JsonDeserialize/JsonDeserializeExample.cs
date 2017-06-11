using System;
using System.Threading.Tasks;
using Fuuko.Classifiers;
using Fuuko.Implementations;
using Fuuko.Readers.Implementations;
using Fuuko.Extensions;

namespace Fuuko.Examples.JsonDeserialize {

	/// <summary>
	/// Deserialize response from JSON format to 
	/// </summary>
	public class JsonDeserializeExample : Example {

		private class Post {

			public int UserId
			{
				get;
				set;
			}

			public int Id
			{
				get;
				set;
			}

			public string Title
			{
				get;
				set;
			}


			public string Body
			{
				get;
				set;
			}

		}

		public override void Execute () {
			//Create instance HttFluentRequest and pass into first parameter NetHttpBroker.

			var post = HttpFluentRequestFactory.CreateRequest ()
				.Url ( "https://jsonplaceholder.typicode.com/posts/1" ) //define api url
				.Method ( RequestMethod.Get )
				.Send ()
				.GetContentAsObject ( new JsonResponseReader<Post> () ); // define HTTP method

			Console.WriteLine ( "Post" );
			Console.WriteLine ( "User id: " + post.UserId );
			Console.WriteLine ( "Id: " + post.Id );
			Console.WriteLine ( "Title: " + post.Title );
			Console.WriteLine ( "Body: " + post.Body );
		}

		public override async Task ExecuteAsync () {
			//Create instance HttFluentRequest and pass into first parameter NetHttpBroker.

			var response = (HttpResponse) await HttpFluentRequestFactory.CreateRequest ()
				.Url ( "https://jsonplaceholder.typicode.com/posts/1" ) //define api url
				.Method ( RequestMethod.Get )
				.SendAsync ();
			var post = response.ToJson<Post> (); // there is extension method in shorthand but require type cast to HttpResponse

			Console.WriteLine ( "Post" );
			Console.WriteLine ( "User id: " + post.UserId );
			Console.WriteLine ( "Id: " + post.Id );
			Console.WriteLine ( "Title: " + post.Title );
			Console.WriteLine ( "Body: " + post.Body );
		}

	}

}
