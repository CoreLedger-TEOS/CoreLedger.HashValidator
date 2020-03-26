using System;
using System.Diagnostics;
using CoreLedger.HashValidator.Models;
using CoreLedger.HashValidator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreLedger.HashValidator.Controllers
{
	public class EncodeController : Controller
	{
		private readonly ILogger<EncodeController> _logger;
		private readonly DocumentValidator _documentValidator;

		public EncodeController(ILogger<EncodeController> logger, DocumentValidator documentValidator)
		{
			_logger = logger;
			_documentValidator = documentValidator;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(EncodedViewModel encodedViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(encodedViewModel);
			}

			try
			{
				var validatedDocument = _documentValidator.ValidateDocument(encodedViewModel.EncodedData.Trim(), encodedViewModel.DocumentHash.Trim());
				return View("Decoded", validatedDocument);
			}
			catch (FormatException e)
			{
				_logger.LogWarning(e, $"An exception was thrown at decoding. Could not decode base64. Check your input {encodedViewModel.EncodedData.Trim()}");
				ModelState.AddModelError("EncodedData", "Invalid format. Could not decode base64.");
				return View(encodedViewModel);
			}
			catch (JsonReaderException e)
			{
				_logger.LogWarning(e, $"An exception was thrown at parsing the content to JSON. Check if document encoded data contains a JSON. Input {encodedViewModel.EncodedData.Trim()}");
				ModelState.AddModelError("EncodedData", "Invalid format. Could not find valid JSON in decoded text");
				return View(encodedViewModel);
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
