using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float startTime;
    public float lifeTime = 2f;
    public float speed = 10f;
    public float damage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Launch(float force, float damage){
        startTime = Time.time;
        this.damage = damage;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.5f)){
            if(hit.collider.gameObject.tag == "Enemy"){
                hit.collider.gameObject.SendMessage("TakeDamage", damage);
            }
        }
        
    }
}
