
namespace Client
{
    partial class CreateRoomDialog
    {
        private System.ComponentModel.IContainer components = null;


        private ComboBox comboBoxRooms;
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
            comboBoxRooms = new ComboBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            textBox_RoomName = new TextBox();
            button_Ok = new Button();
            SuspendLayout();
            // 
            // comboBoxRooms
            // 
            comboBoxRooms.FormattingEnabled = true;
            comboBoxRooms.Items.AddRange(new object[] { "Animal", "Food", "Country" });
            comboBoxRooms.Location = new Point(45, 94);
            comboBoxRooms.Name = "comboBoxRooms";
            comboBoxRooms.Size = new Size(200, 23);
            comboBoxRooms.TabIndex = 0;
            comboBoxRooms.SelectedIndexChanged += comboBoxRooms_SelectedIndexChanged;
            // 
            // textBox_RoomName
            // 
            textBox_RoomName.Location = new Point(45, 52);
            textBox_RoomName.Name = "textBox_RoomName";
            textBox_RoomName.Size = new Size(200, 23);
            textBox_RoomName.TabIndex = 1;
            // 
            // button_Ok
            // 
            button_Ok.Font = new Font("Segoe UI", 9F);
            button_Ok.Location = new Point(195, 181);
            button_Ok.Name = "button_Ok";
            button_Ok.Size = new Size(81, 27);
            button_Ok.TabIndex = 2;
            button_Ok.Text = "OK";
            button_Ok.UseVisualStyleBackColor = true;
            button_Ok.Click += button_Ok_Click;
            // 
            // CreateRoomDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 220);
            Controls.Add(button_Ok);
            Controls.Add(textBox_RoomName);
            Controls.Add(comboBoxRooms);
            Name = "CreateRoomDialog";
            Text = "Create Room";
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox textBox_RoomName;
        private Button button_Ok;
    }
}