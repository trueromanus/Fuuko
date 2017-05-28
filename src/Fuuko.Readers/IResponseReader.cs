using System.IO;
using System.Text;

namespace Fuuko.Readers {

	/// <summary>
	/// Reader of response.
	/// </summary>
	/// <typeparam name="T">Type of result.</typeparam>
	public interface IResponseReader<T> {

		/// <summary>
		/// Read stream and generate instance class of <see cref="T"/>.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <param name="encoding">Encoding.</param>
		T Read ( Stream stream , Encoding encoding );

	}

}
