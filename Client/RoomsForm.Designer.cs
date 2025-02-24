namespace Client
{
    partial class RoomsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_create = new Button();
            flowLayoutPanelRooms = new FlowLayoutPanel();
            button_Refresh = new Button();
            SuspendLayout();
            // 
            // button_create
            // 
            button_create.Font = new Font("Segoe UI", 12F);
            button_create.Name = "button_create";
            button_create.Size = new Size(110, 40);
            button_create.Location = new Point(this.Width-button_create.Width, this.Height-button_create.Height);
            button_create.TabIndex = 0;
            button_create.Text = "Create";
            button_create.UseVisualStyleBackColor = true;
            button_create.Click += button_create_Click;
            // 
            // flowLayoutPanelRooms
            // 
            flowLayoutPanelRooms.AutoScroll = true;
            flowLayoutPanelRooms.Location = new Point(10, 10);
            flowLayoutPanelRooms.Name = "flowLayoutPanelRooms";
            flowLayoutPanelRooms.Size = new Size(666, 380);
            flowLayoutPanelRooms.TabIndex = 0;
            // 
            // button_Refresh
            // 
            button_Refresh.Font = new Font("Segoe UI", 12F);
            button_Refresh.Location = new Point(682, 12);
            button_Refresh.Name = "button_Refresh";
            button_Refresh.Size = new Size(110, 40);
            button_Refresh.TabIndex = 1;
            button_Refresh.Text = "Refresh";
            button_Refresh.UseVisualStyleBackColor = true;
            button_Refresh.Click += button_Refresh_Click;
            // 
            // RoomsForm
            // 
            //AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            //Controls.Add(button_Refresh);
            Controls.Add(button_create);
            Controls.Add(flowLayoutPanelRooms);
            Name = "RoomsForm";
            Text = "Rooms";
            Load+= (sender, e) =>
            {
                button_create.Location = new Point(this.Width - button_create.Width, this.Height - button_create.Height);
            };
            ResumeLayout(false);
        }

        #endregion

        private Button button_create;
        private FlowLayoutPanel flowLayoutPanelRooms;
        private Button button_Refresh;
    }
}