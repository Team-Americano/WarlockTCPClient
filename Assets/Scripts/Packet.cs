using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Packet
{
    public short CommandId;
    public string PlayerId;
    public string POCOJson;
}
