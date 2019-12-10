using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MasterProject.DBContext;

namespace MasterProject.Models
{
    public class Menu
    {
        public short MENU_ID { get; set; }
        public string MENU_TEXT { get; set; }
        public string TOOL_TIP { get; set; }
        public short ORDER_NO { get; set; }
        public string REF_URL { get; set; }
        public Nullable<short> PARENT_ID { get; set; }
        public bool HAS_CHILD { get; set; }
        public string MENU_TYPE_NAME { get; set; }
        public string ICON_IMAGES_LINK { get; set; }
        public short LEVEL { get; set; }
        public Menu_Type Menu_Type { get; set; }
        public List<Menu> SubMenu { get; set; }

       
    }
}