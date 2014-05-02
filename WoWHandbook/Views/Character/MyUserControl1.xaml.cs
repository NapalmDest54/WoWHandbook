using BlizzAPI.WoW.character.items;
using BlizzAPI.WoW.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WoWHandbook.Common.Backend;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace WoWHandbook.Views.Character
{
    public sealed partial class MyUserControl1 : UserControl
    {
        public MyUserControl1()
        {
            this.InitializeComponent();
        }

        internal void loadFromItem(Item item, EquippedItem equippedItem)
        {
            Color color = Colors.White;
            ColorLookup.colorLookup.TryGetValue(equippedItem.Quality, out color);

            itemInfoName.Text = item.Name;
            itemInfoName.Foreground = new SolidColorBrush(color);

            itemInfoItemLevel.Text = "Item Level " + ((equippedItem == null) ? item.ItemLevel.ToString() : equippedItem.ItemLevel.ToString());
            itemInfoUpgradeLevel.Text = (item.Upgradable) ? (equippedItem == null) ? "Upgrade Level 0/2" : "Upgrade Level " + equippedItem.TooltipParams.Upgrade.Current + "/" + equippedItem.TooltipParams.Upgrade.Total : "Not Upgradable";

            itemInfoBinding.Text = "Binding " + item.ItemBind.ToString();

            itemInfoSlot.Text = "Slot " + "NEED TO ADD";

            itemInfoArmor.Text = item.Armor.ToString() + " Armor";

            itemInfoDescription.Text = item.Description;

            StringBuilder stats = new StringBuilder();
            var reforgedFrom = equippedItem.TooltipParams.Reforge;
            var statsItem = equippedItem.Stats.OrderBy(x => x.StatType).ToList();

            foreach (ItemStat stat in statsItem)
            {
                stats.Append("+");
                stats.Append(stat.Amount.ToString());
                stats.Append(" ");
                stats.Append(stat.StatType.ToString().Replace("Rating", ""));
                if (stat.StatType == equippedItem.TooltipParams.ReforgedToStat)
                {
                    stats.Append(" (Reforged from ");
                    stats.Append(equippedItem.TooltipParams.ReforgedFromStat.ToString());
                    stats.Append(")");
                }
                stats.AppendLine();
            }
            Debug.WriteLine(reforgedFrom.ToString());
            itemInfoStats.Text = stats.ToString();

        }


    }
}
