using MBoxMobile.Helpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBoxMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoPage : ContentPage
    {
        public PhotoPage()
        {
            InitializeComponent();
        }

        private async void ButtonAddPhoto_Clicked(object sender, EventArgs e)
        {
            MediaFile mediaFile = null;

            string action = await DisplayActionSheet(Constants.UI_CONTROL_PHOTO_CHOOSER, "Cancel", null, Constants.UI_CONTROL_PHOTO_CHOOSER_FROM_CAMERA, Constants.UI_CONTROL_PHOTO_CHOOSER_FROM_GALLERY);

            if (action.Equals(Constants.UI_CONTROL_PHOTO_CHOOSER_FROM_GALLERY))
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", "Permission not granted to photos.", "OK");
                    return;
                }

                mediaFile = await CrossMedia.Current.PickPhotoAsync();
            }
            else if (action.Equals(Constants.UI_CONTROL_PHOTO_CHOOSER_FROM_CAMERA))
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", "No camera avaialble.", "OK");
                    return;
                }

                string imageName = string.Format("img_{0}.png", DateTime.UtcNow.ToString("yyyyMMddhhmmss"));
                mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { SaveToAlbum = true, Directory = "MBox Photos", Name = imageName });
            }

            if (mediaFile != null)
            {
                SetMedia(mediaFile);
            }
        }

        private async void ButtonRemovePhoto_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Remove Photo", "Are you sure?", "OK", "Cancel");
            if (result)
            {
                ImageTaken.Source = ImageSource.FromStream(() => null);
                ImageTaken.Source = null;
            }
        }

        private void SetMedia(MediaFile mediaFile)
        {
            byte[] buffer = null;
            using (var memoryStream = new MemoryStream())
            {
                mediaFile.GetStream().CopyTo(memoryStream);
                mediaFile.Dispose();
                buffer = memoryStream.ToArray();
            }

            //buffer = ResizeImage(buffer, 250, 200);
            if (buffer != null && buffer.Length > 0)
            {
                string strBase64 = Convert.ToBase64String(buffer, 0, buffer.Length);
                if (!string.IsNullOrEmpty(strBase64))
                {
                    ImageTaken.Source = ImageSource.FromStream(() => new MemoryStream(buffer));
                }
            }
        }

        //private static byte[] ResizeImage(byte[] imageData, float width, float height)
        //{
        //    IImage image = null;
        //    image = DependencyService.Get<IImage>();
        //    if (image == null)
        //        throw new NullReferenceException("The image resize method was not implemented for this platform!");

        //    return image.ResizeImage(imageData, width, height);
        //}
    }
}
