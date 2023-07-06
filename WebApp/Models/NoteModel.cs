using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class NoteModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public static IEnumerable<NoteModel> GetAll()
        {
            return Logic.NoteLogic.GetAll().Select(note => new NoteModel 
            { 
                Id = note.Id,
                Text = note.Text, 
                CreationDate = note.CreationDate, 
            });
        }
    }
}