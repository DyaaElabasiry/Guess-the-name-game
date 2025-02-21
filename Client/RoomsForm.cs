using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Api_Messages;

namespace Client
{
    public partial class RoomsForm : Form
    {
        
        List<string> roomIds = new List<string>();
        List<GameCategory> categories = new List<GameCategory>();
        
        public RoomsForm()
        {
            InitializeComponent();


            Task.Run(() => { HandleResponses(); });
            Request request = new Request() { Type = RequestType.getRooms };
            ClientPlayer.SendRequest(request);

            //DisplayRooms(rooms);

        }
        private async Task HandleResponses()
        {
            byte[] buffer = new byte[2024];
            while (true)
            {

                int bytesRead =  ClientPlayer.client.GetStream().Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Response response = JsonSerializer.Deserialize<Response>(message);
                if (response.Type == ResponseType.getRooms)
                {
                    getRoomsResponsePayload payload = JsonSerializer.Deserialize<getRoomsResponsePayload>(response.payload.ToString());
                    roomIds = payload.roomIds;
                    categories = payload.categories;

                    DisplayRooms(roomIds, categories);

                }

            }
        }
        private void DisplayRooms(List<string> roomsIds, List<GameCategory> categories)
        {

            this.Invoke(() => {flowLayoutPanelRooms.Controls.Clear();});
            for (int i = 0; i < roomsIds.Count; i++)
            {
                var roomComponent = new RoomComponent(roomIds[i], categories[i].ToString());
                this.Invoke(() => { flowLayoutPanelRooms.Controls.Add(roomComponent); });
            }


        }

        private void button_create_Click(object sender, EventArgs e)
        {
            CreateRoomDialog createRoomDialog = new CreateRoomDialog();
            createRoomDialog.ShowDialog();
        }

        private void flowLayoutPanelRooms_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            
            Request request = new Request() { Type = RequestType.getRooms };
            ClientPlayer.SendRequest(request);
            
            
        }
        
    }
}
