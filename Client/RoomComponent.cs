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
    public partial class RoomComponent : UserControl
    {
        
        public RoomInfo roomInfo;
        private RoomsForm roomsForm;
        public RoomComponent(RoomInfo room,RoomsForm roomsForm)
        {
            InitializeComponent();
            this.roomsForm = roomsForm;
            roomInfo = room;
            if(roomInfo.numberOfPlayers == 2)
            {
                buttonJoin.Enabled = false;
                
            }
            if(roomInfo.isReady)
            {
                buttonSpectate.Enabled = true;
            }


            labelRoomName.Text = roomInfo.roomId;
            label_RoomCategory.Text = roomInfo.category.ToString();
            label_NumberOfPlayers.Text = $"players : {roomInfo.numberOfPlayers.ToString()}";

        }

        private void buttonSpectate_Click(object sender, EventArgs e)
        {
            spectateRequestPayload payload = new spectateRequestPayload() { roomId = roomInfo.roomId };
            Request request = new Request() { Type = RequestType.spectate,payload=payload};
            ClientPlayer.SendRequest(request);
            roomsForm.SpectateRoom(roomInfo);
        }

        private void buttonJoin_Click(object sender, EventArgs e)
        {
            // sending join request to server
            joinRequestPayload payload = new joinRequestPayload() { roomId = roomInfo.roomId };
            Request request = new Request() { Type = RequestType.join, payload = payload };
            ClientPlayer.SendRequest(request);

            // opening game form

            roomsForm.JoinRoom(roomInfo);


        }
    }
}
