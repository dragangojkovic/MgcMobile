using System.Collections.Generic;

namespace MBoxMobile.Helpers
{
    public static class UserTypesSupport
    {
        #region Buttons
        private const string BUTTON_1 = "Main_Uptime";
        private const string BUTTON_2 = "Main_ElectricityUsage";
        private const string BUTTON_3 = "Main_Production";
        private const string BUTTON_4 = "Main_Notifications";
        private const string BUTTON_5 = "Main_AuxiliaryEquipment";
        #endregion

        #region MenuItem
        private const string MENUITEM_1 = "Menu_Home";
        private const string MENUITEM_2 = "Menu_Exit";
        private const string MENUITEM_3 = "Menu_Language";
        private const string MENUITEM_4 = "Menu_Logout";
        #endregion

        public static Dictionary<int, string> GetButtons(int userType)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            switch(userType)
            {
                case 1:
                    dict.Add(1, BUTTON_1);
                    //dict.Add(2, BUTTON_2);
                    //dict.Add(3, BUTTON_3);
                    dict.Add(4, BUTTON_4);
                    //dict.Add(5, BUTTON_5);
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
