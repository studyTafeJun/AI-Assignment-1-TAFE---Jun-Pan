using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerAI : BaseAI
{

    [SerializeField] List<Rock> _rockList;
    [SerializeField] List<Transform> _rocks;

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
    }
}
