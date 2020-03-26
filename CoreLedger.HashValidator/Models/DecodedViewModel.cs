using System.ComponentModel.DataAnnotations;

namespace CoreLedger.HashValidator.Models
{
	public class DecodedViewModel
	{
		[Display(Name = "Document Hash")]
		public string DocumentHash { get; set; }

		[Display(Name = "Document calculated Hash")]
		public string DocumentCalculatedHash { get; set; }

		public bool IsHashValid { get; set; }

		[Display(Name = "Document data decoded")]
		public string DecodedBase64 { get; set; }

		[Display(Name = "Document data used for hash")]
		public string HashSource { get; set; }
	}
}
