using System.Text;
using Fuuko.Models.ResponseModels;

namespace Fuuko {

	/// <summary>
	/// Http fluent response.
	/// </summary>
	public interface IHttpResponse {

		/// <summary>
		/// Response data.
		/// </summary>
		ResponseModel Data {
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

		/// <summary>
		/// Get content as object with type <see cref="T"/>.
		/// </summary>
		/// <param name="responseReader">Reader of response.</param>
		/// <returns>Generated object.</returns>
		T GetContentAsObject<T> ( IResponseReader<T> responseReader );

	}

}
