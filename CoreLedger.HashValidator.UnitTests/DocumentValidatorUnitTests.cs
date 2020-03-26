using CoreLedger.HashValidator.Services;
using CoreLedger.HashValidator.UnitTests.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace CoreLedger.HashValidator.UnitTests
{
	public class DocumentValidatorUnitTests
	{
		[Test]
		public void DocumentValidator_ReturnsValidDocumentForValidInput()
		{
			//Arrange
			var documentValidator = new DocumentValidator();

			//Act
			var documentResponse = documentValidator.ValidateDocument(DocumentData.DocumentContentBase64, DocumentData.DocumentHash);

			//Assert
			Assert.IsTrue(documentResponse.IsHashValid);
			Assert.AreEqual(DocumentData.DocumentContentJson, documentResponse.DecodedBase64);
			Assert.AreEqual(DocumentData.DocumentContentJsonToHash, documentResponse.HashSource);
			Assert.AreEqual(DocumentData.DocumentHash, documentResponse.DocumentHash);
		}

		[Test]
		public void DocumentValidator_CheckPreparingDocumentToHash()
		{
			//Arrange
			var documentValidator = new DocumentValidator();

			//Act
			var documentContent = documentValidator.DecodeDocument(DocumentData.DocumentContentBase64);
			var documentJsonToHash = documentValidator.GetDocumentContentToHash(documentContent);
			var documentJson = JObject.Parse(documentJsonToHash);

			//Assert
			Assert.AreEqual(DocumentData.DocumentContentJsonToHash, documentJsonToHash);
			Assert.IsFalse(documentJson.ContainsKey("footer"));
		}

		[Test]
		public void DocumentValidator_CheckHashFunction()
		{
			//Arrange
			var documentValidator = new DocumentValidator();

			//Act
			var hashResponse = documentValidator.CalculateDocumentHash(DocumentData.DocumentContentJsonToHash);

			//Assert
			Assert.AreEqual(DocumentData.DocumentHash, hashResponse);
		}
	}
}