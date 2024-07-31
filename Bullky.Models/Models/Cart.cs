using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullky.Models.Models
{
	public class Cart
	{
		private static Cart instance = null;
        private Cart() { }
		public int Id { get; set; }
		public int UserId { get; set; }
		public List<CartItem> Items { get; set;}
		public static Cart GetInstance()
		{
			if (instance == null)
			{
				instance = new Cart();
			}
			return instance;
		}
	}
}
