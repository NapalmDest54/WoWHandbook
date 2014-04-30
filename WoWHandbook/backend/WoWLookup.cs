using BlizzAPI;
using BlizzAPI.WoW;
using BlizzAPI.WoW.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWHandbook.backend
{
    public class WoWLookup
    {
        private static WoWLookup instance = null;
        private static WoWClient client = null;

        private WoWLookup()
        {
            client = new WoWClient(new Region(BlizzAPI.Region.Regions.US));
        }

        public static WoWLookup getInstance()
        {
            if (instance == null)
            {
                instance = new WoWLookup();
            }

            return instance;
        }


        public Character getCharacter(String characterName, String server)
        {
            return client.getCharacter(server, characterName).Result;
        }
    }
}
