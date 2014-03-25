using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StirTrekConf.Android
{
    using System.Net;
    using global::Android.Graphics;
    using global::Android.Net;
    using PortableCore.DomainLayer;

    public class MainScreenAdapter : BaseAdapter<Speaker>
    {
        private List<Speaker> items;
        private Activity context;
        private int imageCount = 0;

        public MainScreenAdapter(Activity context, List<Speaker> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Speaker this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Name;

            if (imageCount <= 8)
            {
                var imageBitmap = GetImageBitmapFromUrl(item.ImageUrl);
                view.FindViewById<ImageView>(Resource.Id.Image).SetImageBitmap(imageBitmap);

                imageCount++;
            }
            return view;
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}