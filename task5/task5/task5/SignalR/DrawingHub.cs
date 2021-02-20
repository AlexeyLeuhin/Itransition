using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using task5.Context;
using task5.Controllers;
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
        
        public async Task AddPath(List<Point> points)
        {
            Path path = new Path(points);
            await dbManager.AddPath(path);
            await this.Clients.Others.SendAsync("AddPath",path);
        }


        public async Task DragPath(Point delta, Point pathPoint)
        {
            var points = dbManager.DragPath(delta, pathPoint);
            await this.Clients.Others.SendAsync("DragPath", points, pathPoint);
        }

        public async Task DeletePath(Point pathPoint)
        {
            await dbManager.DeletePath(pathPoint);
            await this.Clients.Others.SendAsync("DeletePath", pathPoint);
        }
    }
}
