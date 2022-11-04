using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamage
{
    [SerializeField] private Transform target;

    public Transform weapon;

    public float shootDistance = 10f;
    public float shootInterval = 2f;
    float shootTime;
    float distanceToTarget;

    public int life = 5;
    NavMeshAgent agent;

    public float distanceToChase = 3f;
    public float chaseInterval = 2f;
    float chaseTime;

    public ParticleSystem particle;
    WaitForSeconds wait;

    void Start()
    {
        agent =GetComponent<NavMeshAgent>();
        
        shootTime = shootInterval;
        chaseTime = chaseInterval;
        wait = new WaitForSeconds(particle.main.duration);
    }
    public bool DoDamage(int vld, bool isPlayer)
    {
        Debug.Log("HE RECIBIDO DAÑO = " + vld + "isPlayer = " + isPlayer);
        if(isPlayer == true)
        {
            life -= vld;
            if(life <= 0)
            {
              StartCoroutine  (Die());
            }
            return true;
        }
        return false;
    }

    IEnumerator Die()
    {
        particle.Play();
        yield return wait;
        GameManager.instance.AddEnemyKill();
        Destroy(gameObject);
    }

    void Update()
    {
        Vector3 posNoRot = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(posNoRot);
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        Chase();

        ShootControl();
    }
    void ShootControl()
    {
        shootTime -= Time.deltaTime;
        if (shootTime < 0)
        {
            if (distanceToTarget < shootDistance)
            {
                shootTime = shootInterval;
                GameObject bullet = ObjectPollingManager.instance.GetBullet(false);
                bullet.transform.position = weapon.position;
                bullet.transform.LookAt(target.position);
            }
        }
    }
    void Chase()
    {
        chaseTime -= Time.deltaTime;
        if (chaseTime < 0)
        {
            if (distanceToTarget > distanceToChase)
            {
                agent.SetDestination(target.position);
                agent.stoppingDistance = distanceToChase;
                chaseTime = chaseInterval;
            }
        }
    }
}
