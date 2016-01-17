using System;
using System.IO;
using System.Text;
using HttFluent.Implementations;
using HttFluent.Models.ResponseModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttFluentTests {
	[TestClass]
	public class HttpResponseUnitTest {

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Constructor_Throw_Response_Null () {
			var response = new HttpResponse ( null );
		}

		[TestMethod]
		public void Constructor_HappyPath () {
			var response = new HttpResponse ( new ResponseModel () );

			Assert.IsNotNull ( response );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void GetContentAsString_Throw_Encoding_Null () {
			var response = new HttpResponse ( new ResponseModel () );

			response.GetContentAsString ( null );
		}

		[TestMethod]
		public void GetContentAsString_CheckResult_ResponseContent_Null () {
			var response = new HttpResponse ( new ResponseModel () );

			var result = response.GetContentAsString ( Encoding.UTF8 );

			Assert.AreEqual ( result , String.Empty );
		}

		[TestMethod]
		public void GetContentAsString_CheckResult_ResponseContent_NotCast_MemoryStream () {
			var fileName = Path.GetTempFileName ();
			var response = new HttpResponse (
				new ResponseModel {
					Content = File.Open ( fileName , FileMode.OpenOrCreate )
				}
			);

			var assert = false;

			try {
				var result = response.GetContentAsString ( Encoding.UTF8 );
			}
			catch ( InvalidProgramException ) {
				assert = true;
			}
			finally {
				response.Response.Content.Dispose ();
				File.Delete ( fileName );
			}
			Assert.IsTrue ( assert );
		}

		[TestMethod]
		public void GetContentAsString_CheckResult_HappyPath () {
			var data = "Test data stream content";
			var encoding = Encoding.UTF8;
			var stream = new MemoryStream();
			var bytes = encoding.GetBytes(data);
			stream.Write(bytes,0,bytes.Length);
			stream.Position = 0;
			
			var response = new HttpResponse (
				new ResponseModel {
					Content = stream
				}
			);

			var result = response.GetContentAsString ( encoding );

			Assert.AreEqual ( result , "Test data stream content" );
		}

	}

}
