using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.POCOs
{
    [Serializable]
    public class GameStatePOCO : POCO
    {
        public short RoundCounter;
        public List<Actor> Player1Party;
        public string Player1Id;
        public List<Actor> Player2Party;
        public string Player2Id;
    }
}
