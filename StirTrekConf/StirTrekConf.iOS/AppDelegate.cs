﻿using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using StirTrekConf.iOS.DependencyImplementation;
using StirTrekConf.PortableCore.AppSpecificInterfaces;
using StirTrekConf.PortableCore.WebServiceLayer;

namespace StirTrekConf.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow _window;
        private SpeakerCollectionViewController _speakerCollectionViewController;
        UICollectionViewFlowLayout _flowLayout;
        private DatabaseConnection _databaseConnection;
        private WebServiceRepo _webServiceRepo;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            _window = new UIWindow(UIScreen.MainScreen.Bounds);

            _databaseConnection = new DatabaseConnection();
            _webServiceRepo = new WebServiceRepo(new WebServicRestClient_iOS(), _databaseConnection, new JsonProcessor());

            if (!_databaseConnection.IsDataDownloaded())
            {
                _webServiceRepo.LoadStirTrekFeed(() => { });
            }

            // Flow Layout
            _flowLayout = new UICollectionViewFlowLayout()
            {
                HeaderReferenceSize = new SizeF(300, 50),
                SectionInset = new UIEdgeInsets(0, 20, 20, 20),
                ScrollDirection = UICollectionViewScrollDirection.Vertical,
                MinimumInteritemSpacing = 50, // minimum spacing between cells
                MinimumLineSpacing = 50, // minimum spacing between rows if ScrollDirection is Vertical or between columns if Horizontal
                ItemSize = new SizeF(300, 100)
            };
           
            _speakerCollectionViewController = new SpeakerCollectionViewController(_flowLayout, new DatabaseConnection());

            _speakerCollectionViewController.CollectionView.ContentInset = new UIEdgeInsets(5,5,5,5);

            var navController = new UINavigationController(_speakerCollectionViewController);
            
            // If you have defined a view, add it here:
            _window.RootViewController = navController;

            // make the window visible
            _window.MakeKeyAndVisible();

            return true;
        }
    }
}