using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        void CreatePrize(PrizeModel model);
        void CreatePerson(PersonModel model);
        void CreateTeam(TeamModel model);
        void CreateTournament(TournamentModel model);
        void UpdateMatchup(MatchupModel model);

        //Loads All Current People
        List<PersonModel> GetPerson_All();
        //Loads All Teams
        List<TeamModel> GetTeam_All();
        //Loads All Tournaments
        List<TournamentModel> GetTournament_All();
    }
}