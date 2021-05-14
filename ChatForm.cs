using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UDPApp
{
    public partial class ChatForm : Form
    {
        private static IPAddress remoteIPAddress = IPAddress.Parse("127.0.0.1");

        public ChatForm()
        {
            InitializeComponent();
            SendBtn.Enabled = true;
            WelcomeLabel.Text = "Welcome " + LoginForm.UserName;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessage(MessageTextBox.Text);
                ChatListBox.Items.Add(LoginForm.UserName + ": " + MessageTextBox.Text);
                MessageTextBox.Clear();            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            Thread Receive = new Thread(new ThreadStart(Receiver));
            Receive.Start();
        }

        private void SendMessage(string userMessage)
        {
            UdpClient sender = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, LoginForm.RemotePort);

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(userMessage);
                sender.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        public void Receiver()
        {
            UdpClient receivingUdpClient = new UdpClient(LoginForm.LocalPort);
            IPEndPoint RemoteIpEndPoint = null;

            try
            {
                while (true)
                {
                    // Receive datagram
                    byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    string returnData = Encoding.UTF8.GetString(receiveBytes);
                    ChatListBox.Items.Add(" --> " + returnData.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
