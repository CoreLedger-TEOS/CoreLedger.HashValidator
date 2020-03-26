using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CoreLedger.HashValidator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreLedger.HashValidator.Services
{
	public class DocumentValidator
	{
		private readonly string FooterKeyName = "footer";

		/// <summary>
		/// Validate if document content and document hash are valid. It calculates the hash for the document content and compares it with the one from input.
		/// </summary>
		/// <param name="documentContentBase64">A string representing document content in a Base64 format</param>
		/// <param name="documentHash">Hash of the document which is validated</param>
		/// <returns></returns>
		public DecodedViewModel ValidateDocument(string documentContentBase64, string documentHash)
		{
			var decodedDocumentContent = DecodeDocument(documentContentBase64);
			var documentContentToHash = GetDocumentContentToHash(decodedDocumentContent);
			var calculatedDocumentHash = CalculateDocumentHash(documentContentToHash);
			bool isHashValid = documentHash.Equals(calculatedDocumentHash, StringComparison.InvariantCultureIgnoreCase);

			return new DecodedViewModel
			{
				DecodedBase64 = decodedDocumentContent,
				HashSource = documentContentToHash,
				DocumentHash = documentHash,
				DocumentCalculatedHash = calculatedDocumentHash,
				IsHashValid = isHashValid
			};
		}

		/// <summary>
		/// Decode a Base64 string to UTF-8 format
		/// </summary>
		/// <param name="documentContentBase64">A string representing document content in a Base64 format</param>
		/// <returns></returns>
		public string DecodeDocument(string documentContentBase64)
		{
			byte[] encodedData = Convert.FromBase64String(documentContentBase64);
			return Encoding.UTF8.GetString(encodedData);
		}

		/// <summary>
		/// Modify a JSON document for the hash function. The function removes formatting of the JSON and the footer property
		/// </summary>
		/// <param name="documentContentJson"></param>
		/// <returns></returns>
		public string GetDocumentContentToHash(string documentContentJson)
		{
			var documentContentToHash = JObject.Parse(documentContentJson);

			//hash of the document is calculated without the footer property
			documentContentToHash.Remove(FooterKeyName);
			return documentContentToHash.ToString(Formatting.None);
		}

		/// <summary>
		/// Calculate the Hash of a document using SHA256 algorithm.
		/// </summary>
		/// <param name="documentContentToHash"></param>
		/// <returns></returns>
		public string CalculateDocumentHash(string documentContentToHash)
		{
			using (var algorithm = SHA256.Create())
			{
				var hashBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(documentContentToHash));
				var result = string.Join("", hashBytes.Select(f => f.ToString("x2")));
				return result;
			}
		}
	}
}
