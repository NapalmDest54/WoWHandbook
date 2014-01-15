using WoWHandbook.backend;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using WOWSharp.Community.Wow;
using WOWSharp.Community;
using System.Collections.Concurrent;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WoWHandbook.views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharacterPage : Page
    {
        private Character character = null;
        private String sourceSting = "http://media.blizzard.com/wow/icons/56/";
        private ConcurrentDictionary<String, EquippedItem> itemDictionary;
        private string[] itemImageNames = { "imageHelm", "imageNeck", "imageShoulder", "imageBack", "imageChest", "imageShirt", "imageTabard", "imageWrist", "imageGloves", "imageWaist", "imageLegs",
                                          "imageFeet", "imageFinger1", "imageFinger2", "imageTrinket1", "imageTrinket2", "imageMainHand", "imageOffHand" };

        public CharacterPage()
        {
            itemDictionary = new ConcurrentDictionary<string, EquippedItem>();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            String[] args = e.Parameter as String[];
            createCharacter(args[0], args[1]);
        }

        private async void createCharacter(String characterName, String realm)
        {
            var client = new WowClient(Region.US);
            character = await client.GetCharacterAsync(realm, characterName, CharacterFields.All);
            itemDictionary.TryAdd("imageHelm", character.Items.Head);
            itemDictionary.TryAdd("imageNeck", character.Items.Neck);
            itemDictionary.TryAdd("imageShoulder", character.Items.Shoulder);
            itemDictionary.TryAdd("imageBack", character.Items.Back);
            itemDictionary.TryAdd("imageChest", character.Items.Chest);
            itemDictionary.TryAdd("imageShirt", character.Items.Shirt);
            itemDictionary.TryAdd("imageTabard", character.Items.Tabard);
            itemDictionary.TryAdd("imageWrist", character.Items.Wrist);
            itemDictionary.TryAdd("imageGloves", character.Items.Hands);
            itemDictionary.TryAdd("imageWaist", character.Items.Waist);
            itemDictionary.TryAdd("imageLegs", character.Items.Legs);
            itemDictionary.TryAdd("imageFeet", character.Items.Feet);
            itemDictionary.TryAdd("imageFinger1", character.Items.Finger1);
            itemDictionary.TryAdd("imageFinger2", character.Items.Finger2);
            itemDictionary.TryAdd("imageTrinket1", character.Items.Trinket1);
            itemDictionary.TryAdd("imageTrinket2", character.Items.Trinket2);
            itemDictionary.TryAdd("imageMainHand", character.Items.MainHand);
            itemDictionary.TryAdd("imageOffHand", character.Items.Offhand);

            this.InitializeComponent();
            characterTitle.Text = characterName + "  -  " + character.Realm;
        }

        private void baseStatsLoaded(object sender, RoutedEventArgs e)
        {
            lock (this)
            {
                TextBlock baseStats = sender as TextBlock;
                StringBuilder sb = new StringBuilder();
                switch (baseStats.Name)
                {
                    case "baseStats":
                        sb.Append("Agility:\t\t" + character.Stats.Agility);
                        sb.AppendLine();
                        sb.Append("Stamina:\t\t" + character.Stats.Stamina);
                        sb.AppendLine();
                        sb.Append("Intellect:\t\t" + character.Stats.Intellect);
                        sb.AppendLine();
                        sb.Append("Spirit:\t\t" + character.Stats.Spirit);
                        sb.AppendLine();
                        sb.Append("Mastery:\t\t" + character.Stats.MasteryRating);
                        break;
                    case "spellStats":
                        sb.Append("Spell Power: " + character.Stats.Intellect);
                        sb.AppendLine();
                        sb.Append("Haste: " + character.Stats.HasteRating);
                        sb.AppendLine();
                        sb.Append("Hit: " + character.Stats.HitRating);
                        sb.AppendLine();
                        sb.Append("Mana Regen: " + character.Stats.ManaPer5);
                        sb.AppendLine();
                        sb.Append("Combat Regen: " + character.Stats.ManaPer5Combat);
                        sb.AppendLine();
                        sb.Append("Crit: " + character.Stats.CritRating);
                        break;
                    case "defenseStats":
                        sb.Append("Armor:\t\t" + character.Stats.Armor);
                        sb.AppendLine();
                        sb.Append("Dodge:\t\t" + character.Stats.DodgeChance);
                        sb.AppendLine();
                        sb.Append("Parry:\t\t" + character.Stats.ParryChance);
                        sb.AppendLine();
                        sb.Append("Block:\t\t" + character.Stats.BlockChance);
                        break;
                    case "rangedStats":
                        sb.Append("Atack Power: " + character.Stats.RangedAttackPower);
                        sb.AppendLine();
                        sb.Append("Haste: " + character.Stats.HasteRating);
                        sb.AppendLine();
                        sb.Append("Hit: " + character.Stats.HitRating);
                        sb.AppendLine();
                        sb.Append("Crit: " + character.Stats.CritRating);
                        break;
                    case "meleeStats":
                        sb.Append("Attack Power:\t" + character.Stats.MeleeAttackPower);
                        sb.AppendLine();
                        sb.Append("Haste:\t" + character.Stats.HasteRating);
                        sb.AppendLine();
                        sb.Append("Hit:\t" + character.Stats.HitRating);
                        sb.AppendLine();
                        sb.Append("Crit:\t" + character.Stats.CritRating);
                        sb.AppendLine();
                        sb.Append("Expertise:\t" + character.Stats.ExpertiseRating);
                        break;
                    default:
                        break;
                }



                baseStats.Text = sb.ToString();

            }

        }


        private void TalentListViewLoaded(object sender, RoutedEventArgs e)
        {
            ListView talentList = (ListView)sender;

            character.Talents.ElementAt(0).ToString();

        }



        private void TalentButtonLoaded(object sender, RoutedEventArgs e)
        {
            Button talentButton = sender as Button;

        }

        private void TalentButtonTextLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            try
            {
                tb.Text = character.Talents[0].Build[Int32.Parse(tb.Name.Replace("talentText", "")) - 1].Spell.Name;
                Spell spell = character.Talents[0].Build[0].Spell;
            }
            catch (ArgumentOutOfRangeException)
            {
                tb.Text = "Empty";
            }
        }

        private void TalentImageLoaded(object sender, RoutedEventArgs e)
        {
            Image tb = sender as Image;
            try
            {
                //
                //tb.Source = new BitmapImage(new Uri("ms-appx:///Assets/" + character.Talents[0].Build[Int32.Parse(tb.Name.Replace("talentImage", "")) - 1].Spell.Icon + ".jpg"));
                String sourceString = "http://media.blizzard.com/wow/icons/56/";
                try
                {
                    sourceString += character.Talents[0].Build[Int32.Parse(tb.Name.Replace("talentImage", "")) - 1].Spell.Icon + ".jpg";
                    tb.Source = new BitmapImage(new Uri(sourceString, UriKind.Absolute));
                }
                catch (ArgumentOutOfRangeException)
                {

                }

            }
            catch (Exception)
            {

            }
        }

        private void CharacterImageLoaded(object sender, RoutedEventArgs e)
        {
            ImageBrush tb = (sender as Grid).Background as ImageBrush;

            try
            {
                String sourceString = "http://us.battle.net/static-render/us/" + character.Thumbnail.Replace("-avatar.jpg", "") + "-profilemain.jpg";
                tb.ImageSource = new BitmapImage(new Uri(sourceString, UriKind.Absolute));
            }
            catch (Exception)
            {
            }
        }

        private void itemSetLoaded(object sender, RoutedEventArgs e)
        {

            Image image = sender as Image;
            try
            {
               
                EquippedItem eq = new EquippedItem();
                itemDictionary.TryGetValue(image.Name, out eq);
                equippedItemHelper(image, eq);
            }
            catch (NullReferenceException)
            {

            }



            //image
        }

        private void equippedItemHelper(Image image, EquippedItem item)
        {
            if (item == null)
            {

                image.Opacity = 0.5;
                (image.Parent as Border).Opacity = 0.5;
                return;
            }

            image.Source = new BitmapImage(new Uri(sourceSting + item.Icon + ".jpg", UriKind.Absolute));
            Border border = image.Parent as Border;
            switch (item.Quality)
            {

                case ItemQuality.Common:
                    border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);
                    break;
                case ItemQuality.Uncommon:
                    border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Green);
                    break;
                case ItemQuality.Rare:
                    border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Blue);
                    break;
                case ItemQuality.Epic:
                    border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Purple);
                    break;
                case ItemQuality.Legendary:
                    border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Orange);
                    break;
                case ItemQuality.Artifact:
                    border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.LightGoldenrodYellow);
                    break;
                case ItemQuality.Heirloom:
                    border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.LightGoldenrodYellow);
                    break;
            }
        }

        private void backButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void EquipmentPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Image image = sender as Image;
            
            switch (image.Name)
            {
                case "imageHelm":
                    equipmentPopupHelper(image, character.Items.Head, "Head");
                    break;
                case "imageNeck":
                    equipmentPopupHelper(image, character.Items.Neck, "Neck");
                    break;
                case "imageShoulder":
                    equipmentPopupHelper(image, character.Items.Shoulder, "Shoulder");
                    break;
                case "imageBack":
                    equipmentPopupHelper(image, character.Items.Back, "Back");
                    break;
                case "imageChest":
                    equipmentPopupHelper(image, character.Items.Chest, "Chest");
                    break;
                case "imageWrist":
                    equipmentPopupHelper(image, character.Items.Wrist, "Wrist");
                    break;
                case "imageGloves":
                    equipmentPopupHelper(image, character.Items.Hands, "Hands");
                    break;
                case "imageWaist":
                    equipmentPopupHelper(image, character.Items.Waist, "Waist");
                    break;
                case "imageMainHand":
                    equipmentPopupHelper(image, character.Items.MainHand, "Main Hand");
                    break;
                case "imageOffHand":
                    equipmentPopupHelper(image, character.Items.Offhand, "Off hand");
                    break;
                case "imageLegs":
                    equipmentPopupHelper(image, character.Items.Legs, "Legs");
                    break;
                case "imageFinger1":
                    equipmentPopupHelper(image, character.Items.Finger1, "Finger");
                    break;
                case "imageFinger2":
                    equipmentPopupHelper(image, character.Items.Finger2, "Finger");
                    break;
                case "imageTrinket1":
                    equipmentPopupHelper(image, character.Items.Trinket1, "Trinket");
                    break;
                case "imageTrinket2":
                    equipmentPopupHelper(image, character.Items.Trinket2, "Trinket");
                    break;
                case "imageFeet":
                    equipmentPopupHelper(image, character.Items.Feet, "Feet");
                    break;

                default:
                    break;
            }
        }

        private void equipmentPopupHelper(Image image, EquippedItem item, String slot)
        {
            if (item == null)
                return;
            itemInfoFrame.Visibility = Visibility.Visible;
            var ttv = image.TransformToVisual(Window.Current.Content);
            Point screenCoords = ttv.TransformPoint(new Point(0, 0));
            Thickness margin = itemInfoFrame.Margin;
            margin.Left = screenCoords.X + image.ActualWidth + 20;
            margin.Top = screenCoords.Y;
            itemInfoFrame.Margin = margin;


            itemInfoName.Text = item.Name;
            SolidColorBrush solidColorBrush;
            switch (item.Quality)
            {

                case ItemQuality.Common:
                    solidColorBrush = new SolidColorBrush(Windows.UI.Colors.White);
                    break;
                case ItemQuality.Uncommon:
                    solidColorBrush = new SolidColorBrush(Windows.UI.Colors.Green);
                    break;
                case ItemQuality.Rare:
                    solidColorBrush = new SolidColorBrush(Windows.UI.Colors.Blue);
                    break;
                case ItemQuality.Epic:
                    solidColorBrush = new SolidColorBrush(Windows.UI.Colors.Purple);
                    break;
                case ItemQuality.Legendary:
                    solidColorBrush = new SolidColorBrush(Windows.UI.Colors.Orange);
                    break;
                case ItemQuality.Artifact:
                    solidColorBrush = new SolidColorBrush(Windows.UI.Colors.LightGoldenrodYellow);
                    break;
                case ItemQuality.Heirloom:
                    solidColorBrush = new SolidColorBrush(Windows.UI.Colors.LightGoldenrodYellow);
                    break;
                default:
                    solidColorBrush = new SolidColorBrush(Windows.UI.Colors.White);
                    break;
            }
            itemInfoName.Foreground = solidColorBrush;
            itemInfoItemLevel.Text = "Item Level " + item.ItemLevel;
            try
            {
                itemInfoUpgradeLevel.Text = "Upgrade Level: " + item.Parameters.Upgrade.Current + "/" + item.Parameters.Upgrade.Total;
                itemInfoUpgradeLevel.Visibility = Visibility.Visible;
            }
            catch (NullReferenceException)
            {
                itemInfoUpgradeLevel.Visibility = Visibility.Collapsed;
            }

            itemInfoSlot.Text = slot;
            if (item.Armor > 0)
            {
                itemInfoArmor.Text = item.Armor + " Armor";
                itemInfoArmor.Visibility = Visibility.Visible;
            }
            else
            {
                itemInfoArmor.Visibility = Visibility.Collapsed;
            }
            StringBuilder stats = new StringBuilder();
            ItemStatType? reforgedFrom = item.Parameters.ReforgedToStat;
            ItemStatType? reforgedTo = item.Parameters.ReforgedFromStat;
            for (int i = 0; i < item.Stats.Count; i++)
            {
                ItemStat stat = item.Stats.ElementAt(i);
                stats.Append("+" + stat.Amount + " " + stat.StatType);
                if (reforgedTo != null && stat.StatType == reforgedTo)
                {
                    stats.Append(" (Reforged from " + reforgedFrom.Value + ")");
                }
                stats.Append("\n");
            }
            itemInfoStats.Text = stats.ToString();
        }

        private void EquipmentPointerExited(object sender, PointerRoutedEventArgs e)
        {
            itemInfoFrame.Visibility = Visibility.Collapsed;

        }



    }





}
