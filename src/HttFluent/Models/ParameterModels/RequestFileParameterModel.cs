
namespace Fuuko.Models.ParameterModels {
	
	/// <summary>
	/// Request file parameter model.
	/// </summary>
	public class RequestFileParameterModel : RequestParameterModel {

		/// <summary>
		/// File path.
		/// </summary>
		public string FilePath {
			get;
			set;
		}

		/// <summary>
		/// File name.
		/// </summary>
		public string FileName {
			get;
			set;
		}

	}

}
