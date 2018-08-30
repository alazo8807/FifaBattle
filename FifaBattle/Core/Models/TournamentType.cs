using System.ComponentModel.DataAnnotations;

namespace FifaBattle.Core.Models
{
	public class TournamentType
	{
		public int Id { get; set; }

		[StringLength(20)]
		public string Name { get; set; }
	}
}