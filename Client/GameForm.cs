using System;
using System.Net.Sockets;
using System.Windows.Forms;
using Api_Messages;

namespace Client
{
    public partial class GameForm : Form
    {
        private string word;
        private char[] displayWord;
        private Label wordLabel;
        private bool myTurn = true;
        private Label turn;

        public GameForm()
        {
            InitializeComponent();
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            ClientPlayer.client = client;
            OnScreenKeyboard keyboard = new OnScreenKeyboard();
            keyboard.KeyPressed += Keyboard_KeyPressed;
            keyboard.Dock = DockStyle.Bottom;
            this.Controls.Add(keyboard);

            word = ClientPlayer.word;
            displayWord = new string('_', word.Length).ToCharArray();

            wordLabel = new Label
            {
                Text = FormatDisplayWord(displayWord),
                Font = new Font("Segoe UI", 24),
                AutoSize = true,
                
            };
           
            wordLabel.Location = new Point((this.Width - wordLabel.Width) / 2, (this.Height - wordLabel.Height) / 2);
            turn = new Label
            {
                Text = "Your Turn",
                Font = new Font("Segoe UI", 24),
                AutoSize = true,
                Location = new Point(10, 10),
            };
            
            this.Controls.Add(wordLabel);
            this.Controls.Add(turn);

            Task.Run(() => { HandleResponses(); });
        }

        private async Task HandleResponses()
        {
            while (true)
            {
                Response response = await ClientPlayer.GetResponse();
                if (response.Type == ResponseType.yourTurn)
                {
                    turn.Text = "Your Turn";
                    myTurn = true;
                }
                else if(response.Type == ResponseType.gameOver)
                {
                    turn.Text = "Game Over";
                    turn.ForeColor = Color.Red;
                    myTurn = false;
                }
            }
        }

        private void Keyboard_KeyPressed(object sender, KeyEventArgs e)
        {
            // Handle the key press event
            if (myTurn)
            {
                Button button = sender as Button;
                char pressedKey = (char)e.KeyCode;
                sendGuess(pressedKey);
                myTurn = false;
                for (int i = 0; i < word.Length; i++)
                {
                    if (char.ToUpper(word[i]) == pressedKey || char.ToLower(word[i]) == pressedKey)
                    {
                        displayWord[i] = word[i];
                    }
                }
                wordLabel.Text = FormatDisplayWord(displayWord);
                button.Enabled = false;
                turn.Text  = "Opponent's Turn";
                if (string.Join("",displayWord) == word)
                {
                    turn.Text = "You Win!";
                    turn.ForeColor = Color.Green;
                    myTurn = false;
                    ClientPlayer.SendRequest(new Request
                    {
                        Type = RequestType.gameOver
                    });
                }
            }
        }

        private string FormatDisplayWord(char[] displayWord)
        {
            return string.Join(" ", displayWord);
        }
        private void sendGuess(char guess)
        {
            Request request = new Request
            {
                Type = RequestType.pressedKey,
                payload = new pressedKeyRequestPayload
                {
                    key = guess
                }
            };
            ClientPlayer.SendRequest(request);
        }
    }
}

