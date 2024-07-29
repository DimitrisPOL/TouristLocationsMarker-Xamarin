using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TouristGuide.Models;
using TouristGuide.Services;

namespace TouristGuide
{
    [Activity(Label = "SightsList")]
    public class SightsList : ListActivity
    {

        public List<LocationDto> locationsDto;
        HttpClientWrapper _wrapper = new HttpClientWrapper();

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (locationsDto == null)
            {
                //var date = DateTime.Now.AddYears()

                try
                {
                    locationsDto = (List<LocationDto>)await _wrapper.getLoctions();
                    // var testing = (List<LocationDto>)await _wrapper.getLoctionsByDate(DateTime.Now.AddYears(1));
                }
                catch (Exception ex)
                {

                }
            }
            if(locationsDto != null)
            {
                string[] locs = locationsDto.Select(l => l.LocationName).ToArray();
                ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.item_list, locs);
            }

            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            };
        }


        protected override void OnStop()
        {

            base.OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}