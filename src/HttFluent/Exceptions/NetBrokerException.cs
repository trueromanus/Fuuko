using System;

namespace Fuuko.Exceptions {

	/// <summary>
	/// Net broker exception.
	/// </summary>
	[Serializable]
	public sealed class NetBrokerException : ApplicationException {

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NetBrokerException ()
			: base ( "HttFluent net broker unhandled exception." ) {
		}

		/// <summary>
		/// Constructor with message.
		/// </summary>
		/// <param name="message">Message.</param>
		public NetBrokerException ( string message )
			: base ( message ) {
		}

		/// <summary>
		/// Constructor with message and inner exception.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="innerException">Inner exception.</param>
		public NetBrokerException ( string message , Exception innerException )
			: base ( message , innerException ) {
		}

	}

}
