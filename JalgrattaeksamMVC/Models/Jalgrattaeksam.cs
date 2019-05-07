using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JalgrattaeksamMVC.Models
{
	public class Jalgrattaeksam
	{
		public int Id { get; set; }
		public string Eesnimi { get; set; }
		public string Perenimi { get; set; }
		public int Teooria { get; set; } = -1;
		public int Slaalom { get; set; } = -1;
		public int Ringtee { get; set; } = -1;
		public int Uulits { get; set; } = -1;
		public int Luba { get; set; } = -1;
	}
}