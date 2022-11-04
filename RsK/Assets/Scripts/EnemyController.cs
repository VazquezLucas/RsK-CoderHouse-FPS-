using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamage
{
    public Transform target;

    public void DoDamage(int vld)
    {
        Debug.Log("HE RECIBIDO DAÑO = " + vld);
    }

    void Update()
    {
        Vector3 posNoRot = new Vector3(target.position.x, 0.0f, target.position.z);
        transform.LookAt(posNoRot);
    }
}
