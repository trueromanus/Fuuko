using System.IO;

namespace HttFluent.Models.ParameterModels {
	
	/// <summary>
	/// Request plain body parameter model.
	/// </summary>
	public class RequestPlainBodyParameterModel : RequestParameterModel {

		/// <summary>
		/// Content.
		/// </summary>
		public Stream Content {
			get;
			set;
		}

	}

}
