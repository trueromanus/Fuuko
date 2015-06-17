using System;

namespace HttFluent.Models {
	
	/// <summary>
	/// Cookie settings model.
	/// </summary>
	public class CookieSettingsModel {

		/// <summary>
		/// Expires.
		/// </summary>
		public DateTime Expires {
			get;
			set;
		}

		/// <summary>
		/// Domain.
		/// </summary>
		public string Domain {
			get;
			set;
		}

		/// <summary>
		/// Path for documents.
		/// </summary>
		public string Path {
			get;
			set;
		}

		/// <summary>
		/// Secure.
		/// </summary>
		public bool Secure {
			get;
			set;
		}

	}

}
