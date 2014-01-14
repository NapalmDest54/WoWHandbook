﻿using WoWHandbook.backend;
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

        public CharacterPage()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           // String[] args = e.Parameter as String[];
           // character = WoWLookup.getInstance().getCharacter(args[0], args[1]);
            character = e.Parameter as Character;
            characterTitle.Text = character.Name;
           
        }

        private void baseStatsLoaded(object sender, RoutedEventArgs e)
        {
            return;
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
            return;
            ListView talentList = (ListView)sender;

            character.Talents.ElementAt(0).ToString();

        }


              
        private void TalentButtonLoaded(object sender, RoutedEventArgs e)
        {
            Button talentButton = sender as Button;

        }

        private void TalentButtonTextLoaded(object sender, RoutedEventArgs e)
        {
            if (1==1)
            {
                return;
            }
            TextBlock tb = sender as TextBlock;
            try
            {
                tb.Text = character.Talents[0].Build[Int32.Parse(tb.Name.Replace("talentText", "")) - 1].Spell.Name;
                Spell spell = character.Talents[0].Build[0].Spell;
                System.Diagnostics.Debug.WriteLine(spell.Name + "\t" + spell.Icon);
            }
            catch
            {
                tb.Text = "Empty";
            }
        }

        private void TalentImageLoaded(object sender, RoutedEventArgs e)
        {
            return;
            Image tb = sender as Image;
            try
            {
                //
                //tb.Source = new BitmapImage(new Uri("ms-appx:///Assets/" + character.Talents[0].Build[Int32.Parse(tb.Name.Replace("talentImage", "")) - 1].Spell.Icon + ".jpg"));
                String sourceString = "http://media.blizzard.com/wow/icons/56/";
                System.Diagnostics.Debug.WriteLine(sourceString);
                if (tb == null)
                {
                    System.Diagnostics.Debug.WriteLine("TB is null");
                }
                sourceString += character.Talents[0].Build[Int32.Parse(tb.Name.Replace("talentImage", "")) - 1].Spell.Icon + ".jpg";
                tb.Source = new BitmapImage(new Uri(sourceString, UriKind.Absolute));
                System.Diagnostics.Debug.WriteLine(sourceString);
            }
            catch (Exception exception)
            {

            }
        }

        private void CharacterImageLoaded(object sender, RoutedEventArgs e)
        {
            return;
            ImageBrush tb = (sender as Grid).Background as ImageBrush;

            try
            {
                String sourceString = "http://us.battle.net/static-render/us/" + character.Thumbnail.Replace("-avatar.jpg", "") + "-profilemain.jpg";
                System.Diagnostics.Debug.WriteLine(sourceString);
                if (tb == null)
                {
                    System.Diagnostics.Debug.WriteLine("TB is null");
                }
                
                tb.ImageSource = new BitmapImage(new Uri(sourceString, UriKind.Absolute));
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("Expcetion Image Loaded");
            }
        }

        private void itemSetLoaded(object sender, RoutedEventArgs e)
        {
            return;   
            Image image = sender as Image;
            try
            {
                switch (image.Name)
                {
                    case "imageHelm":
                        equippedItemHelper(image, character.Items.Head);
                        break;
                    case "imageNeck":
                        equippedItemHelper(image, character.Items.Neck);
                        break;
                    case "imageShoulder":
                        equippedItemHelper(image, character.Items.Shoulder);
                        break;
                    case "imageBack":
                        equippedItemHelper(image, character.Items.Back);
                        break;
                    case "imageChest":
                        equippedItemHelper(image, character.Items.Chest);
                        break;
                    case "imageShirt":
                        equippedItemHelper(image, character.Items.Shirt);
                        break;
                    case "imageTabard":
                        equippedItemHelper(image, character.Items.Tabard);
                        break;
                    case "imageWrist":
                        equippedItemHelper(image, character.Items.Wrist);
                        break;
                    case "imageGloves":
                        equippedItemHelper(image, character.Items.Hands);
                        break;
                    case "imageWaist":
                        equippedItemHelper(image, character.Items.Waist);
                        break;
                    case "imageLegs":
                        equippedItemHelper(image, character.Items.Legs);
                        break;
                    case "imageFeet":
                        equippedItemHelper(image, character.Items.Feet);
                        break;
                    case "imageFinger1":
                        equippedItemHelper(image, character.Items.Finger1);
                        break;
                    case "imageFinger2":
                        equippedItemHelper(image, character.Items.Finger2);
                        break;
                    case "imageTrinket1":
                        equippedItemHelper(image, character.Items.Trinket1);
                        break;
                    case "imageTrinket2":
                        equippedItemHelper(image, character.Items.Trinket2);
                        break;
                    case "imageMainHand":
                        equippedItemHelper(image, character.Items.MainHand);
                        break;
                    case "imageOffHand":
                        equippedItemHelper(image, character.Items.Offhand);
                        break;
                    default:
                        break;

                }
            }
            catch (NullReferenceException)
            {

            }
            
            

            //image
        }

        private void equippedItemHelper(Image image, EquippedItem item)
        {
            return;
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
            return;
            Image image = sender as Image;
            switch (image.Name)
            {
                case "imageHelm":
                    
                    itemInfoFrame.Visibility = Visibility.Visible;
                    var ttv = image.TransformToVisual(Window.Current.Content);
                    Point screenCoords = ttv.TransformPoint(new Point(0, 0));
                    //System.Diagnostics.Debug.WriteLine(e.GetCurrentPoint(MainGrid).Position.X);
                    System.Diagnostics.Debug.WriteLine(screenCoords.X);
                    Thickness margin = itemInfoFrame.Margin;
                    margin.Left = screenCoords.X + image.ActualWidth + 20;
                    margin.Top = screenCoords.Y;
                    itemInfoFrame.Margin = margin;

                    EquippedItem item = character.Items.Head;
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
                   

                    /*TextBlock textBlock = new TextBlock();
                    textBlock.Text = "TEST";
                    Thickness margin = textBlock.Margin;
                    margin.Left = 150;
                    margin.Top = 260;
                    textBlock.Margin = margin;
                    textBlock.Visibility = Visibility.Visible;
                    (FindName("MainGrid") as Grid).Children.Add(textBlock);
                    */
                    break;

                default:
                    break;
            }
        }

        private void EquipmentPointerExited(object sender, PointerRoutedEventArgs e)
        {
            itemInfoFrame.Visibility = Visibility.Collapsed;

        }



    }





}
