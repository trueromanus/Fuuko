using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fuuko.Classifiers;

namespace Fuuko {

	/// <summary>
	/// Http fluent interface.
	/// </summary>
	public interface IHttpFluentRequest {

		/// <summary>
		/// Url.
		/// </summary>
		/// <param name="url">Url.</param>
		IHttpFluentRequest Url ( string url );

		/// <summary>
		/// Extra parameter uri.
		/// </summary>
		/// <param name="extraParameterUri">Value.</param>
		/// <remarks>
		/// Example 
		///      -    /1   for  full url http://test.com/1/
		///      -    /aurora/profile   for  full url http://socialnetwork.com/aurora/profile/
		/// </remarks>
		IHttpFluentRequest ExtraParameterUri ( string extraParameterUri );

		/// <summary>
		/// Method.
		/// </summary>
		/// <param name="method">Method.</param>
		IHttpFluentRequest Method ( RequestMethod method );

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		IHttpFluentRequest Parameter ( string name , string value );

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		IHttpFluentRequest Parameter ( string name , int value );

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		IHttpFluentRequest Parameter ( string name , Stream value );

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="filePath">File path.</param>
		IHttpFluentRequest Parameter ( string name , string filePath , string fileName );

		/// <summary>
		/// Remove parameter.
		/// </summary>
		/// <param name="name">Name parameter.</param>
		void RemoveParameter ( string name );

		/// <summary>
		/// Remove parameters.
		/// </summary>
		/// <param name="names">Names sequence.</param>
		void RemoveParameters ( IEnumerable<string> names );

		/// <summary>
		/// Clear parameters.
		/// </summary>
		IHttpFluentRequest ClearParameters ();

		/// <summary>
		/// Content type header.
		/// </summary>
		/// <param name="type"></param>
		IHttpFluentRequest ContentType ( string type );

		/// <summary>
		/// Content lenght.
		/// </summary>
		/// <param name="contentLength">Content lenght.</param>
		IHttpFluentRequest ContentLength ( long contentLength );

		/// <summary>
		/// Referer.
		/// </summary>
		/// <param name="referer">Referer.</param>
		IHttpFluentRequest Referer ( string referer );

		/// <summary>
		/// Accept.
		/// </summary>
		/// <param name="accepts">Accept sequence.</param>
		IHttpFluentRequest Accept ( IEnumerable<string> accepts );

		/// <summary>
		/// Accept encodings.
		/// </summary>
		/// <param name="encodings">Encodings.</param>
		IHttpFluentRequest AcceptEncodings ( IEnumerable<AcceptEncoding> encodings );

		/// <summary>
		/// Accept charsets.
		/// </summary>
		/// <param name="charsets">Charsets.</param>
		IHttpFluentRequest AcceptCharsets ( IEnumerable<Encoding> charsets );

		/// <summary>
		/// Accept language.
		/// </summary>
		/// <param name="locales">Locales.</param>
		IHttpFluentRequest AcceptLanguage ( IEnumerable<CultureInfo> locales );

		/// <summary>
		/// Allows a 304 Not Modified to be returned if content is unchanged.
		/// </summary>
		/// <param name="date"></param>
		IHttpFluentRequest IfModifiedSince ( DateTimeOffset date );

		/// <summary>
		/// Origin.
		/// </summary>
		/// <param name="origin">Origin.</param>
		IHttpFluentRequest Origin ( string origin );

		/// <summary>
		/// Accept datetime.
		/// </summary>
		/// <param name="date">Date.</param>
		IHttpFluentRequest AcceptDatetime ( DateTimeOffset date );

		/// <summary>
		/// From e-mail address.
		/// </summary>
		/// <param name="email">E-mail address.</param>
		IHttpFluentRequest From ( string email );

		/// <summary>
		/// Host.
		/// </summary>
		/// <param name="host">Host.</param>
		IHttpFluentRequest Host ( string host );

		/// <summary>
		/// Date.
		/// </summary>
		/// <param name="date">Date.</param>
		IHttpFluentRequest Date ( DateTimeOffset date );

		/// <summary>
		/// User agent.
		/// </summary>
		/// <param name="userAgent">User agent.</param>
		IHttpFluentRequest UserAgent ( string userAgent );

		/// <summary>
		/// Timeout.
		/// </summary>
		/// <param name="timeout">Timeout.</param>
		IHttpFluentRequest Timeout ( TimeSpan timeout );

		/// <summary>
		/// Add cookies.
		/// </summary>
		/// <param name="values">Cookie values.</param>
		IHttpFluentRequest Cookie ( IDictionary<string , string> values );

		/// <summary>
		/// Add cookie.
		/// </summary>
		/// <param name="name">Cookie name.</param>
		/// <param name="value">Cookie value.</param>
		IHttpFluentRequest Cookie ( string name , string value );

		/// <summary>
		/// Remove cookie.
		/// </summary>
		/// <param name="name">Name.</param>
		void RemoveCookie ( string name );

		/// <summary>
		/// Remove cookie.
		/// </summary>
		/// <param name="names">Names.</param>
		void RemoveCookie ( IEnumerable<string> names );

		/// <summary>
		/// Clear cookie.
		/// </summary>
		IHttpFluentRequest ClearCookie ();

		/// <summary>
		/// Send request.
		/// </summary>
		/// <returns>Response.</returns>
		IHttpResponse Send ();

		/// <summary>
		/// Send asynchronized.
		/// </summary>
		/// <returns>Response.</returns>
		Task<IHttpResponse> SendAsync ( CancellationToken cancellationToken = default ( CancellationToken ) );

	}

}
