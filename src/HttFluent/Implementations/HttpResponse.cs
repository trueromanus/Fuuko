using HttFluent.Models.ResponseModels;

namespace HttFluent.Implementations {

	/// <summary>
	/// HttFluent response.
	/// </summary>
	public class HttpResponse : IHttpResponse {

		private readonly ResponseModel m_Response;

		/// <summary>
		/// Constructor for transfer model.
		/// </summary>
		/// <param name="response">Response.</param>
		public HttpResponse ( ResponseModel response ) {
			m_Response = response;
		}

		/// <summary>
		/// Data.
		/// </summary>
		public ResponseModel Response {
			get {
				return m_Response;
			}
		}

	}

}
