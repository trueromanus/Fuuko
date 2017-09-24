using System;

namespace Fuuko.Exceptions {

	/// <summary>
	/// Parameter exception.
	/// </summary>
	[Serializable]
	public class ParameterException : ApplicationException {

		/// <summary>
		/// Parameterless constructor.
		/// </summary>
		public ParameterException ()
			: base ( "Invalid request parameter." ) {
		}

		/// <summary>
		/// Constructor with message.
		/// </summary>
		/// <param name="message">Error message.</param>
		public ParameterException ( string message )
			: base ( message ) {
		}

		/// <summary>
		/// Constructor with message and inner exception.
		/// </summary>
		/// <param name="message">Error message.</param>
		/// <param name="innerException">Inner exception.</param>
		public ParameterException ( string message , Exception innerException )
			: base ( message , innerException ) {
		}

	}

}
