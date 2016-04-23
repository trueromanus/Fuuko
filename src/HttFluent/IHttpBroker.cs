using System.Threading;
using System.Threading.Tasks;
using HttFluent.Models.RequestModels;
using HttFluent.Models.ResponseModels;

namespace HttFluent {

	/// <summary>
	/// Http broker.
	/// </summary>
	public interface IHttpBroker {

		/// <summary>
		/// Send request.
		/// </summary>
		/// <param name="requestSettings">Request settings.</param>
		/// <returns>Response model.</returns>
		ResponseModel SendRequest ( RequestSettingsModel requestSettings );

		/// <summary>
		/// Send request asynchronized.
		/// </summary>
		/// <param name="requestSettings">Request settings.</param>
		/// <returns>Response model.</returns>
		Task<ResponseModel> SendRequestAsync ( RequestSettingsModel requestSettings , CancellationToken cancellationToken = default(CancellationToken) );

	}

}
