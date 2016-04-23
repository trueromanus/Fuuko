using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
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
		/// <exception cref="ArgumentNullException"></exception>
		public HttpResponse ( ResponseModel response ) {
			Contract.Requires ( response != null );
			Contract.Ensures ( Equals ( m_Response , response ) );
			if ( response == null ) throw new ArgumentNullException ( "response" );

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

		/// <summary>
		/// Get content as string.
		/// </summary>
		/// <param name="encoding">Encoding for content.</param>
		/// <returns>Content as string.</returns>
		/// <exception cref="InvalidProgramException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		public string GetContentAsString ( Encoding encoding ) {
			Contract.Requires ( encoding != null );
			if ( encoding == null ) throw new ArgumentNullException ( "encoding" );

			if ( m_Response.Content == null ) return String.Empty;

			var memoryStream = Response.Content as MemoryStream;
			if ( memoryStream == null ) throw new InvalidProgramException ( "Failed cast response content with MemoryStream." );

			return encoding.GetString ( memoryStream.ToArray () );
		}

		/// <summary>
		/// Get content as string.
		/// </summary>
		/// <returns>Content as string.</returns>
		public string GetContentAsString () {
			if ( m_Response.Content == null ) return String.Empty;
			if ( m_Response.ContentEncoding == null ) return String.Empty;

			var memoryStream = Response.Content as MemoryStream;
			if ( memoryStream == null ) throw new InvalidProgramException ( "Failed cast response content with MemoryStream." );

			return m_Response.ContentEncoding.GetString ( memoryStream.ToArray () );
		}

	}

}
