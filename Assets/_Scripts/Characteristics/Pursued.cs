
using System.Collections.Generic;
using UnityEngine;

public sealed class Pursued : Characteristic, ITargetable
{
    [SerializeField] private int maxNumOfPursuers = 2;

    private List<IPursuer> pursuers = new List<IPursuer>();


    public void TakeOff(IPursuer pursuer)
    {
        pursuers.Remove(pursuer);
        
        if (pursuers.Count == 0)
            TakeOff();

    }

    public void Join(IPursuer pursuer)
    {
        if (pursuers.Count < maxNumOfPursuers)
            pursuers.Add(pursuer);

    }

}

