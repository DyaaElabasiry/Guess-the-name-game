
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
            label_RoomName = new Label();
            label_Category = new Label();
            SuspendLayout();
            // 
            // comboBoxRooms
            // 
            comboBoxRooms.FormattingEnabled = true;
            comboBoxRooms.Items.AddRange(new object[] { "Animal", "Food", "Country" });
            comboBoxRooms.Location = new Point(218, 103);
            comboBoxRooms.Name = "comboBoxRooms";
            comboBoxRooms.Size = new Size(200, 23);
            comboBoxRooms.TabIndex = 0;
            comboBoxRooms.SelectedIndexChanged += comboBoxRooms_SelectedIndexChanged;
            comboBoxRooms.SelectedIndex = 0;
            // 
            // textBox_RoomName
            // 
            textBox_RoomName.Location = new Point(218, 54);
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
            // label_RoomName
            // 
            label_RoomName.AutoSize = true;
            label_RoomName.Font = new Font("Segoe UI", 12F);
            label_RoomName.Location = new Point(55, 52);
            label_RoomName.Name = "label_RoomName";
            label_RoomName.Size = new Size(98, 21);
            label_RoomName.TabIndex = 3;
            label_RoomName.Text = "Room Name";
            // 
            // label_Category
            // 
            label_Category.AutoSize = true;
            label_Category.Font = new Font("Segoe UI", 12F);
            label_Category.Location = new Point(55, 101);
            label_Category.Name = "label_Category";
            label_Category.Size = new Size(73, 21);
            label_Category.TabIndex = 4;
            label_Category.Text = "Category";
            // 
            // CreateRoomDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 220);
            Controls.Add(label_Category);
            Controls.Add(label_RoomName);
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
        private Label label_RoomName;
        private Label label_Category;
    }
}