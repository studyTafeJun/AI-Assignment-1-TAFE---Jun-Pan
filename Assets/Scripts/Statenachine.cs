using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNachine : BaseBuilding
{
    //declaring enum
    public enum AiStates
    { 
        Attack,
        Defence,
        Greed,
    }
    //assigning allows enum to be used by outsiders
    [SerializeField] AiStates currentAiStates;

    [SerializeField] GameObject _miner;
    [SerializeField] GameObject _troop;
    [SerializeField] GameObject _turret;


    void SwitchState()
    {
        switch (currentAiStates) 
        {
            case AiStates.Attack:
                StartCoroutine(Attack());
                break;
            case AiStates.Defence:
                StartCoroutine(Defence());
                break;
            case AiStates.Greed:
                StartCoroutine(Greed());
                break;
        }
    }

    IEnumerator Attack() 
    {
        yield return null;
        while (currentAiStates == AiStates.Attack) 
        { 
            
        }
        SwitchState();

    }
    IEnumerator Defence()
    {
        while (currentAiStates == AiStates.Defence)
        {

        }
        SwitchState();
        yield return null;
    }
    IEnumerator Greed()
    {
        while (currentAiStates == AiStates.Greed)
        {
            SendTroops(_miner);
        }
        SwitchState();
        yield return null;
    }
}
