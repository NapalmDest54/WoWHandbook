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

            itemInfoName.Text = item.name;
            itemInfoName.Foreground = new SolidColorBrush(color);

            itemInfoItemLevel.Text = "Item Level " + ((equippedItem == null) ? item.itemLevel.ToString() : equippedItem.ItemLevel.ToString());
            itemInfoUpgradeLevel.Text = (equippedItem == null) ? item.upgradable ? "Upgradeable" : "Not-Upgradeable" : "Upgrade Level: " + equippedItem.TooltipParams.Upgrade.Current + "/" + equippedItem.TooltipParams.Upgrade.Total;

            itemInfoBinding.Text = "Binding " + item.itemBind.ToString();

            itemInfoSlot.Text = "Slot " + "NEED TO ADD";

            itemInfoArmor.Text = item.armor.ToString() + " Armor";

            itemInfoDescription.Text = item.description;

            StringBuilder stats = new StringBuilder();
            var reforgedFrom = equippedItem.TooltipParams.Reforge;
            var statsItem = equippedItem.Stats;
            foreach (ItemStat stat in statsItem)
            {
                stats.Append("+");
                stats.Append(stat.StatType.ToString().Replace("Rating", ""));
                if (stat.StatType == (BlizzAPI.WoW.Items.ItemStats.ItemStatType)reforgedFrom)
                {
                    //equippedItem.TooltipParams.
                }
                stats.Append(" ");
                stats.Append(stat.Amount.ToString());
                stats.AppendLine();
            }
            Debug.WriteLine(reforgedFrom.ToString());
            itemInfoStats.Text = stats.ToString();

        }
    }
}
