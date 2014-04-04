using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.CoreImage;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using StirTrekConf.PortableCore.AppSpecificInterfaces;
using StirTrekConf.PortableCore.DomainLayer;
using MonoTouch.CoreGraphics;
using MonoTouch.ObjCRuntime;

namespace StirTrekConf.iOS
{
    public class SpeakerCollectionViewController : UICollectionViewController
    {
        static NSString speakerCellId = new NSString("SpeakerId");
        static NSString headerId = new NSString("Header");

        private readonly IDatabaseConnection _database;
        private IList<Speaker> speakers;
 
        public SpeakerCollectionViewController(UICollectionViewLayout layout, IDatabaseConnection database) : base(layout)
        {
            _database = database;
            speakers = _database.GetSpeakers();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CollectionView.RegisterClassForCell(typeof(SpeakerCell), speakerCellId);
            CollectionView.RegisterClassForSupplementaryView(typeof(Header), UICollectionElementKindSection.Header, headerId);

            // add a custom menu item
            UIMenuController.SharedMenuController.MenuItems = new UIMenuItem[] { 
				new UIMenuItem ("Custom", new Selector ("custom:")) 
			};

            CollectionView.BackgroundColor = new UIColor(new CGColor(255, 245, 245, 245));
            Title = "Stir Trek Conf";
        }

        public override int NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override int GetItemsCount(UICollectionView collectionView, int section)
        {
            return speakers.Count;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, MonoTouch.Foundation.NSIndexPath indexPath)
        {
            var speakerCell = (SpeakerCell)collectionView.DequeueReusableCell(speakerCellId, indexPath);

            var speaker = speakers[indexPath.Row];

            var image = UIImage.FromFile(string.Format("images/{0}.png", speaker.Id));
            speakerCell.Name = speaker.Name;
            speakerCell.Image = image;

            return speakerCell;
        }

        public override UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            var headerView = (Header)collectionView.DequeueReusableSupplementaryView(elementKind, headerId, indexPath);
            headerView.Text = "Speakers";
            return headerView;
        }

        public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.CellForItem(indexPath);
            cell.ContentView.BackgroundColor = UIColor.Yellow;
        }

        public override void ItemUnhighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.CellForItem(indexPath);
            cell.ContentView.BackgroundColor = UIColor.White;
        }

        public override bool ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

        // for edit menu
        public override bool ShouldShowMenu(UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

        public override bool CanPerformAction(UICollectionView collectionView, MonoTouch.ObjCRuntime.Selector action, NSIndexPath indexPath, NSObject sender)
        {
            return true;
        }

        public override void PerformAction(UICollectionView collectionView, MonoTouch.ObjCRuntime.Selector action, NSIndexPath indexPath, NSObject sender)
        {
            Console.WriteLine("code to perform action");
        }

        // CanBecomeFirstResponder and CanPerform are needed for a custom menu item to appear
        public override bool CanBecomeFirstResponder
        {
            get
            {
                return true;
            }
        }

        public override bool CanPerform(Selector action, NSObject withSender)
        {
            if (action == new Selector("custom"))
                return true;
            else
                return false;
        }

        // System provided cut, copy and paste will be sent to PerformAction method above, but any custom menu items
        // must have their assocatied actions implementated explicitly
        [Export("custom")]
        void Custom()
        {
            Console.WriteLine("custom");
        }
    }

    public class SpeakerCell : UICollectionViewCell
    {
        UITextField usernameField;
        UIImageView imageView;
        private UILabel labelView;

        [Export("initWithFrame:")]
        public SpeakerCell(System.Drawing.RectangleF frame)
            : base(frame)
        {
            //BackgroundView = new UIView { BackgroundColor = UIColor.Orange };

            //SelectedBackgroundView = new UIView { BackgroundColor = UIColor.Green };

            //ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
            //ContentView.Layer.BorderWidth = 2.0f;
            //ContentView.BackgroundColor = UIColor.White;
            //ContentView.Transform = CGAffineTransform.MakeScale(0.8f, 0.8f);
            var im = UIImage.FromFile("images/45.png");
            
            imageView = new UIImageView(im);
            
            float h = 31.0f;
            float w = 300.0f;

            labelView = new UILabel
            {
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.PreferredCaption1,
                Frame = new RectangleF(50.0f, (im.Size.Height/2)-25.0f, w - 50.0f, h)
            };

            ContentView.AddSubview(imageView);
            ContentView.AddSubview(labelView);

        }

        public UIImage Image
        {
            set
            {
                imageView.Image = value;
            }
        }

        public string Name
        {
            set { labelView.Text = value; }
        }
    }

    public class Header : UICollectionReusableView
    {
        UILabel label;

        public string Text
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
                SetNeedsDisplay();
            }
        }

        [Export("initWithFrame:")]
        public Header(RectangleF frame)
            : base(frame)
        {
            label = new UILabel
            { 
                Frame = new RectangleF(0, 0, 300, 50), 
                TextColor = UIColor.Blue,
                Font = UIFont.PreferredHeadline,
            };

            AddSubview(label);
        }
    }
}