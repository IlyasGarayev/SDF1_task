using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sdf.Core.Models;
using Sdf.Data.Repository;
using static System.Formats.Asn1.AsnWriter;

namespace Sdf.UI
{
    public partial class GameResultsForm : Form
    {
        private readonly string pathTeams = "C:\\Users\\student\\Desktop\\Sdf\\Sdf\\Data\\Database\\teams.json";
        private readonly string pathScore = "C:\\Users\\student\\Desktop\\Sdf\\Sdf\\Data\\Database\\score.json";
        ComboBox cmbTeam1, cmbTeam2;
        TextBox txtScore1, txtScore2;
        DataGridView dataGridViewResults;

        public GameResultsForm()
        {
            Component();
            LoadTeams();
            LoadScores();
        }

        void Component()
        {
           
            var lblTeam1 = new Label
            {
                Text = " Team 1:",
                Location = new Point(50, 50),
                AutoSize = true
            };
            this.Controls.Add(lblTeam1);

            
            cmbTeam1 = new ComboBox
            {
                Location = new Point(150, 50),
                Width = 200
            };
            this.Controls.Add(cmbTeam1);

            
            var lblTeam2 = new Label
            {
                Text = " Team 2:",
                Location = new Point(50, 100),
                AutoSize = true
            };
            this.Controls.Add(lblTeam2);

            
            cmbTeam2 = new ComboBox
            {
                Location = new Point(150, 100),
                Width = 200
            };
            this.Controls.Add(cmbTeam2);

            
            var lblScore1 = new Label
            {
                Text = "Score for Team 1:",
                Location = new Point(50, 150),
                AutoSize = true
            };
            this.Controls.Add(lblScore1);

            
            txtScore1 = new TextBox
            {
                Location = new Point(150, 150),
                Width = 50
            };
            this.Controls.Add(txtScore1);

            
            var lblScore2 = new Label
            {
                Text = "Score for Team 2:",
                Location = new Point(250, 150),
                AutoSize = true
            };
            this.Controls.Add(lblScore2);

            
            txtScore2 = new TextBox
            {
                Location = new Point(350, 150),
                Width = 50
            };
            this.Controls.Add(txtScore2);

            
            var btnAddResult = new Button
            {
                Text = "Add Result",
                Location = new Point(150, 200),
                Width = 200
            };
            btnAddResult.Click += btnAddResult_Click;
            this.Controls.Add(btnAddResult);

            
            dataGridViewResults = new DataGridView
            {
                Location = new Point(50, 250),
                Width = 600,
                Height = 200,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dataGridViewResults);
        }



        void LoadTeams()
        {
            var teamRepository = new Repository<Team>(pathTeams);
            var teams = teamRepository.GetAll();

            foreach (var team in teams)
            {
                cmbTeam1.Items.Add(team.Name);
                cmbTeam2.Items.Add(team.Name);
            }
        }


        void LoadScores()
        {
            var scoreRepository = new Repository<Score>(pathScore);
            var scores = scoreRepository.GetAll();

            dataGridViewResults.DataSource = scores;
        }


        private void btnAddResult_Click(object sender, EventArgs e)
        {
            string team1 = cmbTeam1.SelectedItem?.ToString();
            string team2 = cmbTeam2.SelectedItem?.ToString();
            if (team1 == null || team2 == null || string.IsNullOrWhiteSpace(txtScore1.Text) || string.IsNullOrWhiteSpace(txtScore2.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            int score1 = int.Parse(txtScore1.Text);
            int score2 = int.Parse(txtScore2.Text);

            var score = new Score { Team1 = team1, Team2 = team2, Score1 = score1, Score2 = score2 };

            var scoreRepository = new Repository<Score>(pathScore);
            scoreRepository.Add(score);

            MessageBox.Show("Result added successfully!");
            LoadScores();
        }
    }
}
