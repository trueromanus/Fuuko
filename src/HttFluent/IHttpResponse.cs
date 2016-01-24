using System.Text;
using HttFluent.Models.ResponseModels;

namespace HttFluent {

	/// <summary>
	/// Http fluent response.
	/// </summary>
	public interface IHttpResponse {

		/// <summary>
		/// Response data.
		/// </summary>
		ResponseModel Response {
			get;
		}

		/// <summary>
		/// Get content as string.
		/// </summary>
		/// <param name="encoding">Encoding for content.</param>
		/// <returns>Content as string.</returns>
		string GetContentAsString ( Encoding encoding );

		/// <summary>
		/// Get content as string.
		/// </summary>
		/// <returns>Content as string.</returns>
		string GetContentAsString ();

	}

}
