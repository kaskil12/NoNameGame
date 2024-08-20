using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour
{
    public Transform ShootPoint;
    public GameObject BulletPrefab;
    public float BulletForce = 20f;
    public float ShootRate = 0.5f;
    public bool isEquipped;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isEquipped){
            if(Input.GetButtonDown("Fire1")){
                Shoot();
            }
        }
    }
    public void Shoot(){
        GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position, ShootPoint.rotation);
        bullet.SendMessage("Launch", BulletForce, Damage);
    }
    public void Equip(){
        isEquipped = true;
    }
    public void Unequip(){
        isEquipped = false;
    }
}
