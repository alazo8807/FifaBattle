using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FifaBattle.Core.Domain
{
	public class Player
	{
		public string Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[ForeignKey("Team")]
		public int TeamId { get; set; }
		public Team Team { get; set; }

		[ForeignKey("Tournament")]
		public string TournamentId { get; set; }
		public Tournament Tournament { get; set; }
	}
}