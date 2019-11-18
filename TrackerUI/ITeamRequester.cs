using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerUI
{
    //Returns Team from Create Team Form
    public interface ITeamRequester
    {
        void TeamComplete(TeamModel model);
    }
}