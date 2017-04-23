using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fuuko.Classifiers;
using Fuuko.Models.ParameterModels;
using Fuuko.Models.RequestModels;

namespace Fuuko {

	/// <summary>
	/// Http fluent interface.
	/// </summary>
	public interface IHttFluentRequest {

		/// <summary>
		/// Settings.
		/// </summary>
		RequestSettingsModel Settings {
			get;
		}

		/// <summary>
		/// Url.
		/// </summary>
		/// <param name="url">Url.</param>
		IHttFluentRequest Url ( string url );

		/// <summary>
		/// Extra parameter uri.
		/// </summary>
		/// <param name="extraParameterUri">Value.</param>
		/// <remarks>
		/// Example 
		///      -    /1   for  full url http://test.com/1/
		///      -    /aurora/profile   for  full url http://socialnetwork.com/aurora/profile/
		/// </remarks>
		IHttFluentRequest ExtraParameterUri ( string extraParameterUri );

		/// <summary>
		/// Method.
		/// </summary>
		/// <param name="method">Method.</param>
		IHttFluentRequest Method ( RequestMethod method );

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		IHttFluentRequest Parameter ( string name , string value );

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		IHttFluentRequest Parameter ( string name , int value );

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		IHttFluentRequest Parameter ( string name , Stream value );

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="filePath">File path.</param>
		IHttFluentRequest Parameter ( string name , string filePath , string fileName );


		/// <summary>
		/// Clear parameters.
		/// </summary>
		IHttFluentRequest ClearParameters ();

		/// <summary>
		/// Content type header.
		/// </summary>
		/// <param name="type"></param>
		IHttFluentRequest ContentType ( string type );

		/// <summary>
		/// Content lenght.
		/// </summary>
		/// <param name="contentLength">Content lenght.</param>
		IHttFluentRequest ContentLength ( long contentLength );

		/// <summary>
		/// Referer.
		/// </summary>
		/// <param name="referer">Referer.</param>
		IHttFluentRequest Referer ( string referer );

		/// <summary>
		/// Accept.
		/// </summary>
		/// <param name="accepts">Accept sequence.</param>
		IHttFluentRequest Accept ( IEnumerable<string> accepts );

		/// <summary>
		/// Accept encodings.
		/// </summary>
		/// <param name="encodings">Encodings.</param>
		IHttFluentRequest AcceptEncodings ( IEnumerable<AcceptEncoding> encodings );

		/// <summary>
		/// Accept charsets.
		/// </summary>
		/// <param name="charsets">Charsets.</param>
		IHttFluentRequest AcceptCharsets ( IEnumerable<Encoding> charsets );

		/// <summary>
		/// Accept language.
		/// </summary>
		/// <param name="locales">Locales.</param>
		IHttFluentRequest AcceptLanguage ( IEnumerable<CultureInfo> locales );

		/// <summary>
		/// Allows a 304 Not Modified to be returned if content is unchanged.
		/// </summary>
		/// <param name="date"></param>
		IHttFluentRequest IfModifiedSince ( DateTimeOffset date );

		/// <summary>
		/// Origin.
		/// </summary>
		/// <param name="origin">Origin.</param>
		IHttFluentRequest Origin ( string origin );

		/// <summary>
		/// Accept datetime.
		/// </summary>
		/// <param name="date">Date.</param>
		IHttFluentRequest AcceptDatetime ( DateTimeOffset date );

		/// <summary>
		/// From e-mail address.
		/// </summary>
		/// <param name="email">E-mail address.</param>
		IHttFluentRequest From ( string email );

		/// <summary>
		/// Host.
		/// </summary>
		/// <param name="host">Host.</param>
		IHttFluentRequest Host ( string host );

		/// <summary>
		/// Date.
		/// </summary>
		/// <param name="date">Date.</param>
		IHttFluentRequest Date ( DateTimeOffset date );

		/// <summary>
		/// User agent.
		/// </summary>
		/// <param name="userAgent">User agent.</param>
		IHttFluentRequest UserAgent ( string userAgent );

		/// <summary>
		/// Timeout.
		/// </summary>
		/// <param name="timeout">Timeout.</param>
		IHttFluentRequest Timeout ( TimeSpan timeout );

		/// <summary>
		/// Cookie header.
		/// </summary>
		/// <param name="values">Cookie values.</param>
		/// <param name="domain">Domain.</param>
		/// <param name="path">Path.</param>
		/// <param name="secure">Secure.</param>
		/// <param name="expires">Expires.</param>
		IHttFluentRequest Cookie ( IDictionary<string , string> values , string domain , string path , bool secure , DateTime expires );

		/// <summary>
		/// Clear cookie.
		/// </summary>
		IHttFluentRequest ClearCookie ();

		/// <summary>
		/// Send request.
		/// </summary>
		/// <returns>Response.</returns>
		IHttpResponse Send ();

		/// <summary>
		/// Send asynchronized.
		/// </summary>
		/// <returns>Response.</returns>
		Task<IHttpResponse> SendAsync ( CancellationToken cancellationToken = default(CancellationToken) );

	}

}
