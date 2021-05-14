using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPApp
{
    public partial class LoginForm : Form
    {
        public static string UserName = "User";
        public static int LocalPort = 5001;
        public static int RemotePort = 5002;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            UserName = UserNameTextBox.Text;
            LocalPort = int.Parse(LocalPortTextBox.Text);
            RemotePort = int.Parse(RemotePortTextBox.Text);
            this.Hide();
            new ChatForm().Show();
        }
    }
}
