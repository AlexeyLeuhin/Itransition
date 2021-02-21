using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using task5.Context;
using task5.Models;

namespace task5.SignalR
{
    public class DrawingHub: Hub
    {
        private readonly DBContextManager dbManager;
        public DrawingHub(ElementsContext db)
        {
            dbManager = new DBContextManager(db);
        }
        
        public async Task AddLine(string json)
        {
            Line line = new Line();
            line.Data = json;
            await dbManager.AddLine(line);
            await this.Clients.Others.SendAsync("AddLine", json);
        }

        public async Task DeleteLine(string json)
        {
            await dbManager.DeleteLine(json);
            await this.Clients.Others.SendAsync("DeleteLine", json);
        }

        public async Task UpdateLine(string oldJson, string newJson)
        {
            await dbManager.UpdateLine(oldJson, newJson);
            await this.Clients.Others.SendAsync("UpdateLine", oldJson, newJson);
        }
    }
}
