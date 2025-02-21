using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using System.Text.Json;
using System.Text;
using Api_Messages;

namespace Client
{
    public partial class LoginForm : Form
    {
        TcpClient client;
        public LoginForm()
        {
            InitializeComponent();
            client = new TcpClient("127.0.0.1", 5000);
            ClientPlayer.client = client;
            
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            string name = textBox_enter_name.Text;
            ClientPlayer.name = name;
            loginRequestPayload loginRequest = new loginRequestPayload() { username = name }; 
            Request request = new Request() { Type = RequestType.login, payload = loginRequest };
            ClientPlayer.SendRequest( request);
            RoomsForm roomsForm = new RoomsForm();
            roomsForm.Show();
            this.Hide();

        }

       
    }
}
