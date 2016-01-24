using System;
using System.Linq;
using System.Reflection;
using System.Text;
using HttFluent.Exceptions;

namespace HttFluent.Examples {
	class Program {
		static void Main ( string[] args ) {
			var basicExampleType = typeof ( Example );

			var exampleTypes = Assembly.GetEntryAssembly ().GetTypes ()
				.Where (
					type =>
						basicExampleType.IsAssignableFrom(type) &&
						type != basicExampleType
				)
				.ToList ();

			foreach ( var exampleType in exampleTypes ) {
				var instance = ( Activator.CreateInstance ( exampleType ) as Example );
				Console.WriteLine ( "Execute example: {0}" , instance.GetName () );

				try {
					instance.Execute ();
				}
				catch ( NetBrokerException e ) {
					Console.WriteLine ( "Exception with message: {0}" , e.Message );
					Console.ReadKey ();
					continue;
				}

				Console.WriteLine ( "Example {0} complete executed." , instance.GetName () );
				Console.ReadKey ();
			}
		}
	}
}
