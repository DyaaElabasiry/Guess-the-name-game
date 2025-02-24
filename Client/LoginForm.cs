using System;
using System.Windows.Forms;
using Api_Messages;

namespace Client
{
    public partial class LoginForm : UserControl
    {
        private MainForm mainForm;
        public LoginForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            string name = textBox_enter_name.Text;
            ClientPlayer.PlayerName = name;
            loginRequestPayload loginRequest = new loginRequestPayload() { username = name };
            Request request = new Request() { Type = RequestType.login, payload = loginRequest };
            ClientPlayer.SendRequest(request);

            mainForm.ShowRoomsPanel();
            
        }
    }
}
