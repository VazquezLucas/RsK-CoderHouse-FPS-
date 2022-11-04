using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour, IDamage
{
    public Transform posGun;
    public Transform cam;
    
    public LayerMask ignoreLayer;
    RaycastHit hit;
    public int life = 20;

    public GameObject damageEffect;
    public float saveInterval = 0.5f;
    float saveTime;
    WaitForSeconds wait;

    private void Start()
    {
        damageEffect.SetActive(false);
        saveTime = 0.0f;
        wait = new WaitForSeconds(0.2f);
    }

    private void Update()
    {
        Debug.DrawRay(cam.position, cam.forward * 100f, Color.red);
        Debug.DrawRay(posGun.position, cam.forward * 100f, Color.blue);

        if (Input.GetMouseButtonDown(0))
        {

            //Vector3 direction = cam.TransformDirection(new Vector3(Random.Range(-0.05f, 0.05f), 1));
            Vector3 direction = cam.TransformDirection(new Vector3(0, 0, 1));
            //GameObject bulletObj = Instantiate(bulletPrefab);
            GameObject bulletObj = ObjectPollingManager.instance.GetBullet(true);

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
        saveTime -= Time.deltaTime;

    }

    public bool DoDamage(int vld, bool isPlayer)
    {
        Debug.Log("HE RECIBIDO DAÑO = " + vld + "isPlayer = " + isPlayer);
        if (isPlayer == true) return false;
        else
        {
            if (saveTime <= 0)
            {
                life -= vld;
                StartCoroutine(Effect());
            }
            
        }

        return true;
    }
    IEnumerator Effect()
    {
        saveTime = saveInterval;
        damageEffect.SetActive(true);
        yield return wait;
        damageEffect.SetActive(false);
        if(life <= 0)
        {
            GameManager.instance.FinGame(false);
        }
    }
}
