using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAI : BaseAI
{
    [SerializeField] float _damage = 2;
    [SerializeField] float _swingDelay = 1;
    public enum WarriorStates 
    { 
        MoveToAttackBase,
        DamageBase,
        AttackTroops,
    }
    [SerializeField] WarriorStates _currentWarriorState;

    Transform _oppositeBase;

    private void Start()
    {
        if (ownerBuilding.GetComponent<StateNachine>()) 
        {
            Player _buildingScripted = FindObjectOfType<Player>();
            _oppositeBase = _buildingScripted.GetComponent<Transform>();
        } else if (ownerBuilding.GetComponent<Player>()) 
        {
            StateNachine _buildingScripted = FindObjectOfType<StateNachine>();
            _oppositeBase = _buildingScripted.GetComponent<Transform>();
        }
        SwitchStates();
    }
    void SwitchStates()
    {
        switch (_currentWarriorState)
        {
            case WarriorStates.MoveToAttackBase:
                StartCoroutine(MoveToAttackBase());
                break;
            case WarriorStates.DamageBase:
                StartCoroutine(DamageBase());
                break;
            case WarriorStates.AttackTroops:
                StartCoroutine(AttackTroops());
                break;
        }
    }
    IEnumerator MoveToAttackBase() 
    {
        Debug.Log("Breaking to: " + _oppositeBase);
        currentTarget = _oppositeBase;
        while (_currentWarriorState == WarriorStates.MoveToAttackBase)
        {
            if (Vector2.Distance(transform.position, currentTarget.position) < stoppingDistance)
            {
                _currentWarriorState = WarriorStates.DamageBase;
            }
            moveTowards();
            yield return null;
        }
        SwitchStates();
    }
    IEnumerator DamageBase() 
    {
        Debug.Log("Stricking oppenent's base");
        while (_currentWarriorState == WarriorStates.DamageBase)
        {
            currentTarget.GetComponent<BaseBuilding>().hP -= _damage;
            yield return new WaitForSeconds(_swingDelay);
        }
    }
    IEnumerator AttackTroops() 
    {
        BaseAI currentBaseAI = currentTarget.GetComponent<BaseAI>();
        while (_currentWarriorState == WarriorStates.AttackTroops && currentTarget != null)
        {
            if (Vector2.Distance(transform.position, currentTarget.position) < stoppingDistance)
            {
                currentBaseAI.hP -= _damage;
                yield return new WaitForSeconds(_swingDelay);
            }
            moveTowards();
            yield return null;
        }
        _currentWarriorState = WarriorStates.MoveToAttackBase;
        SwitchStates();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning(collision);
        if (collision.GetComponent<BaseAI>().ownerBuilding != ownerBuilding) 
        {
            _currentWarriorState = WarriorStates.AttackTroops;
            currentTarget = collision.transform;
        }
    }
}
