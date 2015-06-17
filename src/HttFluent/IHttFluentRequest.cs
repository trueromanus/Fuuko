using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using HttFluent.Classifiers;
using HttFluent.Models.ParameterModels;

namespace HttFluent {

	/// <summary>
	/// Http fluent interface.
	/// </summary>
	public interface IHttFluentRequest {

		/// <summary>
		/// Url.
		/// </summary>
		/// <param name="value">Url.</param>
		void Url ( string value );

		/// <summary>
		/// Method.
		/// </summary>
		/// <param name="method">Method.</param>
		void Method ( RequestMethod method );

		/// <summary>
		/// Parameters.
		/// </summary>
		/// <param name="parameters">Parameters.</param>
		void Parameters ( IEnumerable<RequestParameterModel> parameters );

		/// <summary>
		/// Content type header.
		/// </summary>
		/// <param name="type"></param>
		void ContentType ( string type );

		/// <summary>
		/// Content lenght.
		/// </summary>
		/// <param name="contentLength">Content lenght.</param>
		void ContentLength ( string contentLength );

		/// <summary>
		/// Referer.
		/// </summary>
		/// <param name="referer">Referer.</param>
		void Referer ( string referer );

		/// <summary>
		/// Accept.
		/// </summary>
		/// <param name="accept">Accept.</param>
		void Accept ( string accept );

		/// <summary>
		/// Accept encodings.
		/// </summary>
		/// <param name="encodings">Encodings.</param>
		void AcceptCharset ( IEnumerable<AcceptEncoding> encodings );

		/// <summary>
		/// Accept language.
		/// </summary>
		/// <param name="locales">Locales.</param>
		void AcceptLanguage ( IEnumerable<CultureInfo> locales );

		/// <summary>
		/// Allows a 304 Not Modified to be returned if content is unchanged.
		/// </summary>
		/// <param name="date"></param>
		void IfModifiedSince ( DateTime date );

		/// <summary>
		/// Origin.
		/// </summary>
		/// <param name="origin">Origin.</param>
		void Origin ( string origin );

		/// <summary>
		/// Accept datetime.
		/// </summary>
		/// <param name="date">Date.</param>
		void AcceptDatetime ( DateTime date );

		/// <summary>
		/// From e-mail address.
		/// </summary>
		/// <param name="email">E-mail address.</param>
		void From ( string email );

		/// <summary>
		/// Host.
		/// </summary>
		/// <param name="host">Host.</param>
		void Host ( string host );

		/// <summary>
		/// Date.
		/// </summary>
		/// <param name="date">Date.</param>
		void Date ( DateTime date );

		/// <summary>
		/// User agent.
		/// </summary>
		/// <param name="userAgent">User agent.</param>
		void UserAgent ( string userAgent );

		/// <summary>
		/// Cookie header.
		/// </summary>
		/// <param name="values">Cookie values.</param>
		void Cookie ( IEnumerable<string> values );

		/// <summary>
		/// Send request.
		/// </summary>
		/// <returns>Response.</returns>
		IHttpFluentResponse Send ();

		/// <summary>
		/// Send asynchronized.
		/// </summary>
		/// <returns></returns>
		Task<IHttpFluentResponse> SendAsync ();

	}

}
