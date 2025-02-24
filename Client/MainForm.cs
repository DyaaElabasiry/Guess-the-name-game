using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Api_Messages;

namespace Client
{
    public partial class MainForm : Form
    {
        private TcpClient client;
        private LoginForm loginForm;
        private RoomsForm roomsForm;
        private GameForm gameForm;
        private WaitingForOtherPlayers waitingForOtherPlayers;

        public MainForm()
        {
            InitializeComponent();
            client = new TcpClient("127.0.0.1", 5000);
            ClientPlayer.client = client;

            loginForm = new LoginForm(this);
            roomsForm = new RoomsForm(this);
            gameForm = new GameForm(this);
            waitingForOtherPlayers = new WaitingForOtherPlayers();

            waitingForOtherPlayers.Dock = DockStyle.Fill;

            panelLogin.Controls.Add(loginForm);
            panelRooms.Controls.Add(roomsForm);
            panelGame.Controls.Add(gameForm);
            panelWaiting.Controls.Add(waitingForOtherPlayers);

            ShowLoginPanel();

            Task.Run(() => { HandleResponses(); });
        }

        private async Task HandleResponses()
        {
            byte[] buffer = new byte[2024];
            while (true)
            {
                int bytesRead = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Response response = JsonSerializer.Deserialize<Response>(message);

                if (response.Type == ResponseType.yourTurn)
                {
                    gameForm.Invoke(new Action(() => gameForm.HandleYourTurn(response)));
                }
                else if (response.Type == ResponseType.gameOver)
                {
                    gameForm.Invoke(new Action(() => gameForm.HandleGameOver()));
                }
                else if (response.Type == ResponseType.getRooms)
                {
                    roomsForm.Invoke(new Action(() => roomsForm.DisplayRooms(response.payload)));
                }
                else if (response.Type == ResponseType.spectateRoom)
                {

                    gameForm.Invoke(new Action(() => gameForm.spectateGame(response)));
                }
                else if(response.Type == ResponseType.startGame)
                {
                    ShowGamePanel(false);
                }
            }
        }

        public void ShowLoginPanel()
        {
            panelLogin.BringToFront();
        }

        public void ShowRoomsPanel()
        {
            panelRooms.BringToFront();
        }

        public void ShowGamePanel(bool isSpectator)
        {
            panelGame.BringToFront();
            if (isSpectator)
            {
               gameForm.setAsSpectator();
            }
            else
            {
                gameForm.joinGame();
            }
        }
        public void ShowWaitingForOtherPlayersPanel()
        {
            panelWaiting.BringToFront();
        }
    }
}
