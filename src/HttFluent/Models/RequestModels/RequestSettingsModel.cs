using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using HttFluent.Classifiers;
using HttFluent.Models.CookieModels;
using HttFluent.Models.ParameterModels;

namespace HttFluent.Models.RequestModels {
	
	/// <summary>
	/// Request model.
	/// </summary>
	public class RequestSettingsModel {

		/// <summary>
		/// Default constructor.
		/// </summary>
		public RequestSettingsModel () {
			Parameters = new List<RequestParameterModel> ();
			Accepts = new List<string> ();
			Encodings = new List<AcceptEncoding> ();
			Locales = new List<CultureInfo> ();
			Charsets = new List<Encoding> ();
			Cookies = new List<CookieModel> ();
		}

		/// <summary>
		/// Url.
		/// </summary>
		public Uri Url {
			get;
			set;
		}

		/// <summary>
		/// Method.
		/// </summary>
		public RequestMethod Method {
			get;
			set;
		}

		/// <summary>
		/// Parameters.
		/// </summary>
		public ICollection<RequestParameterModel>Parameters {
			get;
			set;
		}

		/// <summary>
		/// Type.
		/// </summary>
		public string Type {
			get;
			set;
		}

		/// <summary>
		/// Content length.
		/// </summary>
		public long ContentLength {
			get;
			set;
		}

		/// <summary>
		/// Content type.
		/// </summary>
		public string ContentType {
			get;
			set;
		}

		/// <summary>
		/// Referer.
		/// </summary>
		public string Referer {
			get;
			set;
		}

		/// <summary>
		/// Accept.
		/// </summary>
		public IEnumerable<string> Accepts {
			get;
			set;
		}

		/// <summary>
		/// Encodings.
		/// </summary>
		public IEnumerable<AcceptEncoding> Encodings {
			get;
			set;
		}

		/// <summary>
		/// Locales which accept client.
		/// </summary>
		public IEnumerable<CultureInfo> Locales {
			get;
			set;
		}

		/// <summary>
		/// Charsets.
		/// </summary>
		public IEnumerable<Encoding> Charsets {
			get;
			set;
		}

		/// <summary>
		/// If modified since.
		/// </summary>
		public DateTimeOffset? IfModifiedSince {
			get;
			set;
		}

		/// <summary>
		/// Origin.
		/// </summary>
		public string Origin {
			get;
			set;
		}

		/// <summary>
		/// Accept date time.
		/// </summary>
		public DateTimeOffset? AcceptDatetime {
			get;
			set;
		}

		/// <summary>
		/// From email.
		/// </summary>
		public string FromEmail {
			get;
			set;
		}

		/// <summary>
		/// Host.
		/// </summary>
		public string Host {
			get;
			set;
		}

		/// <summary>
		/// Date.
		/// </summary>
		public DateTimeOffset? Date {
			get;
			set;
		}

		/// <summary>
		/// User agent.
		/// </summary>
		public string UserAgent {
			get;
			set;
		}

		/// <summary>
		/// Cookies
		/// </summary>
		public IEnumerable<CookieModel> Cookies {
			get;
			set;
		}

	}

}
