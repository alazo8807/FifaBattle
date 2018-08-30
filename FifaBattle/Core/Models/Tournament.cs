﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FifaBattle.Core.Models
{
	public class Tournament
	{
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

		public ICollection<Player> Players { get; set; }

		//protected Tournament()
		//{
		//}

		//public Tournament(string name, int numberOfPlayers, int tournamentTypeId, string userId)
		//{
		//	if (name == null)
		//		throw new ArgumentNullException("name");

		//	if (numberOfPlayers == 0)
		//		throw new ArgumentException("numberOfPlayers");

		//	if (tournamentTypeId == 0)
		//		throw new ArgumentException("tournamentTypeId");

		//	if (userId == null)
		//		throw new ArgumentNullException("userId");

		//	Players = new Collection<Player>();

		//	Name = name;
		//	NumberOfPlayers = numberOfPlayers;
		//	TournamentTypeId = tournamentTypeId;
		//	CreatorId = userId;
		//	DateCreated = DateTime.Now;
		//}

		public void AddPlayers(ICollection<Player> players)
		{
			if (players == null)
				throw new ArgumentNullException("players");

			foreach (var player in players)
			{
				player.TournamentId = Id;

				Players.Add(player);
			}
		}
	}
}