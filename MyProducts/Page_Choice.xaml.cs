using MyProducts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Page_Choice : Page
    {
        public Page_Choice()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.DataContext = App.Ds;
            //await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5));
            
            //Frame root = Window.Current.Content as Frame;
            //ProgressRing PR = (ProgressRing)root.FindName("Prog_Ring");
            //PR.IsActive = false;
            //PR.Visibility = Visibility.Collapsed;
            //Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ShowAsync();
            //var progInd = Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ProgressIndicator;
            //progInd.Text = "Chargement...";
            //progInd.ShowAsync();

        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((SmartPhone)e.ClickedItem).Id;
            this.Frame.Navigate(typeof(Page_Details), itemId);            
        }

        private void listView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            App.Ds.Progress_ring_bool = false;
        }
    }
}
