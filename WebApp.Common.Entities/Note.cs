using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Common.Entities
{
    public class Note
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
