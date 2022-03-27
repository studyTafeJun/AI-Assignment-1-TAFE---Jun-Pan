using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBuilding : MonoBehaviour
{
    [SerializeField] protected float _hP = 1000;
    [SerializeField] protected float _maxHP = 1000;
    [SerializeField] protected Text _hPText;
    public bool isDead = false;

    public virtual void Start()
    {
        if (_hPText != null) {
            ChangeHp();
        }
    }

    void ChangeHp() 
    {
        _hPText.text = _hP.ToString("n0");
    }
    public void SendTroops(GameObject troops) 
    {
        GameObject troop = Instantiate(troops, transform);
    }
}
