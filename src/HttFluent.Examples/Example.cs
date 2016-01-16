using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttFluent.Examples {
	
	/// <summary>
	/// Example.
	/// </summary>
	public abstract class Example {

		public virtual string GetName () {
			return this.GetType ().Name;
		}

		public abstract void Execute ();

	}

}
