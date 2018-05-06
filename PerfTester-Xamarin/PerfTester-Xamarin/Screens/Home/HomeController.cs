﻿using System;

using UIKit;

namespace PerfTesterXamarin.Screens.Home
{
    public partial class HomeController : UIViewController
    {
        public HomeController() : base("HomeController", null)
        {
            SetupNavigationBar();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        void SetupNavigationBar()
        {
            Title = "Home";
        }
    }
}
