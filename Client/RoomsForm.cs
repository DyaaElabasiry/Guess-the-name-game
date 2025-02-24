using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;
using Api_Messages;

namespace Client
{
    public partial class RoomsForm : UserControl
    {
        public MainForm mainForm;

        public RoomsForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            Request request = new Request() { Type = RequestType.getRooms };
            ClientPlayer.SendRequest(request);
        }

        public void DisplayRooms(object payload)
        {
            getRoomsResponsePayload roomsPayload = JsonSerializer.Deserialize<getRoomsResponsePayload>(payload.ToString());
            List<RoomInfo> rooms = roomsPayload.rooms;

            flowLayoutPanelRooms.Controls.Clear();
            foreach (var room in rooms)
            {
                var roomComponent = new RoomComponent(room, this);
                flowLayoutPanelRooms.Controls.Add(roomComponent);
            }
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            CreateRoomDialog createRoomDialog = new CreateRoomDialog();
            createRoomDialog.ShowDialog();
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            Request request = new Request() { Type = RequestType.getRooms };
            ClientPlayer.SendRequest(request);
        }

        public void JoinRoom(RoomInfo room)
        {
            ClientPlayer.roomInfo = room;
            //mainForm.ShowGamePanel(false);
            mainForm.ShowWaitingForOtherPlayersPanel();
        }
        public void SpectateRoom(RoomInfo room)
        {
            ClientPlayer.roomInfo = room;
            mainForm.ShowGamePanel(true);
        }
    }
}
