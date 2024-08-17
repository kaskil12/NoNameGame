using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public bool isEquiped = false;
    public float damage = 10f;
    public float range = 2f;
    public float attackSpeed = 1f;
    public float attackCooldown = 0f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if(isEquiped)
        {
            if(Input.GetMouseButtonDown(0) && attackCooldown == 0)
            {
                animator.SetTrigger("Attack");
                Debug.Log("Attacking");
                attackCooldown = 1;
                StartCoroutine(Attack());
            }
            if(attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }
        }
    }
    public void Equip()
    {
        animator.SetTrigger("Equip");
        StartCoroutine(EquipDelay());
    }
    IEnumerator EquipDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isEquiped = true;
    }
    public void Unequip()
    {
        isEquiped = false;
    }
    public void HitCheck()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.tag == "Stone")
            {
                hit.transform.gameObject.SendMessage("TakeDamage", damage);
                
            }
        }
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.05f);
        HitCheck();
        yield return new WaitForSeconds(attackSpeed);
        attackCooldown = 0;
    }
}
