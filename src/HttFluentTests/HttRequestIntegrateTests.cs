using System.Collections.Generic;
using HttFluent.Classifiers;
using HttFluent.Implementations;
using HttFluent.Implementations.HttpBrokers;
using HttFluent.Models.ParameterModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttFluentTests {

	[TestClass]
	public class HttRequestIntegrateTests {

		[TestMethod]
		public void CheckResult_SimpleRequest () {
			var request = new HttFluentRequest ( new NetHttpBroker () );
			var response = request
				.Url ( "http://mail.ru" )
				.Method ( RequestMethod.Get )
				.UserAgent ( "HttFluentRequestLibraryIntegrateTest" )
				.Send ();

			Assert.AreEqual ( response.Response.StatusCode , 200 );
		}

		[TestMethod]
		public void CheckResult_GetRequestWithParameters () {
			var request = new HttFluentRequest ( new NetHttpBroker () );
			var response = request
				.Url ( "http://go.mail.ru/search" )
				.Method ( RequestMethod.Get )
				.Parameters ( 
					new List<RequestParameterModel> {
						new RequestNumberParameterModel{
							Name = "fm",
							Value = 1
						},
						new RequestStringParameterModel{
							Name = "q",
							Value = "Test"
						},
					}
				)
				.UserAgent ( "HttFluentRequestLibraryIntegrateTest" )
				.SendAsync ().Result;

			Assert.AreEqual ( response.Response.StatusCode , 200 );
		}

	}

}
