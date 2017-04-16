using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBoxMobile.Helpers
{
    public static class UserTypesSupport
    {
        #region Buttons
        private const string BUTTON_1 = "Efficiency";
        private const string BUTTON_2 = "Electricity usage";
        private const string BUTTON_3 = "Production";
        private const string BUTTON_4 = "Notifications";
        private const string BUTTON_5 = "Auxiliary equipment";
        #endregion

        #region MenuItem
        private const string MENUITEM_1 = "Home";
        private const string MENUITEM_2 = "Exit";
        private const string MENUITEM_3 = "Language";
        private const string MENUITEM_4 = "Logout";
        #endregion

        public static Dictionary<int, string> GetButtons(int userType)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            switch(userType)
            {
                case 1:
                    dict.Add(1, BUTTON_1);
                    dict.Add(2, BUTTON_2);
                    dict.Add(3, BUTTON_3);
                    dict.Add(4, BUTTON_4);
                    dict.Add(5, BUTTON_5);
                    break;
                case 2: //testing
                    dict.Add(2, BUTTON_2);
                    dict.Add(3, BUTTON_3);
                    dict.Add(5, BUTTON_5);
                    dict.Add(1, BUTTON_1);
                    break;
            }

            return dict;
        }

        public static Dictionary<int, string> GetMenuItems(int userType)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            switch(userType)
            {
                case 1:
                    dict.Add(1, MENUITEM_1);
                    dict.Add(2, MENUITEM_2);
                    dict.Add(3, MENUITEM_3);
                    dict.Add(4, MENUITEM_4);
                    break;
                case 2: //testing
                    dict.Add(1, MENUITEM_1);
                    dict.Add(2, MENUITEM_2);
                    dict.Add(4, MENUITEM_4);
                    break;
            }

            return dict;
        }
    }
}
