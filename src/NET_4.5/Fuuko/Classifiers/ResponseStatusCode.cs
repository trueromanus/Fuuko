
namespace Fuuko.Classifiers {

	/// <summary>
	/// Status code.
	/// </summary>
	public enum ResponseStatusCode {

		Unknown = 0 ,

		//Information codes

		Continue = 100 ,

		Switching = 101 ,

		Processing = 102 ,

		//Success codes

		OK = 200 ,

		Created = 201 ,

		Accepted = 202 ,

		NonAuthoritativeInformation = 203 ,

		NoContent = 204 ,

		ResetContent = 205 ,

		PartialContent = 206 ,

		MultiStatus = 207 ,

		IMUsed = 226 ,

		//Redirection

		MultipleChoices = 300 ,

		MovedPermanently = 301 ,

		MovedTemporarily = 302 ,

		Found = 302 ,

		SeeOther = 303 ,

		NotModified = 304 ,

		UseProxy = 305 ,

		RedirectionReserved = 306 ,

		TemporaryRedirect = 307 ,

		//Client Error

		BadRequest = 400 ,

		Unauthorized = 401 ,

		PaymentRequired = 402 ,

		Forbidden = 403 ,

		NotFound = 404 ,

		MethodNotAllowed = 405 ,

		NotAcceptable = 406 ,

		ProxyAuthenticationRequired = 407 ,

		RequestTimeout = 408 ,

		Conflict = 409 ,

		Gone = 410 ,

		LengthRequired = 411 ,

		PreconditionFailed = 412 ,

		RequestEntityTooLarge = 413 ,

		RequestURITooLarge = 414 ,

		UnsupportedMediaType = 415 ,

		RequestedRangeNotSatisfiable = 416 ,

		ExpectationFailed = 417 ,

		UnprocessableEntity = 422 ,

		Locked = 423 ,

		FailedDependency = 424 ,

		UnorderedCollection = 425 ,

		UpgradeRequired = 426 ,

		PreconditionRequired = 428 ,

		TooManyRequests = 429 ,

		RequestHeaderFieldsTooLarge = 431 ,

		RequestedHostUnavailable = 434 ,

		CloseConnectionWithoutHeader = 444 , // non standart code for nginx http://nginx.org/en/docs/http/ngx_http_rewrite_module.html#return

		RetryWith = 449 ,

		UnavailableForLegalReasons = 451 ,

		//Server Error

		InternalServerError = 500 ,
		
		NotImplemented = 501 ,
		
		BadGateway = 502 ,
		
		ServiceUnavailable = 503 ,
		
		GatewayTimeout = 504 ,
		
		HTTPVersionNotSupported = 505 ,
		
		VariantAlsoNegotiates = 506 ,
		
		InsufficientStorage = 507 ,
		
		LoopDetected = 508 ,
		
		BandwidthLimitExceeded = 509 ,
		
		NotExtended = 510 ,
		
		NetworkAuthenticationRequired = 511

	};

}
