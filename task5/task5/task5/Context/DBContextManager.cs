using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task5.Models;

namespace task5.Context
{
    public class DBContextManager
    {
        private readonly ElementsContext _db;
        public DBContextManager(ElementsContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> UpdateTextArea(TextArea model)
        {
            if (_db != null)
            {
                _db.TextAreas.Update(model);
                await _db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IActionResult> AddTextArea(TextArea model)
        {
            await _db.AddAsync(model);
            await _db.SaveChangesAsync();
            return null;
        }

        public async Task<IActionResult> DeleteTextArea(long id)
        {
            TextArea model = await _db.TextAreas.FindAsync(id);
            if(model != null)
            {
                _db.Remove(model);
            }
            await _db.SaveChangesAsync();
            return null;
        }

        public async Task<IActionResult> AddPath(Path path)
        {
            //List<Point> tmp = path.Segments;
            //path.Segments.Clear();
            await _db.AddAsync(path);
            await _db.SaveChangesAsync();
            //foreach (var point in tmp)
            //{              
            //    await _db.AddAsync(point);
            //    await _db.SaveChangesAsync();
            //}          

            return null;
        }

        public async Task<List<Point>> DragPath(Point delta, Point pathPoint)
        {
            Point p = _db.Points.First<Point>(point => point.X == pathPoint.X && point.Y == pathPoint.Y);     
            List<Point> pointsToDrag = _db.Points.Where<Point>(point => point.PathId == p.PathId).ToList();
            foreach(var point in pointsToDrag)
            {
                point.X -= delta.X;
                point.Y -= delta.Y;
                _db.Update(point);
                _db.SaveChanges();
            }
            return pointsToDrag;
        }

        public async Task<List<Point>> DeletePath(Point pathPoint)
        {
            Point p = _db.Points.First<Point>(point => point.X == pathPoint.X && point.Y == pathPoint.Y);
            Path path = _db.Pathes.Find(p.PathId);
            List<Point> points = _db.Points.Where<Point>(point => point.PathId == p.PathId).ToList();
            foreach(Point point in points)
            {
                _db.Remove(point);
            }
            _db.Remove(path);
            _db.SaveChanges();
            return null;
        }

        public async Task<IActionResult> AddText(Text text)
        {
            await _db.AddAsync(text);
            await _db.SaveChangesAsync();
            return null;
        }

        public async Task<IActionResult> DeleteText(string json)
        {
            Text model = _db.Textes.First<Text>(text => text.Data == json);
            _db.Remove(model);
            await _db.SaveChangesAsync();
            return null;
        }

        public async Task<IActionResult> UpdateText(String oldJson, String newJson)
        {
            Text model = _db.Textes.First<Text>(text => text.Data == oldJson);
            model.Data = newJson;
            _db.Update(model);
            await _db.SaveChangesAsync();
            return null;
        }
    }
}
