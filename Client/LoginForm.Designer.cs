namespace Client
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label_enter_name = new Label();
            button_login = new Button();
            textBox_enter_name = new TextBox();
            SuspendLayout();
            // 
            // label_enter_name
            // 
            label_enter_name.AutoSize = true;
            label_enter_name.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label_enter_name.Location = new Point(178, 135);
            label_enter_name.Name = "label_enter_name";
            label_enter_name.Size = new Size(160, 25);
            label_enter_name.TabIndex = 0;
            label_enter_name.Text = "Enter your name";
            // 
            // button_login
            // 
            button_login.Location = new Point(415, 203);
            button_login.Name = "button_login";
            button_login.Size = new Size(85, 30);
            button_login.TabIndex = 1;
            button_login.Text = "Login";
            button_login.UseVisualStyleBackColor = true;
            button_login.Click += button_login_Click;
            // 
            // textBox_enter_name
            // 
            textBox_enter_name.Font = new Font("Segoe UI", 12F);
            textBox_enter_name.Location = new Point(178, 203);
            textBox_enter_name.Name = "textBox_enter_name";
            textBox_enter_name.Size = new Size(207, 29);
            textBox_enter_name.TabIndex = 2;
            // 
            // LoginForm
            // 
            //AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox_enter_name);
            Controls.Add(button_login);
            Controls.Add(label_enter_name);
            Name = "LoginForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_enter_name;
        private Button button_login;
        private TextBox textBox_enter_name;
    }
}
