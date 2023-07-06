using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.BLL.Core;

namespace WebApp.Models
{
    public class Logic
    {
        public static NoteLogic NoteLogic { get; private set; }

        static Logic()
        {
            NoteLogic = new NoteLogic();
        }
    }
}