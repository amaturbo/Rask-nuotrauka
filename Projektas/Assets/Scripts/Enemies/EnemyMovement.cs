using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    Transform player;
    NavMeshAgent nav;
    private Animator anim;
    bool isRunning = true;
    Transform myTransform;
    float nextAttack = 0;
    float timer = 0;

    PlayerController playerController;
    public float damage;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", isRunning);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        myTransform = transform;
    }
    
    void Update ()
    {
        if (nav.enabled == true)
            nav.SetDestination(player.position);
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + 1.15F;
            if (Vector3.Distance(myTransform.position, player.position) <= 7F)
            {
                tryToAttack();
            }
            else chase();
        }
	}

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }

    private void tryToAttack()
    {
        Vector3 lookVector = new Vector3(player.transform.position.x, myTransform.position.y, player.transform.position.z);
        myTransform.LookAt(lookVector);
                
        nav.isStopped = true;
        anim.SetBool("isRunning", false);
        anim.SetBool("isWoodcutting", true);
        Invoke("Attack", 0.6F);
    }

    private void chase()
    {
        nav.isStopped = false;
        anim.SetBool("isRunning", true);
        anim.SetBool("isWoodcutting", false);
    }

    /*private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            lastDamage = 0;
            nav.isStopped = true;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWoodcutting", true);
        }
    }*/

    /*private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            nav.isStopped = false;
            anim.SetBool("isRunning", true);
            anim.SetBool("isWoodcutting", false);
        }
    }*/

    public void Attack()
    {
            if (Vector3.Distance(myTransform.position, player.position) <= 7F)
                Damage();
    }

    private void Damage()
    {
        playerController.TakeDamage(damage);
    }

    /*private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (lastDamage >= 1.15)
            {
                Damage();
                lastDamage = 0;
            }
        }
    }*/

}
