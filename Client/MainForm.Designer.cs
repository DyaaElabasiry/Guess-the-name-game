namespace Client
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelLogin;
        private Panel panelRooms;
        private Panel panelGame;
        private Panel panelWaiting;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelLogin = new Panel();
            this.panelRooms = new Panel();
            this.panelGame = new Panel();
            this.panelWaiting = new Panel();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.Dock = DockStyle.Fill;
            this.panelLogin.Location = new Point(0, 0);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new Size(800, 450);
            this.panelLogin.TabIndex = 0;
            // 
            // panelRooms
            // 
            this.panelRooms.Dock = DockStyle.Fill;
            this.panelRooms.Location = new Point(0, 0);
            this.panelRooms.Name = "panelRooms";
            this.panelRooms.Size = new Size(800, 450);
            this.panelRooms.TabIndex = 1;
            // 
            // panelGame
            // 
            this.panelGame.Dock = DockStyle.Fill;
            this.panelGame.Location = new Point(0, 0);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new Size(800, 450);
            this.panelGame.TabIndex = 2;
            // 
            // panelWaiting
            // 
            this.panelWaiting.Dock = DockStyle.Fill;
            this.panelWaiting.Location = new Point(0, 0);
            this.panelWaiting.Name = "panelWaiting";
            this.panelWaiting.Size = new Size(800, 450);
            this.panelWaiting.TabIndex = 3;
            // 
            // MainForm
            // 
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelRooms);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.panelWaiting);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
        }
    }
}
