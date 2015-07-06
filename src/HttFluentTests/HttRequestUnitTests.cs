using System;
using System.Linq;
using System.Collections.Generic;
using HttFluent;
using HttFluent.Classifiers;
using HttFluent.Implementations;
using HttFluent.Models.ParameterModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HttFluentTests {

	/// <summary>
	/// Http request unit tests.
	/// </summary>
	[TestClass]
	public class HttRequestUnitTests {

		private class Wrapper {

			public HttFluentRequest Request {
				get;
				set;
			}

			public Mock<IHttpBroker> StubBroker {
				get;
				set;
			}

		}

		private Wrapper CreateWrapper () {
			var stubBroker = new Mock<IHttpBroker> ();

			return new Wrapper {
				StubBroker = stubBroker ,
				Request = new HttFluentRequest ( stubBroker.Object )
			};
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Constructor_Throw_Broker_Null () {
			new HttFluentRequest ( null );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Url_Throw_Url_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Url ( null );
		}

		[TestMethod]
		public void Url_ChekResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Url ( "http://google.ru/" );

			Assert.AreEqual ( wrapper.Request.Settings.Url.ToString () , "http://google.ru/" );
		}

		[TestMethod]
		public void Method_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Method ( RequestMethod.Post );

			Assert.AreEqual ( wrapper.Request.Settings.Method , RequestMethod.Post );
		}

		[TestMethod]
		public void Parameters_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameters (
				new List<RequestParameterModel> {
					new RequestNumberParameterModel{
						Name = "test",
						Value = 20
					}
				}
			);

			Assert.AreEqual ( wrapper.Request.Settings.Parameters.Count , 1 );
			Assert.AreEqual ( wrapper.Request.Settings.Parameters.First ().Name , "test" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Parameters_Throw_Parameters_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameters ( null );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentException ) )]
		public void Parameters_Throw_Parameters_Empty () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameters ( new List<RequestParameterModel> () );
		}

	}

}
