using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class CUD
    {
        public static void Create(Note note)
        {
            List<Note> list = JsonDS.Deserialize<List<Note>>("Note.json") == null? new List<Note>(): JsonDS.Deserialize<List<Note>>("Note.json");
            list.Add(note);
            JsonDS.Serialize("Note.json", list);
        }
        public static void Update(Note note, string NoteName, string Date)
        {
            List<Note> list = JsonDS.Deserialize<List<Note>>("Note.json");
            foreach(var element in list)
            {
                if (element.Name == NoteName && element.Date == Date)
                {
                    element.Name = note.Name;
                    element.Price = note.Price;
                    element.TypeOf = note.TypeOf;
                    element.Add = note.Add;
                }
            }
            JsonDS.Serialize("Note.json", list);
        }
        public static void Delete(Note note)
        {
            List<Note> list = JsonDS.Deserialize<List<Note>>("Note.json");
            foreach(var element in list)
            {
                if(element.Name == note.Name && element.Date == note.Date)
                {
                    list.Remove(element);
                    break;
                }
            }
            JsonDS.Serialize("Note.json", list);
        }
    }
}
