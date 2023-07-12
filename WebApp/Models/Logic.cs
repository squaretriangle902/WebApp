using Denis.UserList.BLL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Logic
    {
        public static UserLogic UserLogic { get; private set; }
        public static AwardLogic AwardLogic { get; private set; }

        static Logic()
        {
            AwardLogic = new AwardLogic();
            UserLogic = new UserLogic(AwardLogic);
        }
    }
}