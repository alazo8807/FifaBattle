using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FifaBattle.Core.Models
{
	public class Tournament
	{
		[Key]
		public string Id { get; set; }

		[Required]
		public string Name { get; set; }

		[ForeignKey("TournamentType"), Required(ErrorMessage = "Please Select a Tournament Type.")]
		[Display(Name = "Tournament Type")]
		public int TournamentTypeId { get; set; }

		public TournamentType TournamentType { get; set; }

		[Display(Name = "Number Of Players")]
		public int NumberOfPlayers { get; set; }

		[Display(Name = "Number Of Matches")]
		public int NumberOfMatches { get; set; }

		public DateTime DateCreated { get; set; }

		public ApplicationUser Creator { get; set; }

		[Required]
		public string CreatorId { get; set; }

		public List<Player> Players { get; set; }
	}
}