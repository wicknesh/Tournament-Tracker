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
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPersonAll();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        public CreateTeamForm()
        {
            InitializeComponent();

            /*CreateSampleData();*/

            WireUpLists();
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Vignesh", LastName = "J S" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Sivapriya", LastName = "Pillai" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Gautham", LastName = "Jayan" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Vaishnav", LastName = "Sundaram" });
        }

        private void WireUpLists()
        {
            selectTeamMemberDropDown.DataSource = null;
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
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
                p = db.CreatePerson(p);

                selectedTeamMembers.Add(p);

                WireUpLists();


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

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists(); 
            }

            if(p == null)
            {
                MessageBox.Show("No member selected. Please select a member to add to list!");
            }
        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;
            if (p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists(); 
            }
            if(p == null)
            {
                MessageBox.Show("No member selected. Please select a member to remove!");
            }
        }
    }
}
