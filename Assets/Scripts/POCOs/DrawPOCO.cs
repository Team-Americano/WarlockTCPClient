using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable] // I dont know if we need this
public class DrawPOCO : POCO
{
    public List<Actor> Hand;
    public short Round;
}

