using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour, IDamage
{
    public Transform posGun;
    public Transform cam;
    public LayerMask ignoreLayer;

    RaycastHit hit;

    private void Update()
    {
        Debug.DrawRay(cam.position, cam.forward * 100f, Color.red);
        Debug.DrawRay(posGun.position, cam.forward * 100f, Color.blue);

        if (Input.GetMouseButtonDown(0))
        {

            //Vector3 direction = cam.TransformDirection(new Vector3(Random.Range(-0.05f, 0.05f), 1));
            Vector3 direction = cam.TransformDirection(new Vector3(0, 0, 1));
            //GameObject bulletObj = Instantiate(bulletPrefab);
            GameObject bulletObj = ObjectPollingManager.instance.GetBullet();

            bulletObj.transform.position = posGun.position;
            if(Physics.Raycast(cam.position, direction, out hit, Mathf.Infinity, ~ignoreLayer))
            {
                bulletObj.transform.LookAt(hit.point);
            }
            else
            {
                //Vector3 dir = cam.position + cam.forward * 10f;
                Vector3 dir = cam.position + direction * 10f;
                bulletObj.transform.LookAt(dir);
            }
            
        }
    }

    public void DoDamage(int vld)
    {
        Debug.Log("HE RECIBIDO DAÑO = " + vld);
    }
}
