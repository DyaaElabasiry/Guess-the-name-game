namespace Client
{
    partial class RoomComponent
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelRoomName;
        private Button buttonJoin;
        private Button buttonSpectate;

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
            labelRoomName = new Label();
            buttonJoin = new Button();
            buttonSpectate = new Button();
            label_RoomCategory = new Label();
            SuspendLayout();
            // 
            // labelRoomName
            // 
            labelRoomName.AutoSize = true;
            labelRoomName.Location = new Point(3, 10);
            labelRoomName.Name = "labelRoomName";
            labelRoomName.Size = new Size(74, 15);
            labelRoomName.TabIndex = 0;
            labelRoomName.Text = "Room Name";
            // 
            // buttonJoin
            // 
            buttonJoin.Location = new Point(238, 5);
            buttonJoin.Name = "buttonJoin";
            buttonJoin.Size = new Size(75, 23);
            buttonJoin.TabIndex = 1;
            buttonJoin.Text = "Join";
            buttonJoin.UseVisualStyleBackColor = true;
            buttonJoin.Click += buttonJoin_Click;
            // 
            // buttonSpectate
            // 
            buttonSpectate.Location = new Point(319, 5);
            buttonSpectate.Name = "buttonSpectate";
            buttonSpectate.Size = new Size(75, 23);
            buttonSpectate.TabIndex = 2;
            buttonSpectate.Text = "Spectate";
            buttonSpectate.UseVisualStyleBackColor = true;
            buttonSpectate.Click += buttonSpectate_Click;
            // 
            // label_RoomCategory
            // 
            label_RoomCategory.AutoSize = true;
            label_RoomCategory.Location = new Point(100, 10);
            label_RoomCategory.Name = "label_RoomCategory";
            label_RoomCategory.Size = new Size(90, 15);
            label_RoomCategory.TabIndex = 3;
            label_RoomCategory.Text = "Room Category";
            // 
            // RoomComponent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label_RoomCategory);
            Controls.Add(buttonSpectate);
            Controls.Add(buttonJoin);
            Controls.Add(labelRoomName);
            Name = "RoomComponent";
            Size = new Size(406, 35);
            ResumeLayout(false);
            PerformLayout();
        }
        private Label label_RoomCategory;
    }
}
