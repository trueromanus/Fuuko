using System.Collections.Generic;
using System.IO;
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

		[TestMethod]
		public void CheckResult_PostSaveFile () {
			var result = true;
			var testContent = "Lalala trulala blulala muhahahahaha!!!";

			var fakeServer = new FakeHttpServer (
				16550 ,
				( request , response ) => {
					if ( request.HttpMethod != "POST" ) result = false;
					if ( request.ContentType != "application/octet-stream" ) result = false;
					if ( request.ContentLength64 != testContent.Length ) result = false;
					if ( new StreamReader ( request.InputStream ).ReadToEnd () != testContent ) result = false;
					response.StatusCode = 200;
				}
			);
			var randomFile = Path.Combine ( Path.GetTempPath () , Path.GetRandomFileName () );
			File.WriteAllText ( randomFile , testContent );
			var httRequest = new HttFluentRequest ( new NetHttpBroker () );

			using ( var file = File.OpenRead ( randomFile ) ) {
				httRequest
					.Url ( "http://127.0.0.1:16550/" )
					.ContentType ( "application/octet-stream" )
					.Method ( RequestMethod.Post )
					.Parameter ( "myfile" , file )
					.Send ();
			}

			File.Delete ( randomFile );
			fakeServer.Dispose ();

			Assert.IsTrue ( result );
		}

	}

}
