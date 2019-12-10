using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterProject.Models
{
    public class Menu_Type
    {
        public byte MENU_TYPE_ID { get; set; }
        public string MENU_TYPE_NAME { get; set; }
        public Nullable<bool> ISVISIBLE { get; set; }
    }
}