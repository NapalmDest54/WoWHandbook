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
using Windows.UI.Xaml.Documents;

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
        private ConcurrentDictionary<String, EquippedItem> equippedItemDictionary;
        private ConcurrentDictionary<EquippedItem, Item> itemDictionary;
        private string[] itemImageNames = { "imageHead", "imageNeck", "imageShoulder", "imageBack", "imageChest", "imageShirt", "imageTabard", "imageWrist", "imageHands", "imageWaist", "imageLegs",
                                          "imageFeet", "imageFinger1", "imageFinger2", "imageTrinket1", "imageTrinket2", "imageMainHand", "imageOffHand" };
        private WowClient client;
        public CharacterPage()
        {
            equippedItemDictionary = new ConcurrentDictionary<string, EquippedItem>();
            itemDictionary = new ConcurrentDictionary<EquippedItem, Item>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            String[] args = e.Parameter as String[];
            createCharacter(args[0], args[1]);
        }

        private async void createCharacter(String characterName, String realm)
        {
            client = new WowClient(Region.US);
            character = await client.GetCharacterAsync(realm, characterName, CharacterFields.All);

            equippedItemDictionary.TryAdd("imageHead", character.Items.Head);
            equippedItemDictionary.TryAdd("imageNeck", character.Items.Neck);
            equippedItemDictionary.TryAdd("imageShoulder", character.Items.Shoulder);
            equippedItemDictionary.TryAdd("imageBack", character.Items.Back);
            equippedItemDictionary.TryAdd("imageChest", character.Items.Chest);
            equippedItemDictionary.TryAdd("imageShirt", character.Items.Shirt);
            equippedItemDictionary.TryAdd("imageTabard", character.Items.Tabard);
            equippedItemDictionary.TryAdd("imageWrist", character.Items.Wrist);
            equippedItemDictionary.TryAdd("imageHands", character.Items.Hands);
            equippedItemDictionary.TryAdd("imageWaist", character.Items.Waist);
            equippedItemDictionary.TryAdd("imageLegs", character.Items.Legs);
            equippedItemDictionary.TryAdd("imageFeet", character.Items.Feet);
            equippedItemDictionary.TryAdd("imageFinger1", character.Items.Finger1);
            equippedItemDictionary.TryAdd("imageFinger2", character.Items.Finger2);
            equippedItemDictionary.TryAdd("imageTrinket1", character.Items.Trinket1);
            equippedItemDictionary.TryAdd("imageTrinket2", character.Items.Trinket2);
            equippedItemDictionary.TryAdd("imageMainHand", character.Items.MainHand);
            equippedItemDictionary.TryAdd("imageOffHand", character.Items.Offhand);
            this.InitializeComponent();
            characterTitle.Text = character.Name + "  -  " + character.Realm;

            foreach (KeyValuePair<String, EquippedItem> entry in equippedItemDictionary)
            {
                if (entry.Value == null)
                {
                    continue;
                }
                Item item = await client.GetItemAsync(entry.Value.ItemId);
                itemDictionary.TryAdd(entry.Value, item);
            }



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

                EquippedItem eq;
                equippedItemDictionary.TryGetValue(image.Name, out eq);
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
            EquippedItem eq;
            equippedItemDictionary.TryGetValue(image.Name, out eq);
            equipmentPopupHelper(image, eq, image.Name.Replace("image", ""));
        }

        private void equipmentPopupHelper(Image image, EquippedItem equipedItem, String slot)
        {
            if (equipedItem == null)
                return;

            Item detailedItem;
            itemDictionary.TryGetValue(equipedItem, out detailedItem);


            itemInfoName.Text = equipedItem.Name;
            SolidColorBrush solidColorBrush;
            switch (equipedItem.Quality)
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
            itemInfoItemLevel.Text = "Item Level " + equipedItem.ItemLevel;
            try
            {
                itemInfoUpgradeLevel.Text = "Upgrade Level: " + equipedItem.Parameters.Upgrade.Current + "/" + equipedItem.Parameters.Upgrade.Total;
                itemInfoUpgradeLevel.Visibility = Visibility.Visible;
            }
            catch (NullReferenceException)
            {
                itemInfoUpgradeLevel.Visibility = Visibility.Collapsed;
            }

            if (detailedItem != null)
            {
                String bindType = null;
                switch (detailedItem.BindType)
                {
                    case ItemBindType.BindOnEquipped:
                        bindType = "Binds on Equip";
                        break;
                    case ItemBindType.BindOnPickup:
                        bindType = "Binds on Pickup";
                        break;
                    case ItemBindType.BindOnUse:
                        bindType = "Binds on Use";
                        break;
                    case ItemBindType.BindToAccount:
                        bindType = "Binds on Account";
                        break;
                    case ItemBindType.Unbound:
                        bindType = "Unbound";
                        break;
                    default:
                        break;
                }
                if (bindType == null)
                {
                    itemInfoBinding.Visibility = Visibility.Collapsed;
                }
                else
                {
                    itemInfoBinding.Visibility = Visibility.Visible;
                    itemInfoBinding.Text = bindType;
                }

                itemInfoDescription.Text = "";
                foreach (ItemSpell spell in detailedItem.ItemSpells)
                {
                    itemInfoDescription.Text += spell.Spell.Description + "\n\n";
                }
                if (itemInfoDescription.Text == "")
                {
                    itemInfoDescription.Visibility = Visibility.Collapsed;
                }


                else
                {
                    itemInfoDescription.Text.Trim();
                    itemInfoDescription.Visibility = Visibility.Visible;
                }
                //itemInfoDescription.Text = detailedItem.ItemSpells.ElementAt(0).Spell.Description;


                itemInfoSlot.Text = slot;
                if (equipedItem.Armor > 0)
                {
                    itemInfoArmor.Text = equipedItem.Armor + " Armor";
                    itemInfoArmor.Visibility = Visibility.Visible;
                }
                else
                {
                    itemInfoArmor.Visibility = Visibility.Collapsed;
                }



                if (detailedItem.ItemSet != null)
                {
                    itemInfoSetBonus.Visibility = Visibility.Visible;
                    itemInfoSetBonus.Blocks.Clear();

                    
                    foreach (ItemSetBonus bonus in detailedItem.ItemSet.Bonuses)
                    {
                        int itemSetCount = 0;
                        foreach (KeyValuePair<EquippedItem, Item> entry in itemDictionary)
                        {
                            if (entry.Value.ItemSet != null && entry.Value.ItemSet.Id == detailedItem.ItemSet.Id)
                            {
                                itemSetCount++;
                            }
                        }

                        
                        Run bonusText = new Run();
                        if (itemSetCount < bonus.Threshold)
                        {
                            bonusText.Foreground = new SolidColorBrush(Windows.UI.Colors.Gray);
                        }
                        bonusText.FontSize = 14;
                        bonusText.Text = "Set (" + bonus.Threshold + "): " +bonus.Description + "\n";

                        // Create a paragraph and add the Run and Bold to it.
                        Paragraph myParagraph = new Paragraph();
                        myParagraph.Inlines.Add(bonusText);
                        itemInfoSetBonus.Blocks.Add(myParagraph);
                    }
                }
                else
                {
                    itemInfoSetBonus.Visibility = Visibility.Collapsed;
                }
            } // END IF detailedInfo != null

            StringBuilder stats = new StringBuilder();
            ItemStatType? reforgedFrom = equipedItem.Parameters.ReforgedToStat;
            ItemStatType? reforgedTo = equipedItem.Parameters.ReforgedFromStat;
            for (int i = 0; i < equipedItem.Stats.Count; i++)
            {
                ItemStat stat = equipedItem.Stats.ElementAt(i);
                stats.Append("+" + stat.Amount + " " + stat.StatType.ToString().Replace("Rating", ""));
                if (reforgedTo != null && stat.StatType == reforgedTo)
                {
                    stats.Append(" (Reforged from " + reforgedFrom.Value.ToString().Replace("Rating", "") + ")");
                }
                stats.Append("\n");
            }
            itemInfoStats.Text = stats.ToString();
            itemInfoFrame.Visibility = Visibility.Visible;
            //itemInfoFrame.InvalidateMeasure();
            itemInfoFrame.UpdateLayout();
            var ttv = image.TransformToVisual(Window.Current.Content);
            Point screenCoords = ttv.TransformPoint(new Point(0, 0));
            Thickness margin = itemInfoFrame.Margin;
            if (screenCoords.Y + itemInfoFrame.RenderSize.Height + 15 >= Window.Current.Bounds.Height)
            {
                margin.Top = screenCoords.Y + image.RenderSize.Height - itemInfoFrame.RenderSize.Height;

            }
            else
            {

                margin.Top = screenCoords.Y;
            }

            if (screenCoords.X + itemInfoFrame.ActualWidth >= characterItemsSection.RenderSize.Width)
            {
                margin.Left = screenCoords.X - itemInfoFrame.ActualWidth - 20;
            }
            else
            {
                margin.Left = screenCoords.X + image.ActualWidth + 20;
            }


            itemInfoFrame.Margin = margin;
        }

        private void EquipmentPointerExited(object sender, PointerRoutedEventArgs e)
        {
            itemInfoFrame.Visibility = Visibility.Collapsed;

        }



    }





}
