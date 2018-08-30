using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FifaBattle.Core.Domain
{
	public class TournamentType
	{
		public int Id { get; set; }

		[StringLength(20)]
		public string Name { get; set; }
	}
}