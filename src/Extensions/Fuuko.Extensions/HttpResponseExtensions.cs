using Fuuko.Implementations;
using Fuuko.Readers.Implementations;

namespace Fuuko.Extensions {

	/// <summary>
	/// Exntensions for <see cref="HttpResponse"/>.
	/// </summary>
	public static class HttpResponseExtensions {

		/// <summary>
		/// Convert response from json structure to native class or primitive.
		/// </summary>
		/// <param name="response">Response retrieved from executed Http request.</param>
		/// <returns>Serialized object.</returns>
		public static T ToJson<T> ( this HttpResponse response ) {
			return response.GetContentAsObject<T> ( new JsonResponseReader<T> () );
		}

	}

}
