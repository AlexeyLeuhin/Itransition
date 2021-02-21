using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using task5.Context;
using task5.Controllers;
using task5.Models;

namespace task5.SignalR
{
    public class ElementsHub: Hub
    {
        private readonly DBContextManager dbManager;
        public ElementsHub(ElementsContext db)
        {
            dbManager = new DBContextManager(db);
        }
        
        public async Task UpdateTextArea(TextArea textArea)
        {
            await dbManager.UpdateTextArea(textArea);     
            await this.Clients.Others.SendAsync("UpdateTextArea", textArea);
        }

        public async Task AddTextArea(TextArea textArea)
        {
            await dbManager.AddTextArea(textArea);
            await this.Clients.All.SendAsync("AddTextArea", textArea);
        }

        public async Task DeleteTextArea(long id)
        {
            await dbManager.DeleteTextArea(id);
            await this.Clients.Others.SendAsync("DeleteTextArea", id);
        }
    }
}
