using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseBuilding
{
    public void SendWarrior()
    {
        SendTroops(troop, 50, 200);
    }
    public void SendMiner()
    {
        SendTroops(miner, 0, 50);
    }
}
