using BlizzAPI.WoW.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace WoWHandbook.Common.Backend
{
    public class ColorLookup
    {
        public static readonly Dictionary<ItemQuality, Color> colorLookup = new Dictionary<ItemQuality, Color>()
        {
                {ItemQuality.POOR, Colors.Gray},
                {ItemQuality.COMMON, Colors.White},
                {ItemQuality.UNCOMMON, Colors.Green},
                {ItemQuality.RARE, Colors.Blue},
                {ItemQuality.EPIC, ColorHelper.FromArgb(255, 205, 0, 255)},
                {ItemQuality.LEGENDARY, Colors.Orange},
                {ItemQuality.ARTIFACT, Colors.Gold},
                {ItemQuality.HEIRLOOM, Colors.Gold}
        };
    }
}
