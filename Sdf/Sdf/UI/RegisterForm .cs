using System;
using System.Windows.Forms;
using Sdf.Core.Models;
using Sdf.Data.Repository;

namespace Sdf.UI
{
    public partial class RegisterForm : Form
    {
        private readonly string path = "C:\\Users\\student\\Desktop\\Sdf\\Sdf\\Data\\Database\\users.json";

        TextBox txtUsername, txtPassword;

        public RegisterForm()
        {
            Component();
        }

        void Component()
        {

            var lblUsername = new Label
            {
                Text = "Username:",
                Location = new System.Drawing.Point(50, 50),
                AutoSize = true
            };
            this.Controls.Add(lblUsername);


            txtUsername = new TextBox
            {
                Name = "txtUsername",
                Location = new System.Drawing.Point(150, 50),
                Width = 100
            };
            this.Controls.Add(txtUsername);


            var lblPassword = new Label
            {
                Text = "Password:",
                Location = new System.Drawing.Point(50, 100),
                AutoSize = true
            };
            this.Controls.Add(lblPassword);


            txtPassword = new TextBox
            {
                Name = "txtPassword",
                Location = new System.Drawing.Point(150, 100),
                Width = 100,
                PasswordChar = '*'
            };
            this.Controls.Add(txtPassword);


            var btnRegister = new Button
            {
                Text = "Register",
                Location = new Point(150, 150),
                Width = 100
            };
            btnRegister.Click += btnRegister_Click;
            this.Controls.Add(btnRegister);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            var userRepository = new Repository<User>(path);
            var users = userRepository.GetAll() ?? new List<User>(); 

            if (users.Exists(u => u.Username == username))
            {
                MessageBox.Show("Username already exists.");
                return;
            }

            var newUser = new User { Username = username, Password = password };
            userRepository.Add(newUser);

            MessageBox.Show("Registration successful!");
            LoginForm loginForm =new LoginForm();

            this.Close();
        }

    }
}
