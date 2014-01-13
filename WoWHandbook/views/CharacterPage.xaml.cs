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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WoWHandbook.views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharacterPage : Page
    {
        private Character character = null;

        public CharacterPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            character = WoWLookup.getInstance().getCharacter(e.Parameter as String, "area 52");
            characterTitle.Text = character.Name;
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

        private void StatsTextLoaded(object sender, RoutedEventArgs e)
        {

            /*
            StringBuilder sb = new StringBuilder();
            sb.Append("Level: ");
            sb.Append(character.Level);
            sb.AppendLine();
            sb.Append("Faction: ");
            sb.Append(character.Faction.ToString());
            sb.AppendLine();
            sb.Append("Race: ");
            sb.Append(character.Race.ToString());
            sb.AppendLine();
            sb.Append("Guild: ");
            sb.Append(character.Guild.Name);
            sb.AppendLine();
            sb.Append("Achievement Points: ");
            sb.Append(character.AchievementPoints);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Base\n");
            sb.Append("  Strength: " + character.Stats.Strength);
            sb.AppendLine();
            sb.Append("  Agility: " + character.Stats.Agility);
            sb.AppendLine();
            sb.Append("  Stamina: " + character.Stats.Stamina);
            sb.AppendLine();
            sb.Append("  Intellect: " + character.Stats.Intellect);
            sb.AppendLine();
            sb.Append("  Spirit: " + character.Stats.Spirit);
            sb.AppendLine();
            sb.Append("  Mastery: " + character.Stats.MasteryRating);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Spell\n");
            sb.Append("  Spell Power: " + character.Stats.Intellect);
            sb.AppendLine();
            sb.Append("  Haste: " + character.Stats.HasteRating);
            sb.AppendLine();
            sb.Append("  Hit: " + character.Stats.HitRating);
            sb.AppendLine();
            sb.Append("  Mana Regen: " + character.Stats.ManaPer5);
            sb.AppendLine();
            sb.Append("  Combat Regen: " + character.Stats.ManaPer5Combat);
            sb.AppendLine();
            sb.Append("  Crit: " + character.Stats.CritRating);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Defense\n");
            sb.Append("  Armor: " + character.Stats.Armor);
            sb.AppendLine();
            sb.Append("  Dodge: " + character.Stats.DodgeChance);
            sb.AppendLine();
            sb.Append("  Parry: " + character.Stats.ParryChance);
            sb.AppendLine();
            sb.Append("  Block: " + character.Stats.BlockChance);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Melee\n");
            sb.Append("  Attack Power: " + character.Stats.MeleeAttackPower);
            sb.AppendLine();
            sb.Append("  Haste: " + character.Stats.HasteRating);
            sb.AppendLine();
            sb.Append("  Hit: " + character.Stats.HitRating);
            sb.AppendLine();
            sb.Append("  Crit: " + character.Stats.CritRating);
            sb.AppendLine();
            sb.Append("  Expertise: " + character.Stats.ExpertiseRating);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Ranged\n");
            sb.Append("  Attack Power: " + character.Stats.RangedAttackPower);
            sb.AppendLine();
            sb.Append("  Haste: " + character.Stats.HasteRating);
            sb.AppendLine();
            sb.Append("  Hit: " + character.Stats.HitRating);
            sb.AppendLine();
            sb.Append("  Crit: " + character.Stats.CritRating);



            stats.Text = sb.ToString();
            //stats.Text = character.Thumbnail;
            */
        }

        private void TalentListViewLoaded(object sender, RoutedEventArgs e)
        {
            ListView talentList = (ListView)sender;

            character.Talents.ElementAt(0).ToString();

        }


        public Object findByName(String name)
        {
            List<Control> list = AllChildren(this.Frame);

            foreach (Control c in list)
            {
                if (c.Name == name)
                {
                    return c;
                }
            }

            return null;
        }



        public List<Control> AllChildren(DependencyObject parent)
        {
            var _List = new List<Control>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Control)
                    _List.Add(_Child as Control);
                _List.AddRange(AllChildren(_Child));
            }
            return _List;
        }




        private DependencyObject FindChildControl<T>(DependencyObject control, string ctrlName)
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                FrameworkElement fe = child as FrameworkElement;
                // Not a framework element or is null
                if (fe == null) return null;

                if (child is T && fe.Name == ctrlName)
                {
                    // Found the control so return
                    return child;
                }
                else
                {
                    // Not found it - search children
                    DependencyObject nextLevel = FindChildControl<T>(child, ctrlName);
                    if (nextLevel != null)
                        return nextLevel;
                }
            }
            return null;
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
                System.Diagnostics.Debug.WriteLine(spell.Name + "\t" + spell.Icon);
            }
            catch
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
            String sourceSting = "http://media.blizzard.com/wow/icons/56/";
            Image image = sender as Image;
            switch (image.Name)
            {
                case "imageHelm":
                    sourceSting += character.Items.Head.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageNeck":
                    sourceSting += character.Items.Neck.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageShoulder":
                    sourceSting += character.Items.Shoulder.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageBack":
                    sourceSting += character.Items.Back.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageChest":
                    sourceSting += character.Items.Chest.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageShirt":
                    //sourceSting += character.Items.Shirt.Icon + ".jpg";
                    //image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageTabard":
                    //sourceSting += character.Items.Tabard.Icon + ".jpg";
                    //image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageWrist":
                    sourceSting += character.Items.Wrist.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageGloves":
                    sourceSting += character.Items.Hands.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageWaist":
                    sourceSting += character.Items.Waist.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageLegs":
                    sourceSting += character.Items.Legs.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageFeet":
                    sourceSting += character.Items.Feet.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageFinger1":
                    sourceSting += character.Items.Finger1.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageFinger2":
                    sourceSting += character.Items.Finger2.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageTrinket1":
                    sourceSting += character.Items.Trinket1.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                case "imageTrinket2":
                    sourceSting += character.Items.Trinket2.Icon + ".jpg";
                    image.Source = new BitmapImage(new Uri(sourceSting, UriKind.Absolute));
                    break;
                default:
                    break;

            }
            
            

            //image
        }

        private void backButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.GoBack();
        }



    }





}
