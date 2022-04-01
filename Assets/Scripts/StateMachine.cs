using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : BaseBuilding
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
    //setting the minumin amount of materials to rush
    [SerializeField] [Range(0,1)] float _materialRushThreshold = 0.8f;
    //maxinum amount of material to greed
    [SerializeField] [Range(0, 1)] float _materialGreedThreshold = 0.2f;
    //maxinum amount of health needed to go defence mode
    [SerializeField] [Range(0, 1)] float _hPDefenceThreshold = 0.5f;
    //set timed sends
    [SerializeField] float _timeToSend = 2f;

    [SerializeField] Text _stateText;
    public override void Start()
    {
        SwitchState();
    }
    void ChangeStateText() 
    {
        _stateText.text = "Current State: " + currentAiStates;    
    }
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
        Debug.Log("Ai is Attacking");
        ChangeStateText();
        while (currentAiStates == AiStates.Attack)
        {
            SendTroops(troop, 50, 200);
            if (materialAmount < maxMaterialAmount * _materialGreedThreshold)
            {
                currentAiStates = AiStates.Greed;
            }
            if (hP < _maxHP * _hPDefenceThreshold) 
            {
                currentAiStates = AiStates.Defence;
            }
            yield return new WaitForSeconds(_timeToSend);

            yield return null;
        }
        SwitchState();

    }
    IEnumerator Defence()
    {
        Debug.Log("Ai is Defending");
        ChangeStateText();
        while (currentAiStates == AiStates.Defence)
        {
            if (hP > _maxHP * _hPDefenceThreshold)
            {
                currentAiStates = AiStates.Attack;
            }
            yield return null;
        }
        SwitchState();
    }
    IEnumerator Greed()
    {
        Debug.Log("Ai is Greeding");
        ChangeStateText();
        while (currentAiStates == AiStates.Greed && materialAmount < maxMaterialAmount * _materialRushThreshold && hP > _maxHP * _hPDefenceThreshold)
        {
            SendTroops(miner, 0, 50);
            yield return new WaitForSeconds(_timeToSend);
        }
        if (hP < _maxHP * _hPDefenceThreshold)
        {
            currentAiStates = AiStates.Defence;
        }
        else
        {
            currentAiStates = AiStates.Attack;
        }
        SwitchState();
    }
}
