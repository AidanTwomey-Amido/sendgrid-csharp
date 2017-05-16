﻿// <copyright file="SendGridClient.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace SendGrid
{
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading;
	using System.Threading.Tasks;
	using Helpers.Mail;

	/// <summary>
	/// A HTTP client wrapper for interacting with SendGrid's API
	/// </summary>
	public interface ISendGridClient
	{
		/// <summary>
		/// Gets or sets the path to the API resource.
		/// </summary>
		string UrlPath { get; set; }

		/// <summary>
		/// Gets or sets the API version.
		/// </summary>
		string Version { get; set; }

		/// <summary>
		/// Gets or sets the request media type.
		/// </summary>
		string MediaType { get; set; }

		/// <summary>
		/// Add the authorization header, override to customize
		/// </summary>
		/// <param name="header">Authorization header</param>
		/// <returns>Authorization value to add to the header</returns>
		AuthenticationHeaderValue AddAuthorization( KeyValuePair<string, string> header );

		/// <summary>
		/// Make the call to the API server, override for testing or customization
		/// </summary>
		/// <param name="request">The parameters for the API call</param>
		/// <param name="cancellationToken">Cancel the asynchronous call</param>
		/// <returns>Response object</returns>
		Task<Response> MakeRequest( HttpRequestMessage request, CancellationToken cancellationToken = default( CancellationToken ) );

		/// <summary>
		/// Prepare for async call to the API server
		/// </summary>
		/// <param name="method">HTTP verb</param>
		/// <param name="requestBody">JSON formatted string</param>
		/// <param name="queryParams">JSON formatted query paramaters</param>
		/// <param name="urlPath">The path to the API endpoint.</param>
		/// <param name="cancellationToken">Cancel the asynchronous call.</param>
		/// <returns>Response object</returns>
		/// <exception cref="Exception">The method will NOT catch and swallow exceptions generated by sending a request
		/// through the internal http client. Any underlying exception will pass right through.
		/// In particular, this means that you may expect
		/// a TimeoutException if you are not connected to the internet.</exception>
		Task<Response> RequestAsync( SendGridClient.Method method, string requestBody = null, string queryParams = null, string urlPath = null, CancellationToken cancellationToken = default( CancellationToken ) );

		/// <summary>
		/// Make a request to send an email through SendGrid asychronously.
		/// </summary>
		/// <param name="msg">A SendGridMessage object with the details for the request.</param>
		/// <param name="cancellationToken">Cancel the asychronous call.</param>
		/// <returns>A Response object.</returns>
		Task<Response> SendEmailAsync( SendGridMessage msg, CancellationToken cancellationToken = default( CancellationToken ) );
	}
}