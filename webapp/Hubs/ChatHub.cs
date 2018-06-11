using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SmartAdminMvc.Hubs
{
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        public static List<Client> ConnectedUsers { get; set; } = new List<Client>();
        public void Connect(string id, string username, string name)
        {
            Client c = new Client()
            {
                Name = name,
                Username = username,
                UserID = id,
                ID = Context.ConnectionId
            };
            ConnectedUsers.Add(c);
            Clients.All.updateUsers(ConnectedUsers.Count(), ConnectedUsers.Select(u => new { u.UserID, u.Username, u.Name, u.ID }));
        }
        public void SendV2(string chatID, string userReceive, string message)
        {
            var sender = ConnectedUsers.First(u => u.ID.Equals(Context.ConnectionId));
            Clients.All.broadcastMessage(chatID, sender.Name, userReceive, message);
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var disconnectedUser = ConnectedUsers.FirstOrDefault(c => c.ID.Equals(Context.ConnectionId));
            ConnectedUsers.Remove(disconnectedUser);
            Clients.All.updateUsers(ConnectedUsers.Count(), ConnectedUsers.Select(u => new { u.Username, u.Name }));
            return base.OnDisconnected(stopCalled);
        }
    }
    public class Client
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public string ID { get; set; }
    }
}