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
    public partial class CreateTeamForm : Form
    {
        private ITeamRequester callingForm;
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
            //CreateSample();
            WireUpLists();
        }

        /*private void CreateSample()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Adrian", LastName = "Briones" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Adrian", LastName = "Briones" });
        }*/

        // Loads DropDown List and Selected List Box
        private void WireUpLists()
        {
            //Set to null will cause a refresh when data is add.
            selectTeamMemberDropdown.DataSource = null;
            selectTeamMemberDropdown.DataSource = availableTeamMembers;
            selectTeamMemberDropdown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }

        private void CreateMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();

                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.CellPhoneNumber = cellphoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);

                //Gets added to Team Member List
                selectedTeamMembers.Add(p);
                WireUpLists();

                //Clears Form
                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("You need to fill in all of the fields");
            }
        }

        private bool ValidateForm()
        {
            if (firstNameValue.Text.Length == 0)
            {
                return false;
            }

            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }

            if (emailValue.Text.Length == 0)
            {
                return false;
            }

            if (cellphoneValue.Text.Length == 0)
            {
                return false;
            }
            return true;
        }

        private void AddMemberButton_Click(object sender, EventArgs e)
        {
            //Gets highlight Name from dropdown 
            PersonModel p = (PersonModel)selectTeamMemberDropdown.SelectedItem;

            //Removes them from available list and adds them to selected list
            availableTeamMembers.Remove(p);
            selectedTeamMembers.Add(p);

            if(p != null)
            {
                WireUpLists();
            }
        }

        private void RemoveSelectedTeamMemberButton_Click(object sender, EventArgs e)
        {
            //Gets highlight Name from dropdown 
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            //Removes them from available list and adds them to selected list
            if(p != null)
            { 
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();
            }
        }

        private void CreateTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel();

            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(t);

            //Sends complete info to caller (whoever wants the info)
            callingForm.TeamComplete(t);

            this.Close();
        }
    }
}
