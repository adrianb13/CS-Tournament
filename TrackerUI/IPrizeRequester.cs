using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerUI
{
    //Returns prize info in createTournamentForm
    public interface IPrizeRequester
    {
        void PrizeComplete(PrizeModel model);
    }
}