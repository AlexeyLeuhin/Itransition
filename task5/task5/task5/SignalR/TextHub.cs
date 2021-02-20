using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using task5.Context;
using task5.Models;

namespace task5.SignalR
{
    public class TextHub: Hub
    {
        private readonly DBContextManager dbManager;
        public TextHub(ElementsContext db)
        {
            dbManager = new DBContextManager(db);
        }

        public async Task AddText(String json)
        {
            Text text = new Text();
            text.Data = json;
            await dbManager.AddText(text);
            await this.Clients.Others.SendAsync("AddText", json);
        }

        public async Task DeleteText(String json)
        {
            await dbManager.DeleteText(json);
            await this.Clients.Others.SendAsync("DeleteText", json);
        }

        public async Task UpdateText(String oldJson, String  newJson)
        {
            await dbManager.UpdateText(oldJson, newJson);
            await this.Clients.Others.SendAsync("UpdateText", oldJson, newJson);
        }
    }
}
