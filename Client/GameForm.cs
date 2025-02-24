using System.Text.Json;
using Api_Messages;

namespace Client
{
    public partial class GameForm : UserControl
    {
        private string word;
        private char[] displayWord;
        private bool myTurn = true;
        private bool isSpectating = false;
        private Label wordLabel;
        private Label turn;
        OnScreenKeyboard keyboard;
        private MainForm mainForm;


        public GameForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            keyboard = new OnScreenKeyboard();
            keyboard.KeyPressed += Keyboard_KeyPressed;
            keyboard.Dock = DockStyle.Bottom;
            this.Controls.Add(keyboard);

            

            wordLabel = new Label
            {
                
                Font = new Font("Segoe UI", 24),
                AutoSize = true,
                Anchor = AnchorStyles.None
            };

            wordLabel.Location = new Point((this.Width - wordLabel.Width) / 2, (this.Height - wordLabel.Height) / 2);
            turn = new Label
            {
                Text = "Your Turn",
                Font = new Font("Segoe UI", 24),
                AutoSize = true,
                Location = new Point(10, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            this.Controls.Add(wordLabel);
            this.Controls.Add(turn);
            this.mainForm = mainForm;
            this.Resize += GameForm_Resize;

        }
        private void GameForm_Resize(object sender, EventArgs e)
        {
            // Center the wordLabel when the form is resized
            wordLabel.Location = new Point((this.Width - wordLabel.Width) / 2, (this.Height - wordLabel.Height) / 2);
        }
        public void HandleYourTurn(Response response)
        {
            if (!isSpectating)
            {
                turn.Text = "Your Turn";
                myTurn = true;
            }

            yourTurnResponsePayload payload = JsonSerializer.Deserialize<yourTurnResponsePayload>(response.payload.ToString());
            char pressedKey = payload.key;
            /*if (isSpectating)
            {
                MessageBox.Show($"pressed key {pressedKey}");
            }*/
            updateWord(pressedKey);
            disableKeyboardKey(pressedKey);
        }
        public void joinGame()
        {
            word = ClientPlayer.roomInfo.word;
            displayWord = new string('_', word.Length).ToCharArray();
            wordLabel.Text = FormatDisplayWord(displayWord);
        }
        public void spectateGame(Response response)

        { 
            spectateRoomResponsePayload payload = JsonSerializer.Deserialize<spectateRoomResponsePayload>(response.payload.ToString());
            RoomInfo roomInfo = payload.roomInfo;
            word = roomInfo.word;
            displayWord = roomInfo.guessedChars.ToCharArray();
            wordLabel.Text = FormatDisplayWord(displayWord);
            /*MessageBox.Show("Spectating " + roomInfo.player1Name + " vs " + roomInfo.player2Name);*/

        }
        public void HandleGameOver()
        {
            turn.Text = "Game Over";
            turn.ForeColor = Color.Red;
            myTurn = false;
        }

        private void Keyboard_KeyPressed(object sender, KeyEventArgs e)
        {
            if (myTurn)
            {
                Button button = sender as Button;
                char pressedKey = (char)e.KeyCode;
                myTurn = false;
                if (!isSpectating) { turn.Text = "Opponent's Turn"; }
                
                updateWord(pressedKey);
                sendGuess(pressedKey);
                disableKeyboardKey(pressedKey);

                if (string.Join("", displayWord) == word)
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
                    key = guess,
                    guessedChars = string.Join("", displayWord)
                }
            };
            ClientPlayer.SendRequest(request);
        }

        private void updateWord(char pressedKey)
        {
            // updates the displayed word
            for (int i = 0; i < word.Length; i++)
            {
                if (char.ToUpper(word[i]) == pressedKey || char.ToLower(word[i]) == pressedKey)
                {
                    displayWord[i] = word[i];
                }
            }
            wordLabel.Text = FormatDisplayWord(displayWord);

        }
        private void disableKeyboardKey(char key)
        {
            // Disable the button after updating the word
            foreach (Button button in keyboard.Controls.OfType<Button>())
            {
                if (button.Text.Equals(key.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    button.Enabled = false;
                    break;
                }
            }
        }
        public void disableAllKeboardKeys()
        {
            foreach (Button button in keyboard.Controls.OfType<Button>())
            {
                button.Enabled = false;
            }
        }
        public void setAsSpectator()
        {

            turn.Text = "Spectating";
            myTurn = false;
            isSpectating = true;
            disableAllKeboardKeys();
        }
    }
}
