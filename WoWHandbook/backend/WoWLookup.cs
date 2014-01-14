using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOWSharp.Community;
using WOWSharp.Community.Wow;

namespace WoWHandbook.backend
{
    public class WoWLookup
    {
        private static WoWLookup instance = null;
        private static WowClient client = null;

        private WoWLookup()
        {
            client = new WowClient(Region.EU);
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
            return client.GetCharacterAsync("kazzak", "Grendiser", CharacterFields.All).Result;
        }
    }
}
