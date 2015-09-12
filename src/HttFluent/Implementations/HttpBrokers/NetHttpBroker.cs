﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HttFluent.Classifiers;
using HttFluent.Models.ParameterModels;
using HttFluent.Models.RequestModels;
using HttFluent.Models.ResponseModels;

namespace HttFluent.Implementations.HttpBrokers {

	/// <summary>
	/// Net http broker.
	/// </summary>
	public class NetHttpBroker : IHttpBroker {

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
			if ( requestSettings.UserAgent != null ) {
				client.DefaultRequestHeaders.UserAgent.Add ( new ProductInfoHeaderValue ( requestSettings.UserAgent , "" ) );
			}
			if ( !string.IsNullOrEmpty ( requestSettings.Referer ) ) {
				client.DefaultRequestHeaders.Referrer = new Uri ( requestSettings.Referer );
			}
			client.Timeout = requestSettings.Timeout;

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
		private ContentDispositionModel GetContentDisposition ( HttpContentHeaders contentHeaders ) {
			if ( contentHeaders.ContentDisposition == null ) {
				return new ContentDispositionModel {
					Type = ContentDispositionType.NotDefined
				};
			}

			var model = new ContentDispositionModel ();
			switch ( contentHeaders.ContentDisposition.DispositionType ) {
				case "attachment":
					model.Type = ContentDispositionType.Attachment;
					model.Value = contentHeaders.ContentDisposition.FileName;
					break;
				case "inline":
					model.Type = ContentDispositionType.Inline;
					break;
				default:
					model.Type = ContentDispositionType.DispExtType;
					model.CustomKey = contentHeaders.ContentDisposition.Name;
					//TODO:Multiple values supported
					break;
			}

			return model;
		}

		/// <summary>
		/// Create response.
		/// </summary>
		/// <param name="responseMessage">Response message.</param>
		/// <returns></returns>
		private async Task<ResponseModel> CreateResponse ( HttpResponseMessage responseMessage ) {
			return new ResponseModel {
				StatusCode = (int) responseMessage.StatusCode ,
				ProtocolVersion = responseMessage.Version ,
				Age = responseMessage.Headers.Age ,
				ContentDisposition = GetContentDisposition ( responseMessage.Content.Headers ) ,
				ContentType = responseMessage.Content.Headers.ContentType != null ? responseMessage.Content.Headers.ContentType.MediaType : "" ,
				ContentLength = responseMessage.Content.Headers.ContentLength ?? 0 ,
				Content = await responseMessage.Content.ReadAsStreamAsync ()
			};
		}

		/// <summary>
		/// Prepare sender.
		/// </summary>
		/// <param name="client">Client.</param>
		/// <param name="requestSettings">Request settings.</param>
		/// <returns></returns>
		private Task<HttpResponseMessage> PrepareSender ( HttpClient client , RequestSettingsModel requestSettings ) {
			switch ( requestSettings.Method ) {
				case RequestMethod.Get:
				case RequestMethod.Delete:
					var url = requestSettings.Url.ToString ();
					if ( requestSettings.Parameters.Any () ) {
						var parameters = requestSettings.Parameters
							.Select (
								( parameter ) => {
									return string.Format ( "{0}={1}" , parameter.Name , GetParameterValue ( parameter ) );
								}
							)
							.ToList ();
						url = string.Format ( "{0}?{1}" , url , string.Join ( "&" , parameters ) );
					}
					return client.GetAsync ( url );
				case RequestMethod.Put:
				case RequestMethod.Post:
					var bodyContent = CreateBodyContent ( requestSettings );
					bodyContent.Headers.ContentType = new MediaTypeHeaderValue ( requestSettings.ContentType );
					return client.PostAsync ( requestSettings.Url , bodyContent );
				default:
					throw new NotSupportedException ( "Request method not supported." );
			}
		}

		/// <summary>
		/// Get parameter value.
		/// </summary>
		/// <param name="parameter">Parameter.</param>
		/// <returns></returns>
		private static string GetParameterValue ( RequestParameterModel parameter ) {
			var value = "";
			if ( parameter is RequestStringParameterModel ) {
				value = ( parameter as RequestStringParameterModel ).Value;
			}
			if ( parameter is RequestNumberParameterModel ) {
				value = ( parameter as RequestNumberParameterModel ).Value.ToString ();
			}
			return value;
		}

		/// <summary>
		/// Create body content.
		/// </summary>
		/// <param name="requestSettings">Request settings.</param>
		/// <param name="content">Content.</param>
		private static HttpContent CreateBodyContent ( RequestSettingsModel requestSettings ) {
			if ( requestSettings.Parameters.Any ( a => a is RequestPlainBodyParameterModel ) ) {
				var parameter = requestSettings.Parameters.First ( a => a is RequestPlainBodyParameterModel );
				return new StreamContent ( ( parameter as RequestPlainBodyParameterModel ).Content );
			}
			if ( requestSettings.Parameters.Any ( a => a is RequestFileParameterModel ) ) {
				var content = new MultipartFormDataContent ();
				var otherParameters = requestSettings.Parameters.Where ( a => !( a is RequestFileParameterModel ) );
				foreach ( var parameter in otherParameters ) {
					content.Add ( new StringContent ( GetParameterValue ( parameter ) ) , parameter.Name );
				}
				var files = requestSettings.Parameters.Where ( a => a is RequestFileParameterModel );
				foreach ( var file in files ) {
					var fileParameter = file as RequestFileParameterModel;
					var fileName = string.IsNullOrEmpty ( fileParameter.FileName ) ? Path.GetFileName ( fileParameter.FilePath ) : fileParameter.FileName;
					content.Add ( new StreamContent ( File.OpenRead ( fileParameter.FilePath ) ) , fileParameter.Name , fileName );
				}
				return content;
			}
			else {
				return new FormUrlEncodedContent (
					requestSettings.Parameters
						.Select (
							( parameter ) => {
								return new KeyValuePair<string , string> ( parameter.Name , GetParameterValue ( parameter ) );
							}
						)
						.ToList ()
				);
			}
		}

		/// <summary>
		/// Send request.
		/// </summary>
		/// <param name="requestSettings">Request settings.</param>
		/// <returns>Response model.</returns>
		public ResponseModel SendRequest ( RequestSettingsModel requestSettings ) {
			using ( var clientHandler = new HttpClientHandler () )
			using ( var client = new HttpClient ( clientHandler ) ) {
				PrepareRequest ( client , clientHandler , requestSettings );

				var sender = PrepareSender ( client , requestSettings );
				Task.WaitAll ( new List<Task> { sender }.ToArray () );
				var task = CreateResponse ( sender.Result );
				task.Wait ();
				return task.Result;
			}
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

				var response = await PrepareSender ( client , requestSettings );

				return await CreateResponse ( response );
			}
		}

	}

}
