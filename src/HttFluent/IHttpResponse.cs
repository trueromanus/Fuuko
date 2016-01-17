using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

	}

}
