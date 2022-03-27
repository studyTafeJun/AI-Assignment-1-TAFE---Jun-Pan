using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statenachine : MonoBehaviour
{
    public enum AiStates
    { 
        Attack,
        Defence,
        Greed,
        Flee
    }
    AiStates currentAiStates;
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
            case AiStates.Flee:
                StartCoroutine(Flee());
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

        }
        SwitchState();
        yield return null;
    }
    IEnumerator Flee()
    {
        while (currentAiStates == AiStates.Flee)
        {

        }
        SwitchState();
        yield return null;
    }
}
