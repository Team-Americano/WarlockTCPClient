using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class DraftPOCO : POCO
{
    public List<Actor> Party;
    public List<Actor> Hand;
    public short Mana;
}