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
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = new List<PersonModel>();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        public CreateTeamForm()
        {
            InitializeComponent();
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.PhoneNumber = phoneNumberValue.Text;

                IDataConnection db = GlobalConfig.Connection;
                db.CreatePerson(p);

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                phoneNumberValue.Text = "";
            }
            else 
            {
                MessageBox.Show("This form contains invalid information. Please check it and try again!");
            }

        }

        private bool ValidateForm()
        {
            if(firstNameValue.Text.Length == 0 ||
                lastNameValue.Text.Length == 0 ||
                emailValue.Text.Length == 0 ||
                phoneNumberValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }

        /*private void CreateTeamForm_Load(object sender, EventArgs e)
        {

        }

        private void selectTeamMemberDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addTeamMemberButton_Click(object sender, EventArgs e)
        {

        }

        private void teamOneScoreLabel_Click(object sender, EventArgs e)
        {

        }

        private void teamOneScoreValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {

        }*/
    }
}
