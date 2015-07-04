﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttFluent.Classifiers;
using HttFluent.Models.CookieModels;
using HttFluent.Models.ParameterModels;
using HttFluent.Models.RequestModels;

namespace HttFluent.Implementations {

	/// <summary>
	/// Request based on <see cref="HttpClient"/> class.
	/// </summary>
	public class HttFluentRequest : IHttFluentRequest {

		private readonly RequestSettingsModel m_RequestSettings = new RequestSettingsModel ();

		private readonly IHttpBroker m_HttpBroker;

		/// <summary>
		/// Constructor injection.
		/// </summary>
		/// <param name="httpBroker">Http broker.</param>
		public HttFluentRequest ( IHttpBroker httpBroker ) {
			Contract.Requires ( httpBroker != null );
			if ( httpBroker == null ) throw new ArgumentNullException ( "httpBroker" );

			m_HttpBroker = httpBroker;
		}

		/// <summary>
		/// Settings.
		/// </summary>
		public RequestSettingsModel Settings {
			get {
				return m_RequestSettings;
			}
		}

		/// <summary>
		/// Set url.
		/// </summary>
		/// <param name="url">Url.</param>
		public IHttFluentRequest Url ( string url ) {
			Contract.Requires ( url != null );
			if ( url == null ) throw new ArgumentNullException ( "value" );

			m_RequestSettings.Url = new Uri ( url );

			return this;
		}

		/// <summary>
		/// Method.
		/// </summary>
		/// <param name="method">Method.</param>
		public IHttFluentRequest Method ( RequestMethod method ) {
			m_RequestSettings.Method = method;

			return this;
		}

		/// <summary>
		/// Parameters.
		/// </summary>
		/// <param name="parameters">Request parameters.</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public IHttFluentRequest Parameters ( IEnumerable<RequestParameterModel> parameters ) {
			Contract.Requires ( parameters != null );
			if ( parameters == null ) throw new ArgumentNullException ( "parameters" );
			if ( !parameters.Any () ) throw new ArgumentException ( "Parameters sequence is empty." );

			m_RequestSettings.Parameters = parameters;

			return this;
		}

		/// <summary>
		/// Content type (Content-Type header).
		/// </summary>
		/// <param name="type">Content type in string respresent.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttFluentRequest ContentType ( string type ) {
			Contract.Requires ( type != null );
			if ( type == null ) throw new ArgumentNullException ( "type" );

			m_RequestSettings.ContentType = type;

			return this;
		}

		/// <summary>
		/// Content length (Content-Length header).
		/// </summary>
		/// <param name="contentLength">Content length in long digit.</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public IHttFluentRequest ContentLength ( long contentLength ) {
			Contract.Requires ( contentLength > -1 );
			if ( contentLength < 0 ) throw new ArgumentOutOfRangeException ( "contentLength" );

			m_RequestSettings.ContentLength = contentLength;

			return this;
		}

		/// <summary>
		/// Referer.
		/// </summary>
		/// <param name="referer">Referer.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttFluentRequest Referer ( string referer ) {
			Contract.Requires ( referer != null );
			if ( referer == null ) throw new ArgumentNullException ( "referer" );

			m_RequestSettings.Referer = referer;

			return this;
		}

		/// <summary>
		/// Accept.
		/// </summary>
		/// <param name="accept">Accept.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttFluentRequest Accept ( IEnumerable<string> accepts ) {
			Contract.Requires ( accepts != null );
			Contract.Requires ( accepts.Count () > 0 );
			if ( accepts == null ) throw new ArgumentNullException ( "accepts" );
			if ( accepts.Count () == 0 ) throw new ArgumentException ( "Accepts sequence is empty." );

			m_RequestSettings.Accepts = accepts;

			return this;
		}

		/// <summary>
		/// Accept charset.
		/// </summary>
		/// <param name="encodings">Encoding.</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public IHttFluentRequest AcceptEncodings ( IEnumerable<AcceptEncoding> encodings ) {
			Contract.Requires ( encodings != null );
			Contract.Requires ( encodings.Count () > 0 );
			if ( encodings == null ) throw new ArgumentNullException ( "encodings" );
			if ( encodings.Count () == 0 ) throw new ArgumentException ( "Encodings sequence is empty." );

			m_RequestSettings.Encodings = encodings;

			return this;
		}

		/// <summary>
		/// Accept language (Accept-Language header).
		/// </summary>
		/// <param name="locales"></param>
		/// <returns></returns>
		public IHttFluentRequest AcceptLanguage ( IEnumerable<CultureInfo> locales ) {
			Contract.Requires ( locales != null );
			Contract.Requires ( locales.Count () > 0 );
			if ( locales == null ) throw new ArgumentNullException ( "locales" );
			if ( locales.Count () == 0 ) throw new ArgumentException ( "Locales sequence is empty." );

			m_RequestSettings.Locales = locales;

			return this;
		}

		/// <summary>
		/// If modified since (If-Modified-Since header).
		/// </summary>
		/// <param name="date">Date.</param>
		public IHttFluentRequest IfModifiedSince ( DateTime date ) {
			Contract.Ensures ( m_RequestSettings.IfModifiedSince == date );

			m_RequestSettings.IfModifiedSince = date;

			return this;
		}

		/// <summary>
		/// Origin header.
		/// </summary>
		/// <param name="origin">Origin</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttFluentRequest Origin ( string origin ) {
			Contract.Requires ( origin != null );
			Contract.Ensures ( m_RequestSettings.Origin == origin );
			if ( origin == null ) throw new ArgumentNullException ( "origin" );

			m_RequestSettings.Origin = origin;

			return this;
		}

		/// <summary>
		/// Accept-Datetime header.
		/// </summary>
		/// <param name="date">Date.</param>
		public IHttFluentRequest AcceptDatetime ( DateTime date ) {
			Contract.Ensures ( m_RequestSettings.AcceptDatetime == date );

			m_RequestSettings.AcceptDatetime = date;

			return this;
		}

		/// <summary>
		/// From header.
		/// </summary>
		/// <param name="email">Email address sender.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttFluentRequest From ( string email ) {
			Contract.Requires ( email != null );
			Contract.Ensures ( m_RequestSettings.FromEmail == email );
			if ( email == null ) throw new ArgumentNullException ( "email" );

			m_RequestSettings.FromEmail = email;

			return this;
		}

		/// <summary>
		/// Host header.
		/// </summary>
		/// <param name="host">Host client.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttFluentRequest Host ( string host ) {
			Contract.Requires ( host != null );
			Contract.Ensures ( m_RequestSettings.Host == host );
			if ( host == null ) throw new ArgumentNullException ( "host" );

			m_RequestSettings.Host = host;

			return this;
		}

		/// <summary>
		/// Date header.
		/// </summary>
		/// <param name="date">Date.</param>
		public IHttFluentRequest Date ( DateTime date ) {
			Contract.Ensures ( m_RequestSettings.Date == date );

			m_RequestSettings.Date = date;

			return this;
		}

		/// <summary>
		/// User-Agent header.
		/// </summary>
		/// <param name="userAgent">User agent.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttFluentRequest UserAgent ( string userAgent ) {
			Contract.Requires ( userAgent != null );
			Contract.Ensures ( m_RequestSettings.UserAgent == userAgent );
			if ( userAgent == null ) throw new ArgumentNullException ( "userAgent" );

			m_RequestSettings.UserAgent = userAgent;

			return this;
		}

		/// <summary>
		/// Cookie header.
		/// </summary>
		/// <param name="values">Values.</param>
		public IHttFluentRequest Cookie ( IDictionary<string , string> values ) {
			Contract.Requires ( values != null );
			Contract.Requires ( values.Count () > 0 );
			Contract.Ensures ( m_RequestSettings.Cookies.Count () == values.Count );
			if ( values == null ) throw new ArgumentNullException ( "locales" );
			if ( values.Count () == 0 ) throw new ArgumentException ( "Cookie values sequence is empty." );

			//in cookie used default settings: expires +7 days, domain empty, path / and secure disabled.
			m_RequestSettings.Cookies = values
				.Select (
					cookie => new CookieModel {
						Name = cookie.Key ,
						Value = cookie.Value ,
						Secure = false ,
						Path = "/" ,
						Expires = DateTime.Now.AddDays ( 7 ) ,
						Domain = ""
					}
				)
				.ToList ();

			return this;
		}

		/// <summary>
		/// Accept charsets.
		/// </summary>
		/// <param name="charsets">Charsets.</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public IHttFluentRequest AcceptCharsets ( IEnumerable<Encoding> charsets ) {
			Contract.Requires ( charsets != null );
			Contract.Requires ( charsets.Count () > 0 );
			Contract.Ensures ( m_RequestSettings.Charsets.Count () == charsets.Count () );
			if ( charsets == null ) throw new ArgumentNullException ( "charsets" );
			if ( charsets.Count () == 0 ) throw new ArgumentException ( "Locales sequence is empty." );

			m_RequestSettings.Charsets = charsets;

			return this;
		}

		/// <summary>
		/// Send request.
		/// </summary>
		/// <returns>Response.</returns>
		public IHttpResponse Send () {
			var response = m_HttpBroker.SendRequest ( m_RequestSettings );

			return new HttpResponse ( response );
		}

		/// <summary>
		/// Send request asynchronized.
		/// </summary>
		/// <returns>Response.</returns>
		public async Task<IHttpResponse> SendAsync () {
			var response = await m_HttpBroker.SendRequestAsync ( m_RequestSettings );

			return new HttpResponse ( response );
		}

	}

}
