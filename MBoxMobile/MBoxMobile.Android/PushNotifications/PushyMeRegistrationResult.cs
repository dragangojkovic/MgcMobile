using System;
using ME.Pushy.Sdk.Util.Exceptions;

namespace MBoxMobile.Droid.PushNotifications
{
	public class PushyMeRegistrationResult
	{
		public string DeviceToken { get; set; }
		public PushyException Error { get; set; }
	}
}
