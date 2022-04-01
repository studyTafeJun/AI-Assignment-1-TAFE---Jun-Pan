using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour
{
    public float hP = 10f;
    [SerializeField] float _speed = 1f;
    //this is for preventing glitchy movements
    public float stoppingDistance = 0.05f;

    public Transform currentTarget;
    public GameObject ownerBuilding;


    public void Update()
    {
        if (hP <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void moveTowards()
    {
        //if (Vector2.Distance(transform.position, currentTarget.position) > stoppingDistance) {
        if (currentTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, _speed * Time.deltaTime);
        }
        //}
    }
    public Transform seekClosest(List<Transform> objectList) 
    {
        float currentClosestDistance = float.PositiveInfinity;
        Transform currentClosestTarget = null;
        foreach (Transform objectEntry in objectList) 
        { 
            if (Vector2.Distance(transform.position, objectEntry.position) < currentClosestDistance) 
            {
                currentClosestDistance = Vector2.Distance(transform.position, objectEntry.position);
                currentClosestTarget = objectEntry;
            }

            //Debug.Log(currentClosestTarget);
            //Debug.Log(currentClosestDistance);
        }
        return currentClosestTarget;
    }
}
