using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using HttFluent.Classifiers;
using HttFluent.Models.ParameterModels;
using HttFluent.Models.RequestModels;

namespace HttFluent {

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
		/// Method.
		/// </summary>
		/// <param name="method">Method.</param>
		IHttFluentRequest Method ( RequestMethod method );

		/// <summary>
		/// Parameters.
		/// </summary>
		/// <param name="parameters">Parameters.</param>
		IHttFluentRequest Parameters ( IEnumerable<RequestParameterModel> parameters );

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
		/// Cookie header.
		/// </summary>
		/// <param name="values">Cookie values.</param>
		IHttFluentRequest Cookie ( IDictionary<string , string> values );

		/// <summary>
		/// Send request.
		/// </summary>
		/// <returns>Response.</returns>
		IHttpResponse Send ();

		/// <summary>
		/// Send asynchronized.
		/// </summary>
		/// <returns></returns>
		Task<IHttpResponse> SendAsync ();

	}

}
