using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Fuuko.Readers.Implementations {

	/// <summary>
	/// Json response reader.
	/// </summary>
	public class JsonResponseReader<T> : IResponseReader<T> {

		/// <summary>
		/// Read stream and generate json object.
		/// </summary>
		/// <param name="stream">Input stream.</param>
		/// <param name="encoding">Encoding.</param>
		/// <returns>Generated object.</returns>
		public T Read ( Stream stream , Encoding encoding ) {
			if ( stream == null ) throw new ArgumentNullException ( nameof ( stream ) );
			if ( encoding == null ) throw new ArgumentNullException ( nameof ( encoding ) );

			var memoryStream = stream as MemoryStream;
			if ( memoryStream == null ) return default ( T );

			return JsonConvert.DeserializeObject<T> ( encoding.GetString ( memoryStream.ToArray () ) );
		}

	}

}
