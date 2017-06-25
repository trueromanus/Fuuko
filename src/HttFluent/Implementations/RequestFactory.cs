using Fuuko.Implementations.HttpBrokers;

namespace Fuuko.Implementations {

	/// <summary>
	/// Factory for <see cref="HttpFluentRequest"/>.
	/// </summary>
	public static class RequestFactory {

		/// <summary>
		/// Create <see cref="HttpFluentRequest"/> instance based on <see cref="NetHttpBroker"/>.
		/// </summary>
		public static HttpFluentRequest CreateRequest () {
			return new HttpFluentRequest ( new NetHttpBroker () );
		}

	}

}
