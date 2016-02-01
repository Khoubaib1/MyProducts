using MyProducts.Common;
using MyProducts.Data;
using MyProducts.Models;
using MyProducts.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MyProducts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_Details : Page
    {
        private NavigationHelper navigationHelper;
        private DataSource Ds;
        private DataServices Dserv;
        private SmartPhone Current_Item = new SmartPhone();
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public Page_Details()
        {
            this.InitializeComponent();
            VisualStateManager.GoToState(this, "Portrait", false);
            DisplayInformation displayInfo = DisplayInformation.GetForCurrentView();
            displayInfo.OrientationChanged += DisplayInfoOrientationChanged;
            Dserv = new DataServices();
            Ds = new DataSource();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            
            //this.NavigationHelper.LoadState += NavigationHelper_LoadState;
            //this.navigationHelper.SaveState += NavigationHelper_SaveState;
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            //if (this.Ds.IsEditing)
            //{
            //    e.PageState["isEditing"] = true;
            //    e.PageState["currentText"] = this.viewModel.DataItem.Title;
            //}
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            //if ((e.PageState != null) && e.PageState.ContainsKey("isEditing"))
            //{
            //    this.viewModel.SetEditMode();
            //    this.viewModel.DataItem.Title = e.PageState["currentText"] as string;
            //}
        }

        private void DisplayInfoOrientationChanged(DisplayInformation sender, object args)
        {
            var orientation = sender.CurrentOrientation;
            if (orientation == DisplayOrientations.Landscape || orientation == DisplayOrientations.LandscapeFlipped)
            {
                var res = VisualStateManager.GoToState(this, "Landscape", true);

            }
            if (orientation == DisplayOrientations.Portrait || orientation == DisplayOrientations.PortraitFlipped)
            {
                var res = VisualStateManager.GoToState(this, "Portrait", false);
            }
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.DataContext = App.Ds;
            Current_Item =  App.Ds.GetItem(e.Parameter.ToString());

        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            
            Edit_Description.Visibility = Visibility.Visible;
            Ds.Edit_Icon_bool = true;
            Current_Item.Description = Edit_Description.Text;
            Ds.Smartphone_Details = Current_Item;
            await Dserv.EditSmartphone(Current_Item);
           
        }

        private async void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            await Dserv.DeleteSmartphone(Ds.Smartphone_Details);
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Ds.Edit_Icon_bool = false;
        }
    }
}
