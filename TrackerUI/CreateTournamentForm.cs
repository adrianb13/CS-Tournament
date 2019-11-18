using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        public CreateTournamentForm()
        {
            InitializeComponent();
            InitializeLists();
        }

        private void InitializeLists()
        {
            selectTeamDropdown.DataSource = null;
            selectTeamDropdown.DataSource = availableTeams;
            selectTeamDropdown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        //Add Existing Team To Tournament
        private void AddTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)selectTeamDropdown.SelectedItem;

            //Removes them from available list and adds them to selected list
            availableTeams.Remove(t);
            selectedTeams.Add(t);

            if (t != null)
            {
                InitializeLists();
            }
        }

        //Call CreatePrizeForm
        private void CreatePrizeButton_Click(object sender, EventArgs e)
        {
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
        }

        //IPrizeRequester Interface used to pass prize info from CreatePrizeForm model to ListBox
        public void PrizeComplete(PrizeModel model)
        {
            selectedPrizes.Add(model);
            InitializeLists();
        }

        //Call CreateTeam Form
        private void CreateNewLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm tfrm = new CreateTeamForm(this);
            tfrm.Show();
        }

        //ITeamRequester Interface used to pass team info from CreateTeamForm model to ListBox
        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            InitializeLists();
        }

        private void DeleteSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            //Gets highlight Name from dropdown 
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;

            //Removes them from selected list and adds them to available list
            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);

                InitializeLists();
            }
        }

        private void DeleteSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            //Removes them from selected list
            if (p != null)
            {
                selectedPrizes.Remove(p);

                InitializeLists();
            }
        }

        private void CreateTournamentButton_Click(object sender, EventArgs e)
        {
            //Validate Fee
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);

            if (!feeAcceptable)
            {
                MessageBox.Show("You need to enter valid Entry Fee.", "Invalid Fee", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            //Create Tournament Model
            TournamentModel tm = new TournamentModel();

            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;

            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeams;

            //Creates Rounds Needed for tournament
            TournamentLogic.CreateRounds(tm);

            //Create Tournament Entry, Prize Entry, Team Entry
            GlobalConfig.Connection.CreateTournament(tm);
            
        }
    }
}
