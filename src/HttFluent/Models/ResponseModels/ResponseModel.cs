using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HttFluent.Classifiers;

namespace HttFluent.Models.ResponseModels {
	
	/// <summary>
	/// Response model.
	/// </summary>
	public class ResponseModel {

		/// <summary>
		/// Status code.
		/// </summary>
		public ResponseStatusCode StatusCode {
			get;
			set;
		}

		/// <summary>
		/// Protocol version.
		/// </summary>
		public Version ProtocolVersion {
			get;
			set;
		}

		/// <summary>
		/// Age.
		/// </summary>
		public TimeSpan? Age {
			get;
			set;
		}

		/// <summary>
		/// Allow request's methods.
		/// </summary>
		public IEnumerable<RequestMethod> Allow {
			get;
			set;
		}

		/// <summary>
		/// Content Disposition.
		/// </summary>
		public ContentDispositionModel ContentDisposition {
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
		/// Content length.
		/// </summary>
		public long ContentLength {
			get;
			set;
		}

		/// <summary>
		/// Content.
		/// </summary>
		public Stream Content {
			get;
			set;
		}

		/// <summary>
		/// Content encoding.
		/// </summary>
		public Encoding ContentEncoding {
			get;
			set;
		}

	}

}
