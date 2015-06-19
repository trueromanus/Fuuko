using System.Net.Http;
using System.Threading.Tasks;
using HttFluent.Models.RequestModels;
using HttFluent.Models.ResponseModels;

namespace HttFluent.Implementations.HttpBrokers {

	/// <summary>
	/// Net http broker.
	/// </summary>
	public class NetHttpBroker : IHttpBroker {

		/// <summary>
		/// Send request.
		/// </summary>
		/// <param name="requestSettings">Request settings.</param>
		/// <returns>Response model.</returns>
		public ResponseModel SendRequest ( RequestSettingsModel requestSettings ) {
			return null;
		}

		/// <summary>
		/// Send request asynchronized.
		/// </summary>
		/// <param name="requestSettings">Request settings.</param>
		/// <returns>Response model.</returns>
		public Task<ResponseModel> SendRequestAsync ( RequestSettingsModel requestSettings ) {
			using ( var client = new HttpClient () ) {
				return null;
			}
		}

	}

}
