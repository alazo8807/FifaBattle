using FifaBattle.Core.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FifaBattle.ViewModels
{
	public class TournamentViewModel
	{
		public string Title { get; set; }

		public string Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int TournamentTypeId { get; set; }

		public int NumberOfPlayers { get; set; }

		public int NumberOfMatches { get; set; }

		public IEnumerable<TournamentType> TournamentTypes { get; set; }

		public IList<Player> Players { get; set; }

		//public string Action
		//{
		//	get
		//	{
		//		return Id == 0 ? "Create" : "Update";
		//	}
		//}
	}
}