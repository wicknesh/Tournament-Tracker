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
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeamAll();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        public CreateTournamentForm()
        {
            InitializeComponent();

            WireUpLists();
        }

        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentPlayersListBox.DataSource = null;
            tournamentPlayersListBox.DataSource = selectedTeams;
            tournamentPlayersListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;

            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }

            if (t == null)
            {
                MessageBox.Show("No member selected. Please select a member to add to list!");
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            // Call the create prize form
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();     
        }

        public void PrizeComplete(PrizeModel model)
        {
            // Get back from the form a prize model
            // Take the prize model and put into the list of selected prizes
            selectedPrizes.Add(model);
            WireUpLists();
        }      

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm form = new CreateTeamForm(this);
            form.Show();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void deleteSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentPlayersListBox.SelectedItem;

            if(t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);

                WireUpLists();
            }

            if (t == null)
            {
                MessageBox.Show("No team selected. Please select a team to remove!");
            }


        }

        private void clearTeamsListBoxButton_Click(object sender, EventArgs e)
        {
            //tournamentPlayersListBox.DataSource = null;
            foreach (var item in selectedTeams)
            {
                availableTeams.Add(item);
            }
            selectedTeams.Clear();
            WireUpLists();
        }

        private void deletedSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if( p!= null)
            {
                selectedPrizes.Remove(p);
                WireUpLists();
            }
        }

        private void clearPrizesListBoxButton_Click(object sender, EventArgs e)
        {
            selectedPrizes.Clear();
            WireUpLists();
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            //Create our Tournament model
            TournamentModel tmodel = new TournamentModel();
            tmodel.TournamentName = tournamentNameValue.Text;

            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out decimal fee);
            if (!feeAcceptable)
            {
                MessageBox.Show("Please enter a valid fee!", "Invalid Fee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tmodel.EntryFee = fee;

            tmodel.Prizes = selectedPrizes;
            tmodel.EnteredTeams = selectedTeams;

            // TODO - Wire up matchups
            TournamentLogic.CreateRounds(tmodel);

            // Create Tournament entry
            // Create all of the prizes entries
            // Create all of the team entries
            GlobalConfig.Connection.CreateTournament(tmodel);
        }
    }
}
