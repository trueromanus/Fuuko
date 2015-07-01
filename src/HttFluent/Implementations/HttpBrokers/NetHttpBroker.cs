using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HttFluent.Classifiers;
using HttFluent.Models.RequestModels;
using HttFluent.Models.ResponseModels;

namespace HttFluent.Implementations.HttpBrokers {

	/// <summary>
	/// Net http broker.
	/// </summary>
	public class NetHttpBroker : IHttpBroker {

		private string ContentDispositionHeader = "Content-Disposition";

		/// <summary>
		/// Prepare request.
		/// </summary>
		/// <param name="client">Client.</param>
		/// <param name="clientHandler">Client handler.</param>
		/// <exception cref="ArgumentNullException"></exception>
		private void PrepareRequest ( HttpClient client , HttpClientHandler clientHandler , RequestSettingsModel requestSettings ) {
			Contract.Requires ( clientHandler != null );
			Contract.Requires ( client != null );
			Contract.Requires ( requestSettings != null );
			if ( clientHandler == null ) throw new ArgumentNullException ( "clientHandler" );
			if ( client == null ) throw new ArgumentNullException ( "client" );
			if ( requestSettings == null ) throw new ArgumentNullException ( "requestSettings" );

			client.DefaultRequestHeaders.Date = requestSettings.Date;
			client.DefaultRequestHeaders.From = requestSettings.FromEmail;
			client.DefaultRequestHeaders.Host = requestSettings.Host;
			client.DefaultRequestHeaders.UserAgent.Add ( new ProductInfoHeaderValue ( new ProductHeaderValue ( requestSettings.UserAgent ) ) );
			if ( !string.IsNullOrEmpty ( requestSettings.Referer ) ) {
				client.DefaultRequestHeaders.Referrer = new Uri ( requestSettings.Referer );
			}

			SetAcceptHeaders ( client , requestSettings );
			SetCookies ( clientHandler , requestSettings );
		}

		/// <summary>
		/// Set cookies.
		/// </summary>
		/// <param name="clientHandler">Client handler.</param>
		/// <param name="requestSettings">Request settings.</param>
		/// <exception cref="ArgumentNullException"></exception>
		private static void SetCookies ( HttpClientHandler clientHandler , RequestSettingsModel requestSettings ) {
			Contract.Requires ( clientHandler != null );
			Contract.Requires ( requestSettings != null );
			if ( clientHandler == null ) throw new ArgumentNullException ( "clientHandler" );
			if ( requestSettings == null ) throw new ArgumentNullException ( "requestSettings" );

			foreach ( var cookie in requestSettings.Cookies ) {
				clientHandler.CookieContainer.Add (
					new Cookie {
						Name = cookie.Name ,
						Path = cookie.Path ,
						Secure = cookie.Secure ,
						Value = cookie.Value ,
						Expires = cookie.Expires ,
						Expired = cookie.Expires < DateTime.Now ,
						Domain = cookie.Domain
					}
				);
			}
		}

		/// <summary>
		/// Set accept handlers.
		/// </summary>
		/// <param name="client">Http client.</param>
		/// <param name="requestSettings">Request settings.</param>
		/// <exception cref="ArgumentNullException"></exception>
		private static void SetAcceptHeaders ( HttpClient client , RequestSettingsModel requestSettings ) {
			Contract.Requires ( client != null );
			Contract.Requires ( requestSettings != null );
			if ( client == null ) throw new ArgumentNullException ( "client" );
			if ( requestSettings == null ) throw new ArgumentNullException ( "requestSettings" );

			foreach ( var accept in requestSettings.Accepts ) {
				client.DefaultRequestHeaders.Accept.Add ( new MediaTypeWithQualityHeaderValue ( accept ) );
			}
			foreach ( var locale in requestSettings.Locales ) {
				var regionInfo = new RegionInfo ( locale.LCID );
				client.DefaultRequestHeaders.AcceptLanguage.Add ( new StringWithQualityHeaderValue ( regionInfo.TwoLetterISORegionName ) );
			}
			foreach ( var encoding in requestSettings.Encodings ) {
				client.DefaultRequestHeaders.AcceptEncoding.Add ( new StringWithQualityHeaderValue ( encoding.ToString () ) );
			}
			foreach ( var encoding in requestSettings.Charsets ) {
				client.DefaultRequestHeaders.AcceptCharset.Add ( new StringWithQualityHeaderValue ( encoding.WebName ) );
			}
		}

		/// <summary>
		/// Get content disposition.
		/// </summary>
		/// <param name="headers">Headers.</param>
		/// <returns>Content disposition model.</returns>
		private ContentDispositionModel GetContentDisposition ( HttpResponseHeaders headers ) {
			if ( !headers.Contains ( ContentDispositionHeader ) ) {
				return new ContentDispositionModel {
					Type = ContentDispositionType.NotDefined
				};
			}
			var header = string.Join ( "\n" , headers.GetValues ( ContentDispositionHeader ) );
			var parameters = header.Split ( ';' );
			var type = parameters.First ();
			var value = "";
			if ( parameters.Length > 1 ) value = parameters.Last ();

			var model = new ContentDispositionModel ();
			switch ( header ) {
				case "attachment":
					model.Type = ContentDispositionType.Attachment;
					break;
				case "inline":
					model.Type = ContentDispositionType.Inline;
					break;
				default:
					model.Type = ContentDispositionType.DispExtType;
					model.CustomKey = type;
					break;
			}

			model.Value = value;

			return model;
		}

		/// <summary>
		/// Create response.
		/// </summary>
		/// <param name="responseMessage">Response message.</param>
		/// <returns></returns>
		private ResponseModel CreateResponse ( HttpResponseMessage responseMessage ) {

			return new ResponseModel {
				StatusCode = (int) responseMessage.StatusCode ,
				ProtocolVersion = responseMessage.Version ,
				Age = responseMessage.Headers.Age ,
				ContentDisposition = GetContentDisposition ( responseMessage.Headers )
			};
		}

		public ResponseModel SendRequest ( RequestSettingsModel requestSettings ) {
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Send request asynchronized.
		/// </summary>
		/// <param name="requestSettings">Request settings.</param>
		/// <returns>Response model.</returns>
		/// <exception cref="NotSupportedException"></exception>
		public async Task<ResponseModel> SendRequestAsync ( RequestSettingsModel requestSettings ) {
			using ( var clientHandler = new HttpClientHandler () )
			using ( var client = new HttpClient ( clientHandler ) ) {
				PrepareRequest ( client , clientHandler , requestSettings );

				switch ( requestSettings.Method ) {
					case RequestMethod.Get:
						var response = await client.GetAsync ( requestSettings.Url );

						break;
					case RequestMethod.Post:
						break;
					case RequestMethod.Put:
					case RequestMethod.Delete:
					case RequestMethod.Undefined:
					default:
						throw new NotSupportedException ( "Request method not supported." );
				}

				return null;
			}
		}

	}

}
