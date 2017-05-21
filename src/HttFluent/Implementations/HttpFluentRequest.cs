﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fuuko.Classifiers;
using Fuuko.Exceptions;
using Fuuko.Models.CookieModels;
using Fuuko.Models.ParameterModels;
using Fuuko.Models.RequestModels;

namespace Fuuko.Implementations {

	/// <summary>
	/// Request based on <see cref="HttpClient"/> class.
	/// </summary>
	public class HttpFluentRequest : IHttpFluentRequest {

		private readonly RequestSettingsModel m_RequestSettings = new RequestSettingsModel ();

		private readonly IHttpBroker m_HttpBroker;

		/// <summary>
		/// Constructor injection.
		/// </summary>
		/// <param name="httpBroker">Http broker.</param>
		public HttpFluentRequest ( IHttpBroker httpBroker ) {
			Contract.Requires ( httpBroker != null );
			if ( httpBroker == null ) throw new ArgumentNullException ( "httpBroker" );

			m_HttpBroker = httpBroker;
		}

		/// <summary>
		/// Settings.
		/// </summary>
		internal RequestSettingsModel Settings
		{
			get
			{
				return m_RequestSettings;
			}
		}

		/// <summary>
		/// Set url.
		/// </summary>
		/// <param name="url">Url.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest Url ( string url ) {
			Contract.Requires ( url != null );
			if ( url == null ) throw new ArgumentNullException ( "url" );

			m_RequestSettings.Url = new Uri ( url );

			return this;
		}

		/// <summary>
		/// Extra parameter uri.
		/// </summary>
		/// <param name="extraParameterUri">Value.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest ExtraParameterUri ( string extraParameterUri ) {
			Contract.Requires ( extraParameterUri != null );
			if ( extraParameterUri == null ) throw new ArgumentNullException ( "extraParameterUri" );

			m_RequestSettings.ExtraParameterUrl = extraParameterUri;

			return this;
		}

		/// <summary>
		/// Method.
		/// </summary>
		/// <param name="method">Method.</param>
		public IHttpFluentRequest Method ( RequestMethod method ) {
			m_RequestSettings.Method = method;

			return this;
		}

		/// <summary>
		/// Add or update parameter.
		/// </summary>
		/// <typeparam name="T">Concrete type of parameter.</typeparam>
		/// <exception cref="ParameterException"></exception>
		private T GetParameter<T> ( string name ) where T : RequestParameterModel, new() {
			var parameter = m_RequestSettings.Parameters.FirstOrDefault ( a => a.Name == name );
			if ( parameter == null ) {
				var newParameter = new T {
					Name = name
				};
				m_RequestSettings.Parameters.Add ( newParameter );
				return newParameter;
			}

			var existParameter = ( parameter as T );
			if ( existParameter == null ) throw new ParameterException ( string.Format ( "Parameter with name {0} already exists and have another type." , name ) );

			return existParameter;
		}

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest Parameter ( string name , string value ) {
			Contract.Requires ( name != null );
			Contract.Requires ( value != null );
			if ( name == null ) throw new ArgumentNullException ( "name" );
			if ( value == null ) throw new ArgumentNullException ( "value" );

			var parameter = GetParameter<RequestStringParameterModel> ( name );
			parameter.Value = value;

			return this;
		}

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest Parameter ( string name , int value ) {
			Contract.Requires ( name != null );
			if ( name == null ) throw new ArgumentNullException ( "name" );

			var parameter = GetParameter<RequestNumberParameterModel> ( name );
			parameter.Value = value;

			return this;
		}

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest Parameter ( string name , Stream value ) {
			Contract.Requires ( name != null );
			Contract.Requires ( value != null );
			Contract.Ensures (
				m_RequestSettings.Parameters
					.Cast<RequestPlainBodyParameterModel> ()
					.FirstOrDefault (
						a =>
							a.Name == name &&
							a.Content == value
					) != null
			);
			if ( name == null ) throw new ArgumentNullException ( "name" );
			if ( value == null ) throw new ArgumentNullException ( "value" );


			var parameter = GetParameter<RequestPlainBodyParameterModel> ( name );
			parameter.Content = value;

			return this;
		}

		/// <summary>
		/// Parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="filePath">File path.</param>
		/// <param name="fileName">File name.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest Parameter ( string name , string filePath , string fileName ) {
			Contract.Requires ( name != null );
			Contract.Requires ( filePath != null );
			Contract.Requires ( fileName != null );
			Contract.Ensures (
				m_RequestSettings.Parameters
					.Cast<RequestFileParameterModel> ()
					.FirstOrDefault (
						a =>
							a.Name == name &&
							a.FilePath == filePath &&
							a.FileName == fileName
					) != null
			);
			if ( name == null ) throw new ArgumentNullException ( "name" );
			if ( filePath == null ) throw new ArgumentNullException ( "filePath" );
			if ( fileName == null ) throw new ArgumentNullException ( "fileName" );

			var parameter = GetParameter<RequestFileParameterModel> ( name );
			parameter.FilePath = filePath;
			parameter.FileName = fileName;

			return this;
		}

		/// <summary>
		/// Remove parameter.
		/// </summary>
		/// <param name="name">Name parameter.</param>
		public void RemoveParameter ( string name ) {
			Contract.Requires ( name != null );
			if ( name == null ) throw new ArgumentNullException ( nameof ( name ) );

			m_RequestSettings.Parameters = m_RequestSettings.Parameters
				.Where ( a => a.Name != name )
				.ToList ();
		}

		/// <summary>
		/// Remove parameters.
		/// </summary>
		/// <param name="names">Names sequence.</param>
		public void RemoveParameters ( IEnumerable<string> names ) {
			Contract.Requires ( names != null );
			if ( names == null ) throw new ArgumentNullException ( nameof ( names ) );

			m_RequestSettings.Parameters = m_RequestSettings.Parameters
				.Where ( a => !names.Contains ( a.Name ) )
				.ToList ();
		}

		/// <summary>
		/// Content type (Content-Type header).
		/// </summary>
		/// <param name="type">Content type in string respresent.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest ContentType ( string type ) {
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
		public IHttpFluentRequest ContentLength ( long contentLength ) {
			Contract.Requires ( contentLength > -1 );
			Contract.Ensures ( m_RequestSettings.ContentLength == contentLength );
			if ( contentLength < 0 ) throw new ArgumentOutOfRangeException ( "contentLength" );

			m_RequestSettings.ContentLength = contentLength;

			return this;
		}

		/// <summary>
		/// Referer.
		/// </summary>
		/// <param name="referer">Referer.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest Referer ( string referer ) {
			Contract.Requires ( referer != null );
			Contract.Ensures ( m_RequestSettings.Referer == referer );
			if ( referer == null ) throw new ArgumentNullException ( "referer" );

			m_RequestSettings.Referer = referer;

			return this;
		}

		/// <summary>
		/// Accept.
		/// </summary>
		/// <param name="accept">Accept.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest Accept ( IEnumerable<string> accepts ) {
			Contract.Requires ( accepts != null );
			Contract.Requires ( accepts.Count () > 0 );
			if ( accepts == null ) throw new ArgumentNullException ( "accepts" );
			if ( accepts.Count () == 0 ) throw new ArgumentException ( "Accepts sequence is empty." );

			m_RequestSettings.Accepts.Clear ();
			foreach ( var accept in accepts ) {
				m_RequestSettings.Accepts.Add ( accept );
			}

			return this;
		}

		/// <summary>
		/// Accept charset.
		/// </summary>
		/// <param name="encodings">Encoding.</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public IHttpFluentRequest AcceptEncodings ( IEnumerable<AcceptEncoding> encodings ) {
			Contract.Requires ( encodings != null );
			Contract.Requires ( encodings.Count () > 0 );
			if ( encodings == null ) throw new ArgumentNullException ( "encodings" );
			if ( encodings.Count () == 0 ) throw new ArgumentException ( "Encodings sequence is empty." );

			m_RequestSettings.Encodings.Clear ();
			foreach ( var encoding in encodings ) {
				m_RequestSettings.Encodings.Add ( encoding );
			}

			return this;
		}

		/// <summary>
		/// Accept language (Accept-Language header).
		/// </summary>
		/// <param name="locales"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public IHttpFluentRequest AcceptLanguage ( IEnumerable<CultureInfo> locales ) {
			Contract.Requires ( locales != null );
			Contract.Requires ( locales.Count () > 0 );
			if ( locales == null ) throw new ArgumentNullException ( "locales" );
			if ( locales.Count () == 0 ) throw new ArgumentException ( "Locales sequence is empty." );

			m_RequestSettings.Locales.Clear ();
			foreach ( var locale in locales ) {
				m_RequestSettings.Locales.Add ( locale );
			}

			return this;
		}

		/// <summary>
		/// If modified since (If-Modified-Since header).
		/// </summary>
		/// <param name="date">Date.</param>
		public IHttpFluentRequest IfModifiedSince ( DateTimeOffset date ) {
			Contract.Ensures ( m_RequestSettings.IfModifiedSince == date );

			m_RequestSettings.IfModifiedSince = date;

			return this;
		}

		/// <summary>
		/// Origin header.
		/// </summary>
		/// <param name="origin">Origin</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest Origin ( string origin ) {
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
		public IHttpFluentRequest AcceptDatetime ( DateTimeOffset date ) {
			Contract.Ensures ( m_RequestSettings.AcceptDatetime == date );

			m_RequestSettings.AcceptDatetime = date;

			return this;
		}

		/// <summary>
		/// From header.
		/// </summary>
		/// <param name="email">Email address sender.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest From ( string email ) {
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
		public IHttpFluentRequest Host ( string host ) {
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
		public IHttpFluentRequest Date ( DateTimeOffset date ) {
			Contract.Ensures ( m_RequestSettings.Date == date );

			m_RequestSettings.Date = date;

			return this;
		}

		/// <summary>
		/// User-Agent header.
		/// </summary>
		/// <param name="userAgent">User agent.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest UserAgent ( string userAgent ) {
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
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest Cookie ( IDictionary<string , string> values ) {
			Contract.Requires ( values != null );
			if ( values == null ) throw new ArgumentNullException ( nameof ( values ) );

			foreach ( var cookie in values ) {
				AddCookie ( cookie.Key , cookie.Value );
			}

			return this;
		}

		/// <summary>
		/// Add cookie.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		private void AddCookie ( string name , string value ) {
			var existingCookie = m_RequestSettings.Cookies.FirstOrDefault ( a => a.Name == name );
			if ( existingCookie == null ) {
				m_RequestSettings.Cookies.Add (
					new CookieModel {
						Name = name ,
						Value = value
					}
				);
			}
			else {
				existingCookie.Value = value;
			}
		}

		/// <summary>
		/// Add cookie.
		/// </summary>
		/// <param name="name">Cookie name.</param>
		/// <param name="value">Cookie value.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IHttpFluentRequest Cookie ( string name , string value ) {
			Contract.Requires ( name != null );
			Contract.Requires ( value != null );
			if ( name == null ) throw new ArgumentNullException ( nameof ( name ) );
			if ( value == null ) throw new ArgumentNullException ( nameof ( value ) );

			AddCookie ( name , value );

			return this;
		}

		/// <summary>
		/// Remove cookie.
		/// </summary>
		/// <param name="name">Name.</param>
		public void RemoveCookie ( string name ) {
			Contract.Requires ( name != null );
			if ( name == null ) throw new ArgumentNullException ( nameof ( name ) );

			var cookie = m_RequestSettings.Cookies.FirstOrDefault ( a => a.Name == name );
			if ( cookie != null ) m_RequestSettings.Cookies.Remove ( cookie );
		}

		/// <summary>
		/// Remove cookie.
		/// </summary>
		/// <param name="names">Names.</param>
		public void RemoveCookie ( IEnumerable<string> names ) {
			Contract.Requires ( names != null );
			if ( names == null ) throw new ArgumentNullException ( nameof ( names ) );

			foreach ( var name in names ) {
				RemoveCookie ( name );
			}
		}

		/// <summary>
		/// Accept charsets.
		/// </summary>
		/// <param name="charsets">Charsets.</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public IHttpFluentRequest AcceptCharsets ( IEnumerable<Encoding> charsets ) {
			Contract.Requires ( charsets != null );
			Contract.Requires ( charsets.Count () > 0 );
			Contract.Ensures ( m_RequestSettings.Charsets.Count () == charsets.Count () );
			if ( charsets == null ) throw new ArgumentNullException ( "charsets" );
			if ( charsets.Count () == 0 ) throw new ArgumentException ( "Locales sequence is empty." );

			m_RequestSettings.Charsets.Clear ();
			foreach ( var charset in charsets ) {
				m_RequestSettings.Charsets.Add ( charset );
			}

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
		public async Task<IHttpResponse> SendAsync ( CancellationToken cancellationToken = default ( CancellationToken ) ) {
			var response = await m_HttpBroker.SendRequestAsync ( m_RequestSettings , cancellationToken );

			return new HttpResponse ( response );
		}

		/// <summary>
		/// Clear parameters.
		/// </summary>
		/// <returns></returns>
		public IHttpFluentRequest ClearParameters () {
			m_RequestSettings.Parameters.Clear ();

			return this;
		}

		/// <summary>
		/// Clear cookie.
		/// </summary>
		public IHttpFluentRequest ClearCookie () {
			m_RequestSettings.Cookies.Clear ();

			return this;
		}

		/// <summary>
		/// Timeout.
		/// </summary>
		/// <param name="timeout">Timeout.</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public IHttpFluentRequest Timeout ( TimeSpan timeout ) {
			Contract.Requires ( timeout > TimeSpan.Zero );
			Contract.Ensures ( timeout == m_RequestSettings.Timeout );
			if ( timeout < TimeSpan.Zero ) throw new ArgumentOutOfRangeException ( "timeout" );

			m_RequestSettings.Timeout = timeout;

			return this;
		}

	}

}
