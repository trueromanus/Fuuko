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
		/// <returns>HttFluent response.</returns>
		ResponseModel SendRequest ( RequestSettingsModel requestSettings );

	}

}
