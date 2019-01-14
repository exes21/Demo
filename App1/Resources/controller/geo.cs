using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace App1.Resources.controller
{
    public class geo
    {
        //public  Position _position;

        //public geo()
        //{
        //    GetPosition();
        //}

        public async Task<Position> GetPosition()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var myPosition = await locator.GetPositionAsync();
            Position _position = new Position(myPosition.Latitude, myPosition.Longitude);
            return _position;
        }

      
    }
}