using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Player
    {
        public string PlayerId { get; set; }
        public List<Actor> Party { get; set; }
        public Actor[] TempParty { get; set; }
        public List<Actor> Hand { get; set; }
        public short Mana { get; set; }
        public short Score { get; set; }

        public Player()
        {
            TempParty = new Actor[6];
        }
    }
}
