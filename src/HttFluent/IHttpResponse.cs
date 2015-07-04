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
		/// Data.
		/// </summary>
		ResponseModel Response {
			get;
		}

	}

}
