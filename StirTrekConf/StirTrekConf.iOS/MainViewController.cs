using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using StirTrekConf.iOS.DependencyImplementation;
using StirTrekConf.PortableCore.AppSpecificInterfaces;
using StirTrekConf.PortableCore.DomainLayer;
using StirTrekConf.PortableCore.WebServiceLayer;

namespace StirTrekConf.iOS
{
    //[Register("UniversalView")]
    //public class UniversalView : UIView
    //{
    //    public UniversalView()
    //    {
    //        Initialize();
    //    }

    //    public UniversalView(RectangleF bounds)
    //        : base(bounds)
    //    {
    //        Initialize();
    //    }

    //    void Initialize()
    //    {
    //        BackgroundColor = UIColor.Red;
    //    }
    //}

    [Register("MainViewController")]
    public class MainViewController : DialogViewController
    {
        private IDatabaseConnection _database;
        private WebServiceRepo _webServiceRepo;
        private UITableView table;
        private bool waiting = true;

        public MainViewController() : base(UITableViewStyle.Plain, null)
        {
            _database = new DatabaseConnection();
            _webServiceRepo = new WebServiceRepo(new WebServicRestClient_iOS(), _database, new JsonProcessor());
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override async void ViewDidLoad()
        {
            //View = new UniversalView();

            base.ViewDidLoad();
            table = new UITableView();
            
            View.BackgroundColor = new UIColor(new CGColor(255, 245, 245, 245));
            Add(table);
            
            Title = "Stir Trek Conf";

            //_webServiceRepo.LoadStirTrekFeed(LoadSpeakerData);

            var speakers = _database.GetSpeakers();

            var something = speakers.Select(x => x.Name).ToArray();

            table.Source = new TableSource(new string[]{"Tom", "Jerrel"});
            //table.ReloadData();
            
        }

        public void LoadSpeakerData()
        {
            InvokeOnMainThread(()=> { waiting = false; });
        }

    }

    public class TableSource : UITableViewSource
    {
        protected string[] tableItems;
        protected string cellIdentifier = "TableCell";
        public TableSource(string[] items)
        {
            tableItems = items;
        }
        public override int RowsInSection(UITableView tableview, int section)
        {
            return tableItems.Length;
        }
        public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
        {
            // request a recycled cell to save memory
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            cell.TextLabel.Text = tableItems[indexPath.Row];
            return cell;
        }
    }
}