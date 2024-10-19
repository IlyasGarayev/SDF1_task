using System;
using System.Windows.Forms;
using Sdf.Core.Models;
using Sdf.Data.Repository;

namespace Sdf.UI
{
    public partial class LoginForm : Form
    {
        private readonly string path = "C:\\Users\\student\\Desktop\\Sdf\\Sdf\\Data\\Database\\users.json";
        TextBox txtUsername, txtPassword;

        public LoginForm()
        {

            Component();
        }

        void Component()
        {
            var lblUsername = new Label
            {
                Text = "Username:",
                Location = new Point(50, 50),
                AutoSize = true
            };
            this.Controls.Add(lblUsername);


            txtUsername = new TextBox
            {
                Name = "txtUsername",
                Location = new Point(150, 50),
                Width = 100
            };
            this.Controls.Add(txtUsername);

            var lblPassword = new Label
            {
                Text = "Password:",
                Location = new Point(50, 100),
                AutoSize = true
            };
            this.Controls.Add(lblPassword);


            txtPassword = new TextBox
            {
                Name = "txtPassword",
                Location = new Point(150, 100),
                Width = 100,
                PasswordChar = '*'
            };
            this.Controls.Add(txtPassword);

            var btnLogin = new Button
            {
                Text = "Login",
                Location = new Point(150, 150),
                Width = 100
            };

            btnLogin.Click += btnLogin_Click;
            this.Controls.Add(btnLogin);

            var btnRegister = new Button
            {
                Text = "Register",
                Location = new Point(150, 200),
                Width = 100
            };
            btnRegister.Click += btnRegister_Click;
            this.Controls.Add(btnRegister);


        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            //this.Close();
            registerForm.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            var userRepository = new Repository<User>(path); 
            var users = userRepository.GetAll() ?? new List<User>(); 

            var user = users.Find(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                MessageBox.Show("Login successful!");
                var gameResultsForm = new GameResultsForm();
                gameResultsForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }

    }
}
