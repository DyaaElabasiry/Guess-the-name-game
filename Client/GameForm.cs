using System;
using System.Windows.Forms;
using Api_Messages;

namespace Client
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
            OnScreenKeyboard keyboard = new OnScreenKeyboard();
            keyboard.KeyPressed += Keyboard_KeyPressed;
            keyboard.Dock = DockStyle.Bottom;
            this.Controls.Add(keyboard);
            Task.Run(() => { HandleResponses(); });
        }
        private async Task HandleResponses()
        {
            while (true)
            {
                Response response = await ClientPlayer.GetResponse();
                if () 
                {
                    
                }
            }
        }
        private void Keyboard_KeyPressed(object sender, KeyEventArgs e)
        {
            // Handle the key press event
            // MessageBox.Show($"Key pressed: {e.KeyCode}");
            
        }
    }
}
