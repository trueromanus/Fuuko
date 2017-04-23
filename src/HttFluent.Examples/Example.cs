using System.Threading.Tasks;

namespace Fuuko.Examples {

	/// <summary>
	/// Example.
	/// </summary>
	public abstract class Example {

		/// <summary>
		/// Get example name.
		/// </summary>
		/// <returns></returns>
		public virtual string GetName () {
			return GetType ().Name;
		}

		/// <summary>
		/// Execute example.
		/// </summary>
		public abstract void Execute ();

		/// <summary>
		/// Execute exmaple asynchronized.
		/// </summary>
		public abstract Task ExecuteAsync ();

	}

}
