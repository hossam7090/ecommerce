using System.ComponentModel.DataAnnotations;

namespace BullkyRazor.model
{
	public class Category
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(30)]
		public string Name { get; set; } = string.Empty;
		[Range(1, 100)]
		public int Order { get; set; }
	}
}
