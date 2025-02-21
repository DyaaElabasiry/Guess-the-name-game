using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api_Messages;

namespace Client
{
    public partial class RoomComponent: UserControl
    {
        public string RoomName { get; set; }
        public string Category;
        public RoomComponent(string roomName, string category)
        {
            InitializeComponent();
            RoomName = roomName;
            Category = category;
            labelRoomName.Text = roomName;
            label_RoomCategory.Text = category;

        }

        private void buttonSpectate_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonJoin_Click(object sender, EventArgs e)
        {
            joinRequestPayload payload = new joinRequestPayload() { roomId = RoomName };
            Request request = new Request() { Type = RequestType.join, payload = payload };
            ClientPlayer.SendRequest(request);
        }
    }
}
