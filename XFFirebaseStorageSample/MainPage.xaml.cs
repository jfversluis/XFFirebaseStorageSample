using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using Xamarin.Forms;

namespace XFFirebaseStorageSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();

            if (photo == null)
                return;

            var task = new FirebaseStorage("samplestorage-f4d9b.appspot.com",
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child("DidYouSubscribe")
                .Child("ToMyChannelYet")
                .Child(photo.FileName)
                .PutAsync(await photo.OpenReadAsync());

            task.Progress.ProgressChanged += (s, args) =>
            {
                progressBar.Progress = args.Percentage;
            };

            var downloadlink = await task;
            downloadLink.Text = downloadlink;
        }
    }
}
