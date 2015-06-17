using System;

namespace HttFluent.Models.CookieModels {
	
	/// <summary>
	/// Cookie settings model.
	/// </summary>
	public class CookieModel {

		/// <summary>
		/// Name.
		/// </summary>
		public string Name {
			get;
			set;
		}

		/// <summary>
		/// Value.
		/// </summary>
		public string Value {
			get;
			set;
		}

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
