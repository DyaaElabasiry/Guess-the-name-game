using System;
using System.Windows.Forms;

namespace Client
{
    public partial class OnScreenKeyboard : UserControl
    {
        public event EventHandler<KeyEventArgs> KeyPressed;

        public OnScreenKeyboard()
        {
            InitializeComponent();
            CreateKeyboard();
        }

        private void CreateKeyboard()
        {
            string[] keys = new string[]
            {
                
                "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P",
                "A", "S", "D", "F", "G", "H", "J", "K", "L",
                "Z", "X", "C", "V", "B", "N", "M"
            };

            int x = 10, y = 10;
            foreach (var key in keys)
            {
                Button button = new Button();
                button.Text = key;
                button.Width = 50;
                button.Height = 50;
                button.Left = x;
                button.Top = y;
                button.Click += Button_Click;
                this.Controls.Add(button);

                x += button.Width + 5;
                if (x > this.Width - 60)
                {
                    x = 10;
                    y += button.Height + 5;
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string key = button.Text;
                Keys keyCode = (Keys)Enum.Parse(typeof(Keys), key, true); 
                KeyPressed?.Invoke(button, new KeyEventArgs(keyCode));
            }
            //button.Enabled = false;
        }
    }
}
