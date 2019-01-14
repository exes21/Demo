using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using App1.Resources.controller;
using Plugin.Geolocator.Abstractions;
using Newtonsoft.Json;
namespace App1.Resources.controller

{
    [Service]
public class services1 : Service
{
    static readonly string TAG = typeof(services1).FullName;
    System.Timers.Timer timer = new System.Timers.Timer();
    public IBinder Binder { get; private set; }
    public override void OnCreate()
    {
        // This method is optional to implement
        base.OnCreate();

        Thread hiloprincipal = new Thread(new ThreadStart(hilo2))
        {
            IsBackground = true
        };
        hiloprincipal.Start();

       



    }

    public override IBinder OnBind(Intent intent)
    {
        // This method must always be implemented
        Log.Debug(TAG, "OnBind");
        return this.Binder;
    }

    public override bool OnUnbind(Intent intent)
    {
        // This method is optional to implement
        Log.Debug(TAG, "esto estara funciona");
        return base.OnUnbind(intent);
    }

    public override void OnDestroy()
    {
        // This method is optional to implement
        Log.Debug(TAG, "OnDestroy");
        Binder = null;
        base.OnDestroy();
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
        private async void ejecutarhilo()
        {
                geo _geo = new geo();
                DataService _ServiceData = new DataService();
                TodoItem _TodoItem = new TodoItem();
                Position _position = await _geo.GetPosition();
            Console.WriteLine(_position.Altitude.ToString() +"," + _position.Longitude.ToString());
                await _ServiceData.AddTodoItemAsync(_TodoItem);
        }


    }
}