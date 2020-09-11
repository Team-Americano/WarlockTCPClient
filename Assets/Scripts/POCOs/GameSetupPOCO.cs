using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.POCOs
{
    [Serializable]
    public class GameSetupPOCO : POCO
    {
        public string Player1Id;
        public string Player2Id;
    }
}
