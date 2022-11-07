using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTS_Test.DataManager
{
	public class FilterUpdateEventArgs : EventArgs
	{
		public string  name		{ get; set; }
		public int     code		{ get; set; }
		public string  bar_code { get; set; }
		public decimal price	{ get; set; }
	}
}
