using System.ComponentModel.DataAnnotations;

namespace CoreLedger.HashValidator.Models
{
	public class EncodedViewModel
	{
		[Required]
		[Display(Name ="Document data Base64")]
		public string EncodedData { get; set; }

		[Required]
		[Display(Name ="Document Hash")]
		public string DocumentHash { get; set; }
	}
}
