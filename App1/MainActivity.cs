using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using App1.Resources.controller;
using Plugin.Geolocator.Abstractions;
using System;
using Android;
using Android.Content.PM;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;



namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        TextView latview;
        TextView longview;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            StartService(new Intent(this, typeof(services1)));
            base.OnCreate(savedInstanceState);

            if (CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) != (int)Permission.Granted)
            {
                RequestPermissions(new string[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation }, 0);
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            latview = this.FindViewById<TextView>(Resource.Id.textView2);
            longview = this.FindViewById<TextView>(Resource.Id.textView4);
           

            Button button = this.FindViewById<Button>(Resource.Id.button1);
            button.Click += Button_click;


        }

        private void Button_click(object sender, EventArgs e)
        {
           
            //Thread hiloprincipal = new Thread(new ThreadStart(hilo2))
            //{
            //    IsBackground = true
            //};
            //hiloprincipal.Start();
        }


        private void hilo2()
        {
            try
            {
                timer = new System.Timers.Timer();
                timer.Interval = 5000;
                timer.Elapsed += OnTimedEvent;
                timer.Enabled = true;
                timer.Start();
            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, ex.Message, ToastLength.Long).Show();
            }

        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
         
            Thread hilo = new Thread(new ThreadStart(ejecutarhilo))
            {
                IsBackground = true
            };
            hilo.Start();
        }
        private  async void ejecutarhilo()
        {
            RunOnUiThread(async () =>
            {
                geo _geo = new geo();
                Position _position = await _geo.GetPosition();
                Toast.MakeText(ApplicationContext, _position.Latitude.ToString() + "," + _position.Longitude, ToastLength.Long).Show();

            });
        }
    }
}