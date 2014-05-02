using WoWHandbook.Common;
using WoWHandbook.Data;
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
using BlizzAPI.WoW;
using BlizzAPI.WoW.character;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Xaml.Media.Imaging;
using BlizzAPI.WoW.character.items;
using Windows.UI;
using BlizzAPI.WoW.Items;

// The Hub Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=321224

namespace WoWHandbook.Views.Character
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private BlizzAPI.WoW.character.Character character;
        private WoWClient wowClient;
        private Dictionary<String, String> statDictionary;
        private Dictionary<String, String> equippedImageURLS;
        private Dictionary<String, EquippedItem> equippedItemsDictionary;
        private Dictionary<ItemQuality, Color> itemQualityColor;
        private Dictionary<String, Item> detailedItems;
        private Popup itemDetailPopup;
        private MyUserControl1 itemDetailPopupContent;
        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public HubPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-4");
            this.DefaultViewModel["Section3Items"] = sampleDataGroup;
        }

        /// <summary>
        /// Invoked when a HubSection header is clicked.
        /// </summary>
        /// <param name="sender">The Hub that contains the HubSection whose header was clicked.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            HubSection section = e.Section;
            var group = section.DataContext;
            //this.Frame.Navigate(typeof(SectionPage), ((SampleDataGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a section is clicked.
        /// </summary>
        /// <param name="sender">The GridView or ListView
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemPage), itemId);
        }
        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            wowClient = new WoWClient(new BlizzAPI.Region(BlizzAPI.Region.Regions.US));
            var myList = e.Parameter as List<string>;
            var task = Task.Run(async () => { this.character = await wowClient.getCharacter(myList.ElementAt(1), myList.ElementAt(0)); });

            navigationHelper.OnNavigatedTo(e);
            task.Wait();
            //while (!task.IsCompleted) { Debug.WriteLine(character == null); }
            pageTitle.Text = character.Name + " - " + character.Realm;


            statDictionary = new Dictionary<string, string>()
            {
                {"strengthValue", character.Stats.Strength.ToString()},
                {"agilityValue", character.Stats.Agility.ToString()},
                {"staminaValue", character.Stats.Stamina.ToString()},
                {"intellectValue", character.Stats.Intellect.ToString()},
                {"spiritValue", character.Stats.Spirit.ToString()},
                {"masteryValue", character.Stats.Mastery.ToString("0.00") + "%"},

                {"armorValue", character.Stats.Armor.ToString()},
                {"dodgeValue", character.Stats.Dodge.ToString("0.00") + "%"},
                {"parryValue", character.Stats.Parry.ToString("0.00") + "%"},
                {"blockValue", character.Stats.Block.ToString("0.00") + "%"},
                {"pvpResilienceValue", character.Stats.PvPResilience.ToString("0.00") + "%"},
                {"pvpPowerValue", character.Stats.PvPPower.ToString("0.00") + "%"},

                {"spellPowerValue", character.Stats.SpellPower.ToString()},
                {"spellHasteValue", character.Stats.SpellHaste.ToString("0.00") + "%"},
                {"spellHitValue", "+" + character.Stats.SpellHitPercent.ToString("0.00") + "%"},
                {"manaRegenValue", character.Stats.ManaPer5Seconds.ToString()},
                {"manaCombatRegenValue", character.Stats.ManaCombatPer5Seconds.ToString()},
                {"spellCritValue", character.Stats.SpellCrit.ToString("0.00") + "%"},

                
                {"meleeAttackPowerValue", character.Stats.AttackPower.ToString()},
                {"meleeHasteValue", character.Stats.Haste.ToString("0.00") + "%"},
                {"meleeHitValue", "+" + character.Stats.HitPercent.ToString("0.00") + "%"},
                {"meleeCritValue", character.Stats.Crit.ToString("0.00") + "%"},
                {"meleeExpertiseValue", character.Stats.ExpertiseRating.ToString("0.00") + "%"},
                
                {"rangedAttackPowerValue", character.Stats.RangedAttackPower.ToString()},
                {"rangedHasteValue", character.Stats.RangedHaste.ToString("0.00") + "%"},
                {"rangedHitValue", "+" + character.Stats.RangedHitPercent.ToString("0.00") + "%"},
                {"rangedCritValue", character.Stats.RangedCrit.ToString("0.00") + "%"}
            };

            String baseIconURL = "http://us.media.blizzard.com/wow/icons/56/";
            equippedImageURLS = new Dictionary<string, string>()
            {
                {"imageHead", character.EquippedItems.Head != null ? baseIconURL + character.EquippedItems.Head.Icon + ".jpg" : null},
                {"imageNeck", character.EquippedItems.Neck != null ? baseIconURL + character.EquippedItems.Neck.Icon + ".jpg" : null},
                {"imageShoulder", character.EquippedItems.Shoulder != null ? baseIconURL + character.EquippedItems.Shoulder.Icon + ".jpg" : null},
                {"imageBack", character.EquippedItems.Back != null ? baseIconURL + character.EquippedItems.Back.Icon + ".jpg" : null},
                {"imageChest", character.EquippedItems.Chest != null ? baseIconURL + character.EquippedItems.Chest.Icon + ".jpg" : null},
               // {"imageShirt", character.EquippedItems.Head != null ? baseIconURL + character.EquippedItems.Shirt.Icon + ".jpg" : null},
               // {"imageTabard", character.EquippedItems.Head != null ? baseIconURL + character.EquippedItems.Tabard.Icon + ".jpg" : null},
                {"imageWrist", character.EquippedItems.Wrist != null ? baseIconURL + character.EquippedItems.Wrist.Icon + ".jpg" : null},
                {"imageHands", character.EquippedItems.Hands != null ? baseIconURL + character.EquippedItems.Hands.Icon + ".jpg" : null},
                {"imageWaist", character.EquippedItems.Waist != null ? baseIconURL + character.EquippedItems.Waist.Icon + ".jpg" : null},
                {"imageLegs", character.EquippedItems.Legs != null ? baseIconURL + character.EquippedItems.Legs.Icon + ".jpg" : null},
                {"imageFeet", character.EquippedItems.Feet != null ? baseIconURL + character.EquippedItems.Feet.Icon + ".jpg" : null},
                {"imageRing1", character.EquippedItems.Finger1 != null ? baseIconURL + character.EquippedItems.Finger1.Icon + ".jpg" : null},
                {"imageRing2", character.EquippedItems.Finger2 != null ? baseIconURL + character.EquippedItems.Finger2.Icon + ".jpg" : null},
                {"imageTrinket1", character.EquippedItems.Trinket1 != null ? baseIconURL + character.EquippedItems.Trinket1.Icon + ".jpg" : null},
                {"imageTrinket2", character.EquippedItems.Trinket2 != null ? baseIconURL + character.EquippedItems.Trinket2.Icon + ".jpg" : null},
                {"imageMainHand", character.EquippedItems.MainHand != null ? baseIconURL + character.EquippedItems.MainHand.Icon + ".jpg" : null},
                {"imageOffHand", character.EquippedItems.OffHand != null ? baseIconURL + character.EquippedItems.OffHand.Icon + ".jpg" : null},
            };

            equippedItemsDictionary = new Dictionary<string, EquippedItem>()
            {
                {"HEAD", character.EquippedItems.Head},
                {"NECK", character.EquippedItems.Neck},
                {"SHOULDER", character.EquippedItems.Shoulder},
                {"BACK", character.EquippedItems.Back},
                {"CHEST", character.EquippedItems.Chest},
                {"SHIRT", character.EquippedItems.Shirt},
                {"TABARD", character.EquippedItems.Tabard},
                {"WRIST", character.EquippedItems.Wrist},
                {"HANDS", character.EquippedItems.Hands},
                {"WAIST", character.EquippedItems.Waist},
                {"LEGS", character.EquippedItems.Legs},
                {"FEET", character.EquippedItems.Feet},
                {"RING1", character.EquippedItems.Finger1},
                {"RING2", character.EquippedItems.Finger2},
                {"TRINKET1", character.EquippedItems.Trinket1},
                {"TRINKET2", character.EquippedItems.Trinket2},
                {"MAINHAND", character.EquippedItems.MainHand},
                {"OFFHAND", character.EquippedItems.OffHand}
            };

            getDetailedItems();

            itemQualityColor = new Dictionary<ItemQuality, Color>()
            {
                {ItemQuality.POOR, Colors.Gray},
                {ItemQuality.COMMON, Colors.White},
                {ItemQuality.UNCOMMON, Colors.Green},
                {ItemQuality.RARE, Colors.Blue},
                {ItemQuality.EPIC, Colors.Purple},
                {ItemQuality.LEGENDARY, Colors.Orange},
                {ItemQuality.ARTIFACT, Colors.Gold},
                {ItemQuality.HEIRLOOM, Colors.Gold}

            };
            itemDetailPopupContent = new MyUserControl1();
            itemDetailPopup = new Popup();
            itemDetailPopup.Child = itemDetailPopupContent;
        }

        private async void getDetailedItems()
        {
            detailedItems = new Dictionary<string, Item>();
            foreach (String key in equippedItemsDictionary.Keys)
            {
                EquippedItem equippedItem = null;
                equippedItemsDictionary.TryGetValue(key, out equippedItem);
                if (equippedItem == null)
                    continue;
                detailedItems.Add(key, await wowClient.getItem(equippedItem.ID));
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void loaded(object sender, RoutedEventArgs e)
        {
            String sourceString = "http://us.battle.net/static-render/us/" + character.Thumbnail.Replace("-avatar.jpg", "") + "-profilemain.jpg";
            (equippedItems.Background as ImageBrush).ImageSource = new BitmapImage(new Uri(sourceString, UriKind.Absolute));
        }

        private void statLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            String stat = "";
            statDictionary.TryGetValue(textBlock.Name, out stat);
            if (stat != null)
                textBlock.Text = stat;
            else
                textBlock.Text = "null";
        }

        private void equippedItemsHubSectionLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            HubSection hubSection = sender as HubSection;
            hubSection.MinWidth = Math.Max(700, Window.Current.Bounds.Width / 2f);
        }

        private void equippedItemLoaded(object sender, RoutedEventArgs e)
        {
            Image itemImage = sender as Image;
            if (itemImage == null)
                return;
            String imageURL = "";
            equippedImageURLS.TryGetValue(itemImage.Name, out imageURL);
            if (imageURL != null)
            {
                itemImage.Source = new BitmapImage(new Uri(imageURL, UriKind.Absolute));
            }
        }

        private void equippedItemBorderLoaded(object sender, RoutedEventArgs e)
        {
            Border border = sender as Border;
            if (border != null)
            {

                EquippedItem item = null;
                equippedItemsDictionary.TryGetValue(border.Name.Replace("border", "").ToUpper(), out item);
                if (item != null)
                {
                    Color color = Colors.Black;
                    itemQualityColor.TryGetValue(item.Quality, out color);
                    border.BorderBrush = new SolidColorBrush(color);
                }
                else
                {
                    Debug.WriteLine("item null " + border.Name.Replace("border", ""));
                }
            }
        }

        private void pointerEnteredImage(object sender, PointerRoutedEventArgs e)
        {
            var image = sender as Image;
            Item item = null;
            detailedItems.TryGetValue(image.Name.Replace("image", "").ToUpper(), out item);
            EquippedItem equippedItem = null;
            equippedItemsDictionary.TryGetValue(image.Name.Replace("image", "").ToUpper(), out equippedItem);
            if (item != null)
                itemDetailPopupContent.loadFromItem(item, equippedItem);
            else
                return;
            itemDetailPopupContent.UpdateLayout();
            itemDetailPopupContent.InvalidateMeasure();
            itemDetailPopupContent.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            double heightOfPopup = itemDetailPopupContent.DesiredSize.Height;
            double widthOfPopup = itemDetailPopupContent.RenderSize.Width;
            double verticalOffset = image.TransformToVisual(Window.Current.Content).TransformPoint(new Point(0, 0)).Y;
            if (heightOfPopup + verticalOffset > Window.Current.Content.RenderSize.Height)
            {
                verticalOffset -= heightOfPopup - image.RenderSize.Height;
            }

            itemDetailPopup.HorizontalOffset = image.TransformToVisual(Window.Current.Content).TransformPoint(new Point(0, 0)).X + image.RenderSize.Width + 20;
            itemDetailPopup.VerticalOffset = verticalOffset;
            itemDetailPopup.IsOpen = true;
        }

        private void pointerExitedImage(object sender, PointerRoutedEventArgs e)
        {
            itemDetailPopup.IsOpen = false;
        }
    }
}
