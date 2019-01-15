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
using System.Net;
using System.IO;

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
                timer.Interval = 2000;
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
                Position _position = await _geo.GetPosition();
            //    DataService _ServiceData = new DataService();
            TodoItem _TodoItem = new TodoItem()
           {
                lat = (Double)_position.Latitude,
                lng = (Double)_position.Longitude,


            };
            //_TodoItem.lat.set = _position.Altitude.ToString();


            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://hookb.in/qBxP66YDp0SLBj3NpbPJ");

            httpWebRequest.ContentType = "application/json"; //tipo de archivo que contiene o MIME
            httpWebRequest.Method = "POST"; //METODO

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(_TodoItem); 

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }


            //-----------------------------------



            Console.WriteLine(_position.Altitude.ToString() +"," + _position.Longitude.ToString());
             // await _ServiceData.AddTodoItemAsync(_TodoItem);
        }


    }
}