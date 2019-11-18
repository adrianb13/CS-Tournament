using System;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        private IPrizeRequester callingForm;
        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
        }

        private void CreatePrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel(placeNameValue.Text, placeNumberValue.Text, prizeAmountValue.Text, prizePercentageValue.Text);
                
                GlobalConfig.Connection.CreatePrize(model);

                //Sends complete info to caller (whoever wants the info)
                callingForm.PrizeComplete(model);
                this.Close();

                //placeNameValue.Text = "";
                //placeNumberValue.Text = "";
                //prizeAmountValue.Text = "0";
                //prizePercentageValue.Text = "0";

            }
            else
            {
                MessageBox.Show("This form has invalid information. Please check values.");
            }
        }

        private bool ValidateForm()
        {
            bool output = true;
            int placeNumber = 0;

            if ((int.TryParse(placeNumberValue.Text, out placeNumber)) == false)
            {
                output = false;
            }

            if (placeNumber < 1)
            {
                output = false;
            }

            if(placeNameValue.Text.Length == 0)
            {
                output = false;
            }

            decimal prizeAmount = 0;
            double prizePercentage = 0;

            //Must have prizeAmount or prizePercentage
            if((decimal.TryParse(prizeAmountValue.Text, out prizeAmount)) == false || (double.TryParse(prizePercentageValue.Text, out prizePercentage)) == false)
            {
                output = false;
            }

            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }

            //Prize Percentage must be between 0-100%
            if (prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }

            return output;
        }
    }
}
