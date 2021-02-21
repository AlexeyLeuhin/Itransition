using Microsoft.AspNetCore.Mvc;
using System;
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

        public async Task<IActionResult> AddLine(Line line)
        {
            await _db.AddAsync(line);
            await _db.SaveChangesAsync();
            return null;
        }

        public async Task<IActionResult> DeleteLine(string json)
        {
            Line model = _db.Lines.First<Line>(line => line.Data == json);
            _db.Remove(model);
            await _db.SaveChangesAsync();
            return null;
        }

        public async Task<IActionResult> UpdateLine(String oldJson, String newJson)
        {
            Line model = _db.Lines.First<Line>(line => line.Data == oldJson);
            model.Data = newJson;
            _db.Update(model);
            await _db.SaveChangesAsync();
            return null;
        }
    }
}
