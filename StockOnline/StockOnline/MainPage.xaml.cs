using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace StockOnline
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ToolbarItem item = new ToolbarItem
            {
                Text = "Example Item",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };

            // "this" refers to a Page object
            this.ToolbarItems.Add(item);

        }
    }
}
