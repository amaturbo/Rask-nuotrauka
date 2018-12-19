using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    public float speed = 20F;
    public GameObject impactEffect;
    public int damage;
    
    public void Seek(Transform _target)
    {
        target = _target;
    }

	// Update is called once per frame
	void Update () {
		if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

    void HitTarget()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 1F);
        Destroy(gameObject);
        Damage(target);
    }

    void Damage(Transform enemy)
    {
        EnemyHealth e = enemy.GetComponent<EnemyHealth>();

        if (e != null)
            e.takeDamage(damage);
    }
}
