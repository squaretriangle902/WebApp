using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.BLL.Core;
using WebApp.Common.Entities;

namespace WebApp.Models
{
    public class NoteModel
    {
        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public static IEnumerable<NoteModel> GetAll()
        {
            return Logic.NoteLogic.GetAll().Select(note => new NoteModel 
            { 
                Text = note.Text, 
                CreationDate = note.CreationDate, 
            });
        }

        public static void Add(NoteModel noteModel)
        {
            var note = new Note { Text = noteModel.Text };
            Logic.NoteLogic.Add(note);
        }
    }
}