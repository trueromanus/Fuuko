using System;
using System.Linq;
using System.Collections.Generic;
using HttFluent;
using HttFluent.Classifiers;
using HttFluent.Implementations;
using HttFluent.Models.ParameterModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Globalization;
using System.IO;
using System.Text;
using HttFluent.Exceptions;

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
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Parameter_String_Throw_Name_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( null , "" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ParameterException ) )]
		public void Parameter_String_Throw_Add_Twice_OtherType () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "test" , 1 );
			wrapper.Request.Parameter ( "test" , "fail type" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Parameter_String_Throw_Value_Null () {
			var wrapper = CreateWrapper ();
			string str = null;
			wrapper.Request.Parameter ( "" , str );
		}

		[TestMethod]
		public void Parameter_String_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "test" , "value" );

			Assert.AreEqual ( wrapper.Request.Settings.Parameters.Count , 1 );
			Assert.AreEqual ( wrapper.Request.Settings.Parameters.First ().Name , "test" );
		}

		[TestMethod]
		public void Parameter_String_HappyPath_Update_Value () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "test" , "first value" );
			wrapper.Request.Parameter ( "test" , "second value" );

			var parameter = wrapper.Request.Settings.Parameters.First () as RequestStringParameterModel;
			Assert.AreEqual ( parameter.Name , "test" );
			Assert.AreEqual ( parameter.Value , "second value" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Parameter_Number_Throw_Name_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( null , 0 );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ParameterException ) )]
		public void Parameter_Number_Throw_Add_Twice_OtherType () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "test" , "fail type" );
			wrapper.Request.Parameter ( "test" , 1 );
		}

		[TestMethod]
		public void Parameter_Number_CheckResult_Update_Value () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "test" , 4 );
			wrapper.Request.Parameter ( "test" , 1 );

			var parameter = wrapper.Request.Settings.Parameters.First () as RequestNumberParameterModel;
			Assert.AreEqual ( parameter.Name , "test" );
			Assert.AreEqual ( parameter.Value , 1 );
		}

		[TestMethod]
		public void Parameter_Number_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "test" , 1 );

			Assert.AreEqual ( wrapper.Request.Settings.Parameters.Count , 1 );
			var parameter = wrapper.Request.Settings.Parameters.First () as RequestNumberParameterModel;
			Assert.AreEqual ( parameter.Name , "test" );
			Assert.AreEqual ( parameter.Value , 1 );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Parameter_File_Throw_Name_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( null , @"c:\robocop\part2\scenewithcar\" , "robocop_punish_to_bad_boys!!!.txt" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Parameter_File_Throw_FilePath_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "robocop 2" , null , "robocop_punish_to_bad_boys!!!.txt" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Parameter_File_Throw_FileName_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "robocop 2" , @"c:\robocop\part2\scenewithcar\" , null );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ParameterException ) )]
		public void Parameter_File_Throw_Twice_Update_Other_Type () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "robocop 2" , 1 );
			wrapper.Request.Parameter ( "robocop 2" , @"c:\robocop\part2\scenewithcar\" , "robocop_punish_to_bad_boys!!!.txt" );
		}

		[TestMethod]
		public void Parameter_File_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "robocop 2" , @"c:\robocop\part2\scenewithcar\" , "robocop_punish_to_bad_boys!!!.txt" );

			Assert.AreEqual ( wrapper.Request.Settings.Parameters.Count , 1 );
			var parameter = wrapper.Request.Settings.Parameters.First () as RequestFileParameterModel;
			Assert.AreEqual ( parameter.Name , "robocop 2" );
			Assert.AreEqual ( parameter.FileName , "robocop_punish_to_bad_boys!!!.txt" );
			Assert.AreEqual ( parameter.FilePath , @"c:\robocop\part2\scenewithcar\" );
		}

		[TestMethod]
		public void Parameter_File_CheckResult_HappyPath_Update () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "movie" , @"c:\robocop\part2\scenewithcar\" , "robocop_punish_to_bad_boys!!!.txt" );
			wrapper.Request.Parameter ( "movie" , @"c:\terminator\part2\majordialog\" , "astalavista_baby!!!.txt" );

			Assert.AreEqual ( wrapper.Request.Settings.Parameters.Count , 1 );
			var parameter = (RequestFileParameterModel) wrapper.Request.Settings.Parameters.First ( a => a.Name == "movie" );
			Assert.AreEqual ( parameter.FileName , "astalavista_baby!!!.txt" );
			Assert.AreEqual ( parameter.FilePath , @"c:\terminator\part2\majordialog\" );
		}

		[TestMethod]
		public void ContentType_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.ContentType ( "application/json" );

			Assert.AreEqual ( wrapper.Request.Settings.ContentType , "application/json" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void ContentType_Throw_ContentType_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.ContentType ( null );
		}

		[TestMethod]
		public void ContentLength_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();

			wrapper.Request.ContentLength ( 1000000000 );

			Assert.AreEqual ( wrapper.Request.Settings.ContentLength , 1000000000 );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentOutOfRangeException ) )]
		public void ContentLength_Throw_ContentLength_OutOfRange () {
			var wrapper = CreateWrapper ();
			wrapper.Request.ContentLength ( -1 );
		}

		[TestMethod]
		public void Referer_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Referer ( "http://test.com" );

			Assert.AreEqual ( wrapper.Request.Settings.Referer , "http://test.com" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Referer_Throw_Referer_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Referer ( null );
		}

		[TestMethod]
		public void Accept_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Accept (
				new List<string> {
					"application/json",
					"text/html"
				}
			);

			Assert.AreEqual ( wrapper.Request.Settings.Accepts.Count () , 2 );
			Assert.AreEqual ( wrapper.Request.Settings.Accepts.First () , "application/json" );
			Assert.AreEqual ( wrapper.Request.Settings.Accepts.Last () , "text/html" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Accept_Throw_Accepts_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Accept ( null );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentException ) )]
		public void Accept_Throw_Accepts_Empty () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Accept ( new List<string> () );
		}

		[TestMethod]
		public void AcceptLanguage_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.AcceptLanguage (
				new List<CultureInfo> {
					new CultureInfo("ru-RU"),
					new CultureInfo("en-US")
				}
			);

			Assert.AreEqual ( wrapper.Request.Settings.Locales.Count () , 2 );
			Assert.AreEqual ( wrapper.Request.Settings.Locales.First () , new CultureInfo ( "ru-RU" ) );
			Assert.AreEqual ( wrapper.Request.Settings.Locales.Last () , new CultureInfo ( "en-US" ) );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void AcceptLanguage_Throw_Accepts_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.AcceptLanguage ( null );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentException ) )]
		public void AcceptLanguage_Throw_Accepts_Empty () {
			var wrapper = CreateWrapper ();
			wrapper.Request.AcceptLanguage ( new List<CultureInfo> () );
		}

		[TestMethod]
		public void IfModifiedSince_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.IfModifiedSince ( new DateTimeOffset ( new DateTime ( 2015 , 11 , 10 , 0 , 0 , 0 ) , new TimeSpan ( 3 , 0 , 0 ) ) );

			Assert.AreEqual (
				wrapper.Request.Settings.IfModifiedSince ,
				new DateTimeOffset ( new DateTime ( 2015 , 11 , 10 , 0 , 0 , 0 ) , new TimeSpan ( 3 , 0 , 0 ) )
			);
		}

		[TestMethod]
		public void Origin_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Origin ( "http://allowcors.com" );

			Assert.AreEqual ( wrapper.Request.Settings.Origin , "http://allowcors.com" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Origin_Throw_Origin_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Origin ( null );
		}

		[TestMethod]
		public void AcceptDatetime_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.AcceptDatetime ( new DateTimeOffset ( new DateTime ( 2015 , 11 , 10 , 0 , 0 , 0 ) , new TimeSpan ( 3 , 0 , 0 ) ) );

			Assert.AreEqual ( wrapper.Request.Settings.AcceptDatetime , new DateTimeOffset ( new DateTime ( 2015 , 11 , 10 , 0 , 0 , 0 ) , new TimeSpan ( 3 , 0 , 0 ) ) );
		}

		[TestMethod]
		public void From_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.From ( "test@test.com" );

			Assert.AreEqual ( wrapper.Request.Settings.FromEmail , "test@test.com" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void From_Throw_From_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.From ( null );
		}

		[TestMethod]
		public void Host_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Host ( "ciri@cintra.com" );

			Assert.AreEqual ( wrapper.Request.Settings.Host , "ciri@cintra.com" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Host_Throw_Host_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Host ( null );
		}

		[TestMethod]
		public void Date_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Date ( new DateTimeOffset ( new DateTime ( 2015 , 11 , 10 , 0 , 0 , 0 ) , new TimeSpan ( 3 , 0 , 0 ) ) );

			Assert.AreEqual ( wrapper.Request.Settings.Date , new DateTimeOffset ( new DateTime ( 2015 , 11 , 10 , 0 , 0 , 0 ) , new TimeSpan ( 3 , 0 , 0 ) ) );
		}

		[TestMethod]
		public void UserAgent_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.UserAgent ( "geralt_best_regards" );

			Assert.AreEqual ( wrapper.Request.Settings.UserAgent , "geralt_best_regards" );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void UserAgent_Throw_UserAgent_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.UserAgent ( null );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void AcceptCharsets_Throw_CharSets_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.AcceptCharsets ( null );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentException ) )]
		public void AcceptCharsets_Throw_CharSets_Empty () {
			var wrapper = CreateWrapper ();
			wrapper.Request.AcceptCharsets ( new List<Encoding> () );
		}

		[TestMethod]
		public void AcceptCharsets_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.AcceptCharsets (
				new List<Encoding> {
					Encoding.UTF8
				}
			);

			Assert.IsTrue ( wrapper.Request.Settings.Charsets.First () == Encoding.UTF8 );
			Assert.IsTrue ( wrapper.Request.Settings.Charsets.Count () == 1 );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void AcceptEncodings_Throw_Encodings_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.AcceptEncodings ( null );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentException ) )]
		public void AcceptEncodings_Throw_Encodings_Empty () {
			var wrapper = CreateWrapper ();
			wrapper.Request.AcceptEncodings ( new List<AcceptEncoding> () );
		}

		[TestMethod]
		public void AcceptEncodings_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.AcceptEncodings (
				new List<AcceptEncoding> {
					AcceptEncoding.gzip,
					AcceptEncoding.deflate
				}
			);

			Assert.AreEqual ( wrapper.Request.Settings.Encodings.Count () , 2 );
			Assert.AreEqual ( wrapper.Request.Settings.Encodings.First () , AcceptEncoding.gzip );
			Assert.AreEqual ( wrapper.Request.Settings.Encodings.Last () , AcceptEncoding.deflate );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void ParameterStream_Throw_Name_Null () {
			var wrapper = CreateWrapper ();
			var stream = new MemoryStream ();
			wrapper.Request.Parameter ( null , stream );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void ParameterStream_Throw_Stream_Null () {
			var wrapper = CreateWrapper ();
			Stream stream = null;
			wrapper.Request.Parameter ( "" , stream );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ParameterException ) )]
		public void ParameterStream_Throw_Twice_Update_Other_Type () {
			var wrapper = CreateWrapper ();

			wrapper.Request.Parameter ( "stream" , "lala" );
			Stream stream = new MemoryStream ();
			wrapper.Request.Parameter ( "stream" , stream );
		}

		[TestMethod]
		public void ParameterStream_CheckResult_CheckName () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Parameter ( "test" , new MemoryStream () );

			Assert.IsTrue ( wrapper.Request.Settings.Parameters.Any ( a => a.Name == "test" ) );
		}

		[TestMethod]
		public void ParameterStream_CheckResult_Update_Content () {
			var wrapper = CreateWrapper ();
			var firstStream = new MemoryStream ();
			wrapper.Request.Parameter ( "test" , firstStream );
			firstStream.Dispose ();
			var secondStream = new MemoryStream ();
			wrapper.Request.Parameter ( "test" , secondStream );

			var parameter = (RequestPlainBodyParameterModel) wrapper.Request.Settings.Parameters.First ( a => a.Name == "test" );
			Assert.AreEqual ( parameter.Content , secondStream );
		}

		[TestMethod]
		public void ParameterStream_CheckResult_CheckStream () {
			var wrapper = CreateWrapper ();
			var stream = new MemoryStream ();
			wrapper.Request.Parameter ( "test" , stream );

			Assert.AreEqual ( ( wrapper.Request.Settings.Parameters.First () as RequestPlainBodyParameterModel ).Content , stream );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Cookie_Throw_Values_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Cookie ( null , "" , "" , false , DateTime.Now );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Cookie_Throw_Domain_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Cookie ( new Dictionary<string , string> () , null , "" , false , DateTime.Now );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Cookie_Throw_Path_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Cookie ( new Dictionary<string , string> () , "" , null , false , DateTime.Now );
		}

		[TestMethod]
		public void Cookie_CheckResult_Count () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Cookie (
				new Dictionary<string , string> {
					{ "test" , "value" }
				} ,
				"sex.com" ,
				"/" ,
				false ,
				DateTime.Now
			);

			Assert.AreEqual ( wrapper.Request.Settings.Cookies.Count () , 1 );
		}

		[TestMethod]
		public void Cookie_CheckResult_Value () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Cookie (
				new Dictionary<string , string> {
					{ "test" , "value" }
				} ,
				"sex.com" ,
				"/" ,
				false ,
				DateTime.Now
			);

			var firstValue = wrapper.Request.Settings.Cookies.First ();
			Assert.AreEqual ( firstValue.Name , "test" );
			Assert.AreEqual ( firstValue.Value , "value" );
		}

		[TestMethod]
		public void Cookie_CheckResult_Domain () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Cookie (
				new Dictionary<string , string> {
					{ "test" , "value" }
				} ,
				"sex.com" ,
				"/" ,
				false ,
				DateTime.Now
			);

			var firstValue = wrapper.Request.Settings.Cookies.First ();
			Assert.AreEqual ( firstValue.Domain , "sex.com" );
		}

		[TestMethod]
		public void Cookie_CheckResult_Path () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Cookie (
				new Dictionary<string , string> {
					{ "test" , "value" }
				} ,
				"sex.com" ,
				"/" ,
				false ,
				DateTime.Now
			);

			var firstValue = wrapper.Request.Settings.Cookies.First ();
			Assert.AreEqual ( firstValue.Path , "/" );
		}

		[TestMethod]
		public void Cookie_CheckResult_Secure () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Cookie (
				new Dictionary<string , string> {
					{ "test" , "value" }
				} ,
				"sex.com" ,
				"/" ,
				true ,
				DateTime.Now
			);

			var firstValue = wrapper.Request.Settings.Cookies.First ();
			Assert.AreEqual ( firstValue.Secure , true );
		}

		[TestMethod]
		public void Cookie_CheckResult_Expires () {
			var wrapper = CreateWrapper ();
			var date = DateTime.Now;
			wrapper.Request.Cookie (
				new Dictionary<string , string> {
					{ "test" , "value" }
				} ,
				"sex.com" ,
				"/" ,
				true ,
				date
			);

			var firstValue = wrapper.Request.Settings.Cookies.First ();
			Assert.AreEqual ( firstValue.Expires , date );
		}

		[TestMethod]
		public void ClearParameters_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.ClearParameters ();

			Assert.AreEqual ( wrapper.Request.Settings.Parameters.Count , 0 );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentOutOfRangeException ) )]
		public void Timeout_Throw_Timeout_OutOfRange () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Timeout ( TimeSpan.Zero - new TimeSpan ( 0 , 1 , 0 ) );
		}

		[TestMethod]
		public void Timeout_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.Timeout ( new TimeSpan ( 0 , 1 , 0 ) );

			Assert.AreEqual ( wrapper.Request.Settings.Timeout , new TimeSpan ( 0 , 1 , 0 ) );
		}

		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void ExtraParameterUrl_Throw_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Request.ExtraParameterUri ( null );
		}

		[TestMethod]
		public void ExtraParameterUrl_CheckResult_HappyPath () {
			var wrapper = CreateWrapper ();
			wrapper.Request.ExtraParameterUri ( "arg/12/" );

			Assert.AreEqual ( wrapper.Request.Settings.ExtraParameterUrl , "arg/12/" );
		}

		[TestMethod]
		public void ClearCookie_CheckResult_HappyPath () {
			//Arrange
			var wrapper = CreateWrapper ();
			wrapper.Request.Cookie ( new Dictionary<string , string> () , "test" , "/" , true , DateTime.Now );
			
			//Act
			wrapper.Request.ClearCookie ();

			//Assert
			Assert.AreEqual ( wrapper.Request.Settings.Cookies.Count , 0 );
		}

	}

}
