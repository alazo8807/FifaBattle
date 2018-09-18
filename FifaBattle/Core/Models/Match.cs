using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FifaBattle.Core.Models
{
	public class Match
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key, Column(Order = 1)]
		public int Id { get; set; }

		[Key, Column(Order = 2)]
		[ForeignKey("Tournament")]
		public string TournamentId { get; set; }

		public Tournament Tournament { get; set; }

		[ForeignKey("HomeTeam")]
		public int HomeTeamId { get; set; }

		public Team HomeTeam { get; set; }

		public int HomeTeamGoals { get; set; }

		[ForeignKey("AwayTeam")]
		public int AwayTeamId { get; set; }

		public Team AwayTeam { get; set; }

		public int AwayTeamGoals { get; set; }

		public bool IsFinished { get; set; }

		public short RoundNbr { get; set; }
	}
}