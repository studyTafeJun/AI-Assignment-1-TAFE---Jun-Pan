using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBuilding : MonoBehaviour
{
    public float hP = 1000;
    [SerializeField] protected float _maxHP = 1000;
    //base amount of materials and energy
    public float materialAmount = 100;
    public float energyAmount = 300;

    //max amount of materials and energy
    [SerializeField] protected float maxMaterialAmount = 300;
    [SerializeField] protected float _hPRegenRate = 20;
    //whenever building gets damaged, regen recieves a downtime
    [SerializeField] protected float _hPRegenCooldown = 5;
    [SerializeField] protected float _currentHpCooldown = 0;
    //
    [SerializeField] protected float _previousHp;

    [SerializeField] protected float maxEnergyAmount = 1000;

    //the base rate for generating energy
    [SerializeField] protected float _energyGenerationRate = 1;
    [SerializeField] protected Text _hPText;
    public bool isDead = false;
    //the three things, miner, troops and turrets(turrets are not yet implemented)
    public GameObject miner;
    public GameObject troop;
    public GameObject turret;

    
    public virtual void Start()
    {
        _previousHp = hP;
        if (_hPText != null) {
            ChangeHp();
        }
    }
    public void Update()
    {
        energyAmount += _energyGenerationRate * Time.deltaTime;
    }
    void ChangeHp() 
    {
        _hPText.text = hP.ToString("n0");
    }
    public void SendTroops(GameObject troops, float matCost, float energyCost) 
    {
        if (materialAmount >= matCost && energyAmount >= energyCost) {
            Debug.Log("Troop or miner should be summoned");
            //spawns enemies and set ownerBuilding to the base that summoned it
            GameObject troop = Instantiate(troops, transform.position, Quaternion.identity);
            BaseAI troopAi = troop.GetComponent<BaseAI>();
            troopAi.ownerBuilding = gameObject;
            //reduces amount of energy and material
            materialAmount -= matCost;
            energyAmount -= energyCost;
        } else 
        {
            Debug.Log("You can't afford it");
        }
    }
    IEnumerator Repair() 
    {

        yield return null;
    }
}
