﻿using System;
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
using TrackerLibrary.DataAccess;


namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        IPrizeRequester callingForm;
        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel(
                    placeNumberValue.Text, 
                    placeNameValue.Text, 
                    prizeAmountValue.Text, 
                    prizePercentageValue.Text);

                GlobalConfig.Connection.CreatePrize(model);

                callingForm.PrizeComplete(model);

                this.Close();

                /*placeNumberValue.Text = "";
                placeNameValue.Text = "";
                prizeAmountValue.Text = "0";
                prizePercentageValue.Text = "0";*/
            }
            else
            {

                MessageBox.Show("This form has invalid information. Please check and try again!");
            }
        }

        private bool ValidateForm()
        {
            bool output = true;

            /*int placeNumber = 0;*/
            bool placeNumberValid = int.TryParse(placeNumberValue.Text, out int placeNumber);

            if (!placeNumberValid)
            {
                output = false;
            }

            if(placeNumber < 1)
            {
                output = false;
            }

            if(placeNameValue.Text.Length == 0)
            {
                output = false;
            }

            /*decimal prizeAmount = 0;
            double prizePercentage = 0;*/
            bool prizeAmountValid = decimal.TryParse(prizeAmountValue.Text, out decimal prizeAmount);
            bool prizePercentageValid = double.TryParse(prizePercentageValue.Text, out double prizePercentage);

            if (!prizeAmountValid || !prizePercentageValid)
            {
                output = false;
            }

            if(prizeAmount <= 0 && prizePercentage <=0)
            {   
                output = false;
            }

            if(prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }

            return output;
        }
    }
}
