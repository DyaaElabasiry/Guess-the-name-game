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
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            string name = textBox_enter_name.Text;
            Api_Messages.loginRequestPayload loginRequest = new Api_Messages.loginRequestPayload() { username = name }; // Fully qualify the type
            Request request = new Request() { requestType = RequestType.login, payload = loginRequest };
            SendRequest(client.GetStream(), request);
        }

        private async Task SendRequest(NetworkStream stream, Request request)
        {
            string message = JsonSerializer.Serialize(request);
            byte[] data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);
        }
    }
}
