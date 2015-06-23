using HttFluent.Classifiers;

namespace HttFluent.Models.ResponseModels {
	
	/// <summary>
	/// Content disposition model.
	/// </summary>
	public class ContentDispositionModel {

		/// <summary>
		/// Disposition value.
		/// </summary>
		public string Value {
			get;
			set;
		}

		/// <summary>
		/// Custom key.
		/// </summary>
		public string CustomKey {
			get;
			set;
		}

		/// <summary>
		/// Type.
		/// </summary>
		public ContentDispositionType Type {
			get;
			set;
		}

	}

}
