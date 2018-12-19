using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    public float range = 15F;
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turretRotationSpeed = 0F;
    public float fireRate = 30F; // shots per minute
    private float fireCountdown = 0F;
    public GameObject bulletPrefab;
    public Transform firePoint;

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0F, 0.5F);
	}

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else target = null;
    }
	
	void Update () {
        if (target == null)
            return;

        //target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretRotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(partToRotate.rotation.x, rotation.y, 0F);

        if (fireCountdown <= 0)
        {
            Fire();
            fireCountdown = 60F / fireRate;
        }
        fireCountdown -= Time.deltaTime;
	}

    void Fire()
    {
        GameObject BulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = BulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
