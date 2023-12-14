using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform currentTarget;
    public int damage;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        if (currentTarget == null)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Enemy") && other.gameObject == currentTarget.gameObject)
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
