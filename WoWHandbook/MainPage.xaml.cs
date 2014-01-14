using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WoWHandbook.backend;
using WoWHandbook.views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WoWHandbook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //responseTextBlock.Text = WoWLookup.getInstance().getLevel(nameField.Text, "Area52").ToString();
            String[] args = { nameField.Text, realmField.Text };
            this.Frame.Navigate(typeof(CharacterPage), args); 
               
        }
        private void NameField_KeyDown(object sender, KeyRoutedEventArgs e)
        {
        }

        private void RealmField_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if ( e.Key == Windows.System.VirtualKey.Enter )
            {
                String[] args = { nameField.Text, realmField.Text };
                this.Frame.Navigate(typeof(CharacterPage), args);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.Frame.BackStack.Add(new PageStackEntry(typeof(MainPage), null, null));
        }



    }
}
