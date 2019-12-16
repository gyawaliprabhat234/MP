using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterProject.DBContext;
using MasterProject.Models;

namespace MasterProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Menu> Menu = null;
           using (MASTERPROJECTEntities db = new MASTERPROJECTEntities())
            {
              Menu = (from m in db.SEC_MENU
                                   join
                                   t in db.MENU_TYPE
                                   on m.MENU_TYPE_ID equals t.MENU_TYPE_ID
                                   select new Menu()
                                   {
                                       MENU_ID = m.MENU_ID, 
                                       MENU_TEXT = m.MENU_TEXT, 
                                       MENU_TYPE_NAME = t.MENU_TYPE_NAME, 
                                       HAS_CHILD = m.HAS_CHILD, 
                                       ICON_IMAGES_LINK = m .ICON_IMAGES_LINK , 
                                       REF_URL = m .REF_URL , 
                                       PARENT_ID = m.PARENT_ID , 
                                       TOOL_TIP = m.TOOL_TIP , 
                                       ORDER_NO = m.ORDER_NO 

                                   }).ToList();
            }

 /*

            using (MASTERPROJECTEntities db = new MASTERPROJECTEntities())
            {
                List<Menu> Menus = new List<Menu>();
                string query = @"WITH MENU_CTE (MENU_ID, PARENT_ID, [LEVEL])
                                 AS (
                                 SELECT MENU_ID , PARENT_ID, 1 FROM SEC_MENU WHERE PARENT_ID IS NULL
                                 UNION ALL 
                                 SELECT M.MENU_ID, M.PARENT_ID, C.[LEVEL]+1 FROM SEC_MENU M INNER JOIN 
                                 MENU_CTE C ON M.PARENT_ID = C.MENU_ID
                                 )
                                 SELECT  M.PARENT_ID,
                                   M.MENU_TEXT,
                                   M.MENU_ID,
                                   M.HAS_CHILD,
                                   M.ORDER_NO,
                                   M.ICON_IMAGES_LINK , 
                                   M.REF_URL,
                                   M.TOOL_TIP,
                                   T.MENU_TYPE_NAME,
                                   C.[LEVEL]
                                   FROM SEC_MENU M 
                                    JOIN
                                   MENU_CTE C ON M.MENU_ID = C.MENU_ID
                                   Join 
                                   MENU_TYPE T ON T.MENU_TYPE_ID = M.MENU_TYPE_ID
                                   WHERE T.ISVISIBLE = 1
                                   ORDER BY C.LEVEL, M.ORDER_NO



                                 ";

                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = System.Data.CommandType.Text;
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Menu item = new Menu();

                        item.PARENT_ID = string.IsNullOrEmpty(reader["PARENT_ID"].ToString()) ? (short)0 : (short)reader["PARENT_ID"];
                        item.MENU_ID = (short)reader["MENU_ID"];
                        item.MENU_TEXT = reader["MENU_TEXT"] as string;
                        item.HAS_CHILD = (bool)reader["HAS_CHILD"];
                        item.ORDER_NO = (short)reader["ORDER_NO"];
                        item.ICON_IMAGES_LINK = reader["ICON_IMAGES_LINK"] as string;
                        item.REF_URL = reader["REF_URL"] as string;
                        item.TOOL_TIP = reader["TOOL_TIP"] as string;
                        item.MENU_TYPE_NAME = reader["MENU_TYPE_NAME"] as string;
                        item.LEVEL = Convert.ToInt16(reader["LEVEL"]);
                        Menus.Add(item);
                    }

                }



                using (SqlConnection con = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Menu item = new Menu();

                            item.PARENT_ID = string.IsNullOrEmpty(reader["PARENT_ID"].ToString()) ? (short)0 : (short)reader["PARENT_ID"];
                            item.MENU_ID = (short)reader["MENU_ID"];
                            item.MENU_TEXT = reader["MENU_TEXT"] as string;
                            item.HAS_CHILD = (bool)reader["HAS_CHILD"];
                            item.ORDER_NO = (short)reader["ORDER_NO"];
                            item.ICON_IMAGES_LINK = reader["ICON_IMAGES_LINK"] as string;
                            item.REF_URL = reader["REF_URL"] as string;
                            item.TOOL_TIP = reader["TOOL_TIP"] as string;
                            item.MENU_TYPE_NAME = reader["MENU_TYPE_NAME"] as string;
                            item.LEVEL = Convert.ToInt16(reader["LEVEL"]);

                            Menus.Add(item);
                        }
                    }
                }
                return departmentList;
            }
            */
            return View(Menu);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

  
}
