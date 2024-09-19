using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damage = 10f;

    public float forceX;
    public float forceY;
    public float duration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerStats"))
        {
            Debug.Log("Player hit by spikes");
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
            PlayerMoveControls playerMoveControls = collision.GetComponentInParent<PlayerMoveControls>();

            StartCoroutine(playerMoveControls.KnockBack(forceX, forceY,duration, transform));
        }
    }
}
