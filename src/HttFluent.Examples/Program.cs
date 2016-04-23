using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HttFluent.Exceptions;

namespace HttFluent.Examples {
	class Program {

		private static bool m_IsExecuteAsynchronized = true;

		static void Main ( string[] args ) {
			var basicExampleType = typeof ( Example );
			var isAsynchronized = args.Any ( a => a == "async" );

			var exampleTypes = Assembly.GetEntryAssembly ().GetTypes ()
				.Where (
					type =>
						basicExampleType.IsAssignableFrom ( type ) &&
						type != basicExampleType
				)
				.ToList ();

			foreach ( var exampleType in exampleTypes ) {
				var instance = ( Activator.CreateInstance ( exampleType ) as Example );
				Console.WriteLine ( "Execute example: {0}" , instance.GetName () );

				try {
					if ( isAsynchronized ) {
						m_IsExecuteAsynchronized = false;
						ExecuteAsync ( instance );
						while ( !m_IsExecuteAsynchronized ) {
						}
					}
					else {
						instance.Execute ();
					}
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

		private static async void ExecuteAsync ( Example example ) {
			await example.ExecuteAsync ();
			m_IsExecuteAsynchronized = true;
		}

	}
}
