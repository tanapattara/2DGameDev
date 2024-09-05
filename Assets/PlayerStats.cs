using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    private bool canTakeDamage = true;

    void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        if(!canTakeDamage)
        {
            return;
        }

        health -= damage;
        if(health <= 0)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInParent<GatherInput>().DisableControls();
            Debug.Log("Player is dead");
        }
        StartCoroutine(DamagePrevention());
    }
    private IEnumerator DamagePrevention() 
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.15f);
        if (health > 0)
        {
            canTakeDamage = true;
        }
        else { }
    }
}
