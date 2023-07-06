using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Common.Entities;

namespace WebApp.BLL.Core
{
    public class NoteLogic
    {
        private List<Note> notes;

        public NoteLogic()
        {
            notes = new List<Note>
            {
                new Note{ Id = Guid.NewGuid(), CreationDate = DateTime.Now, Text = "note1" },
                new Note{ Id = Guid.NewGuid(), CreationDate = DateTime.Now, Text = "note1" },
                new Note{ Id = Guid.NewGuid(), CreationDate = DateTime.Now, Text = "note1" },
                new Note{ Id = Guid.NewGuid(), CreationDate = DateTime.Now, Text = "note1" },
                new Note{ Id = Guid.NewGuid(), CreationDate = DateTime.Now, Text = "note1" },
                new Note{ Id = Guid.NewGuid(), CreationDate = DateTime.Now, Text = "note1" },
                new Note{ Id = Guid.NewGuid(), CreationDate = DateTime.Now, Text = "note1" },
            };
        }

        public IEnumerable<Note> GetAll()
        {
            foreach (var note in notes)
            {
                yield return note;
            }
        }
    }
}
