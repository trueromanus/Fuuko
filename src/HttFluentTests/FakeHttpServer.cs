using System;
using System.Net;
using System.Threading.Tasks;

namespace HttFluentTests {

	/// <summary>
	/// Fake http server.
	/// </summary>
	public class FakeHttpServer : IDisposable {

		private HttpListener listener = new HttpListener ();

		/// <summary>
		/// Constructor with port.
		/// </summary>
		/// <param name="port">Listening port.</param>
		public FakeHttpServer ( int port , Action<HttpListenerRequest , HttpListenerResponse> callback ) {
			listener.Prefixes.Add ( string.Format ( "http://127.0.0.1:{0}/" , port ) );
			listener.Start ();

			Task.Run (
				async () => {
					while ( true ) {
						if ( !listener.IsListening ) break;

						var context = await listener.GetContextAsync ();

						callback ( context.Request , context.Response );

						context.Response.OutputStream.Close ();
					}
				}
			);
		}

		/// <summary>
		/// Сlean for listener.
		/// </summary>
		public void Dispose () {
			if ( listener.IsListening ) listener.Stop ();
		}

	}

}
