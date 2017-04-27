[![Build status](https://ci.appveyor.com/api/projects/status/a3w1inx1939h8gy0?svg=true)](https://ci.appveyor.com/project/trueromanus/fuuko)
[![Nuget package](https://img.shields.io/badge/nuget-3.5.0-blue.svg)](https://www.nuget.org/packages/Fuuko/)

## Overview
Fuuko is small library based on [HttpClient](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx) class. It allows to make HTTP queries in declarative style (like Linq). 
The purpose of this project is to make the execution of HTTP requests easier.
Supported synchronized and asynchronized execution.

## Documentation

The documentation is [here](https://github.com/trueromanus/Fuuko/wiki)

## Quick examples

### Google search
```csharp
var response = new HttpFluentRequest ( new NetHttpBroker () )
	.Url ( "https://www.google.ru/webhp" ) //define full url
	.Method ( RequestMethod.Get ) // define HTTP method
	.Parameter ( "q" , "cats" ) // define parameter q=cats
	.Parameter ( "ie" , "UTF-8" ) // define parameter ie=UTF-8
	.Send (); //send request synchronized

//output to console some response properties

Console.WriteLine ( "Content-Type: {0}" , response.Response.ContentType );
Console.WriteLine ( "Content-Length: {0}" , response.Response.ContentLength );
Console.WriteLine ( "Status: {0}" , response.Response.StatusCode );
Console.WriteLine ( "Protocol version: {0}" , response.Response.ProtocolVersion );
Console.WriteLine ( "Content: {0}" , response.GetContentAsString ( Encoding.UTF8 ) );
```

### Few queries
```csharp
var request = new HttpFluentRequest ( new NetHttpBroker () )
	.Url ( "https://example.com/" )
	.Method ( RequestMethod.Get );

//synchronized variant
var arr = new int[] { 1 , 5, 10 };
// execute 3 query in loop
foreach (var item in arr){
    var response = request
    	.Parameter ( "index" , item )
    	.Send ();
    //output response
    Console.WriteLine(response.GetContentAsString ( Encoding.UTF8 ));
}

//asynchronized variant
var tasks = arr.Select ( a => 
    response
    	.Parameter ( "index" , a )
    	.SendAsync ()
);
//execute 3 query in parallels
var responses = await Task.WhenAll(tasks)
//output responses
foreach (var currentResponse in responses){
    Console.WriteLine(currentResponse.GetContentAsString ( Encoding.UTF8 ));
}
```
