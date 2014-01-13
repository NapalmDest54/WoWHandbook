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
            client = new WowClient(Region.US);
        }

        public static WoWLookup getInstance()
        {
            if (instance == null)
            {
                instance = new WoWLookup();
            }

            return instance;
        }


        public int getLevel(String characterName, String server)
        {
            Character character = null;
            //try
           // {
            character = client.GetCharacter("area 52", characterName, true);
           // }
           // catch (Exception e)
           // {
               // System.Diagnostics.Debug.WriteLine(e.Message);
          //  }

            if (character == null)
                return 0;
            return character.Level;
        }




        public Character getCharacter(String characterName, String server)
        {
            return client.GetCharacter("area 52", characterName, true);
        }
    }
}
