using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Api_Messages;


namespace Client
{
    public partial class CreateRoomDialog : Form
    {
        public CreateRoomDialog()
        {
            InitializeComponent();
            
        }
        private void comboBoxRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle the selected index change event here
            string selectedRoom = comboBoxRooms.SelectedItem.ToString();
            //MessageBox.Show($"Selected Room: {selectedRoom}");
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            string roomId = textBox_RoomName.Text;
            GameCategory category = (GameCategory)comboBoxRooms.SelectedIndex;
            createRoomRequestPayload payload = new createRoomRequestPayload() {roomId=roomId,category=category };
            Request request = new Request() { Type=RequestType.create,payload=payload};
            ClientPlayer.SendRequest(request);
            
            this.Close();
        }
    }
}
