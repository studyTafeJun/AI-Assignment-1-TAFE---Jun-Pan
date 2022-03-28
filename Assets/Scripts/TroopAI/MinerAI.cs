using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerAI : BaseAI
{
    [SerializeField] int _miningStrength = 1;
    [SerializeField] float _miningDelay = 1;
    [SerializeField] float _maxCarryLoad = 5;

    [SerializeField] int _heldMaterials = 0;
    [SerializeField] List<Rock> _rockList;
    [SerializeField] List<Transform> _rocks;
    public enum MinerStates 
    { 
        SeekRocks,
        MineRock,
        Return,
        Die
    }
    [SerializeField] MinerStates currentMinerState;

    private void Start()
    {
        //finds every object containing the rock script
        _rockList.AddRange(FindObjectsOfType<Rock>());
        Debug.Log(_rockList[0]);
        //
        foreach (Rock rocks in _rockList) 
        {
            _rocks.AddRange(rocks.GetComponents<Transform>());
        }
        SwitchStates();
    }
    void SwitchStates()
    {
        switch (currentMinerState)
        {
            case MinerStates.SeekRocks:
                StartCoroutine(SeekRocks());
                break;
            case MinerStates.MineRock:
                StartCoroutine(MineRock());
                break;
            case MinerStates.Return:
                StartCoroutine(Return());
                break;
        }
    }
    IEnumerator SeekRocks() 
    {
        Debug.Log("Seeking Rocks");
        currentTarget = seekClosest(_rocks);
        while (currentMinerState == MinerStates.SeekRocks && currentTarget != null)
        {   
            if (Vector2.Distance(transform.position, currentTarget.position) < stoppingDistance)
            {
                currentMinerState = MinerStates.MineRock;
            }
            moveTowards();
            yield return null;
        }
        SwitchStates();
    }
    IEnumerator MineRock() 
    {
        Debug.Log("Mining Rocks");
        Rock minedRock = currentTarget.gameObject.GetComponent<Rock>();
        Debug.Log(minedRock.materialAmount);
        while (currentMinerState == MinerStates.MineRock && _heldMaterials < _maxCarryLoad/*minedRock.materialAmount > 0*/)
        {
            //minedRock.materialAmount -= _miningStrength;
            _heldMaterials += _miningStrength;
            Debug.Log("Rock--");
            yield return new WaitForSeconds(_miningDelay);
        }
        currentMinerState = MinerStates.Return;
        SwitchStates();
    }
    IEnumerator Return() 
    {
        Debug.Log("Returning to base");
        currentTarget = ownerBuilding.transform;
        while (currentMinerState == MinerStates.Return)
        {
            moveTowards();
            if (Vector2.Distance(transform.position, currentTarget.position) < stoppingDistance) 
            {
                ownerBuilding.GetComponent<BaseBuilding>().materialAmount += _heldMaterials;
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
