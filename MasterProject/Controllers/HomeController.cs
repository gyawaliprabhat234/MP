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
            using (MASTERPROJECTEntities db = new MASTERPROJECTEntities())
            {
                List < Menu > Menus = new List<Menu>();
                string query = @"WITH MENU_CTE (MENU_ID, PARENT_ID, [LEVEL])
                                AS (
                                SELECT MENU_ID , PARENT_ID, 1 FROM SEC_MENU WHERE PARENT_ID IS NULL
                                UNION ALL 
                                SELECT M.MENU_ID, M.PARENT_ID, C.[LEVEL]+1 FROM SEC_MENU M INNER JOIN 
                                MENU_CTE C ON M.PARENT_ID = C.MENU_ID

                                )

                                SELECT 
		                                M.PARENT_ID,
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
               
               using(var cmd = db.Database.Connection.CreateCommand())
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
              //  using (SqlConnection con = new SqlConnection(db.Database.Connection.ConnectionString))
              //  {
              //      SqlCommand cmd = new SqlCommand(query, con);
              //      cmd.CommandType = System.Data.CommandType.Text;
              //      con.Open();
              //      using (SqlDataReader reader = cmd.ExecuteReader())
              //      {
              //          while (reader.Read())
              //          {
              //              Menu item = new Menu();

              //              item.PARENT_ID          =  string.IsNullOrEmpty(reader["PARENT_ID"].ToString())? (short)0:(short)reader["PARENT_ID"];
              //              item.MENU_ID            =  (short)reader["MENU_ID"]        ;
              //                item.MENU_TEXT        = reader["MENU_TEXT"]       as string  ;
              //                item.HAS_CHILD        = (bool)reader["HAS_CHILD"]            ; 
              //                item.ORDER_NO         = (short)reader["ORDER_NO"]            ;
              //                item.ICON_IMAGES_LINK = reader["ICON_IMAGES_LINK"]as string  ;
              //                item.REF_URL          = reader["REF_URL"]         as string  ;
              //                item.TOOL_TIP         = reader["TOOL_TIP"]        as string  ;
              //                item.MENU_TYPE_NAME   = reader["MENU_TYPE_NAME"]  as string  ;
              //                item.LEVEL = Convert.ToInt16(reader["LEVEL"]);

              //              Menus.Add(item);                                                                                   
              //          }                                                
              //      }                                                    
              //  }                                                        
              ////  return departmentList;                                 
            }
                return View();
        }
USE [MASTERPROJECT]
GO
INSERT [dbo].[MENU_TYPE] ([MENU_TYPE_ID], [MENU_TYPE_NAME], [ISVISIBLE]) VALUES (1, N'Brand-Label', 1)
GO
INSERT [dbo].[MENU_TYPE] ([MENU_TYPE_ID], [MENU_TYPE_NAME], [ISVISIBLE]) VALUES (2, N'User-Label', 1)
GO
INSERT [dbo].[MENU_TYPE] ([MENU_TYPE_ID], [MENU_TYPE_NAME], [ISVISIBLE]) VALUES (3, N'Menu-Label', 1)
GO
INSERT [dbo].[MENU_TYPE] ([MENU_TYPE_ID], [MENU_TYPE_NAME], [ISVISIBLE]) VALUES (4, N'Control-Label', 1)
GO
INSERT [dbo].[SEC_MENU] ([MENU_ID], [MENU_TEXT], [TOOL_TIP], [ORDER_NO], [REF_URL], [PARENT_ID], [HAS_CHILD], [ICON_IMAGES_LINK], [MENU_TYPE_ID]) VALUES (1, N'Admin LTE', NULL, 1, N'https://localhost:44300/', NULL, 0, N'ref/dist/img/AdminLTELogo.png', 1)
GO
INSERT [dbo].[SEC_MENU] ([MENU_ID], [MENU_TEXT], [TOOL_TIP], [ORDER_NO], [REF_URL], [PARENT_ID], [HAS_CHILD], [ICON_IMAGES_LINK], [MENU_TYPE_ID]) VALUES (2, N'Prabhat Gyawali', NULL, 1, NULL, NULL, 0, N'ref/dist/img/user2-160x160.jpg', 2)
GO
INSERT [dbo].[SEC_MENU] ([MENU_ID], [MENU_TEXT], [TOOL_TIP], [ORDER_NO], [REF_URL], [PARENT_ID], [HAS_CHILD], [ICON_IMAGES_LINK], [MENU_TYPE_ID]) VALUES (3, N'DashBoard', NULL, 1, NULL, NULL, 1, N'fas fa-tachometer-alt', 3)
GO
INSERT [dbo].[SEC_MENU] ([MENU_ID], [MENU_TEXT], [TOOL_TIP], [ORDER_NO], [REF_URL], [PARENT_ID], [HAS_CHILD], [ICON_IMAGES_LINK], [MENU_TYPE_ID]) VALUES (4, N'DashBoard 1', NULL, 1, NULL, 3, 0, N'far fa-circle', 3)
GO
INSERT [dbo].[SEC_MENU] ([MENU_ID], [MENU_TEXT], [TOOL_TIP], [ORDER_NO], [REF_URL], [PARENT_ID], [HAS_CHILD], [ICON_IMAGES_LINK], [MENU_TYPE_ID]) VALUES (5, N'DashBoard 2', N'', 2, NULL, 3, 0, N'far fa-circle', 3)
GO
INSERT [dbo].[SEC_MENU] ([MENU_ID], [MENU_TEXT], [TOOL_TIP], [ORDER_NO], [REF_URL], [PARENT_ID], [HAS_CHILD], [ICON_IMAGES_LINK], [MENU_TYPE_ID]) VALUES (6, N'DashBoard 3', N'', 3, NULL, 3, 1, N'far fa-circle', 3)
GO
INSERT [dbo].[SEC_MENU] ([MENU_ID], [MENU_TEXT], [TOOL_TIP], [ORDER_NO], [REF_URL], [PARENT_ID], [HAS_CHILD], [ICON_IMAGES_LINK], [MENU_TYPE_ID]) VALUES (7, N'Sub 1', N'', 2, NULL, 6, 0, N'far fa-circle', 3)
GO
INSERT [dbo].[SEC_MENU] ([MENU_ID], [MENU_TEXT], [TOOL_TIP], [ORDER_NO], [REF_URL], [PARENT_ID], [HAS_CHILD], [ICON_IMAGES_LINK], [MENU_TYPE_ID]) VALUES (8, N'Sub 2', N'', 1, NULL, 6, 0, N'far fa-circle', 3)
GO
INSERT [dbo].[SEC_MENU] ([MENU_ID], [MENU_TEXT], [TOOL_TIP], [ORDER_NO], [REF_URL], [PARENT_ID], [HAS_CHILD], [ICON_IMAGES_LINK], [MENU_TYPE_ID]) VALUES (9, N'Sub 3', N'', 3, NULL, 6, 0, N'far fa-circle', 3)
GO


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
