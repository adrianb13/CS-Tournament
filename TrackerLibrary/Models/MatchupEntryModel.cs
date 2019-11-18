using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {
        public int Id { get; set; }

        //Team Id
        public int TeamCompetingId { get; set; }

        //Represents one team
        public TeamModel TeamCompeting { get; set; }

        //Represents score for tema
        public double Score { get; set; }

        //Matchup Id
        public int ParentMatchupId { get; set; }

        //Represents the matchup that this team came from as a winner
        public MatchupModel ParentMatchup { get; set; }
    }
}