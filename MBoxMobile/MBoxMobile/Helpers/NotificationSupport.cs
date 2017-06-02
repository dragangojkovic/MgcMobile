using MBoxMobile.Models;
using System;

namespace MBoxMobile.Helpers
{
    public static class NotificationSupport
    {
        public static string HandleNotification(NotificationModel selectedNotification)
        {
            string notificationHandlerPageName = string.Empty;
            if (!App.IsNotificationHandling)
            {
                if (selectedNotification.AlterType == 6436)
                {
                    notificationHandlerPageName = "InputNotificationsKWhPage";
                }
                else
                {
                    switch (selectedNotification.AlterReply)
                    {
                        case 6551:
                            notificationHandlerPageName = "InputNotificationsAcknowledgePage";
                            break;
                        case 6552:
                            notificationHandlerPageName = "InputNotificationsDescriptionPage";
                            break;
                        case 6553:
                            if (string.IsNullOrEmpty(selectedNotification.Description))
                                notificationHandlerPageName = "InputNotificationsDescriptionPage";
                            else
                                notificationHandlerPageName = "InputNotificationsSolutionPage";
                            break;
                        case 6559:
                            notificationHandlerPageName = "InputNotificationsSolutionPage";
                            break;
                        default:
                            notificationHandlerPageName = string.Empty;
                            break;
                    }
                }
            }

            return notificationHandlerPageName;
        }
    }
}
