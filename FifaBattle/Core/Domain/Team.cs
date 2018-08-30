using System.ComponentModel.DataAnnotations;

namespace FifaBattle.Core.Domain
{
	public class Team
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public bool IsOfficialTeam { get; set; }

	}
}