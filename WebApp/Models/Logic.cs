using Denis.UserList.BLL.Core;
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
        public static UserLogic UserLogic { get; private set; }
        public static AwardLogic AwardLogic { get; private set; }

        static Logic()
        {
            NoteLogic = new NoteLogic();
            AwardLogic = new AwardLogic();
            UserLogic = new UserLogic(AwardLogic);
        }
    }
}