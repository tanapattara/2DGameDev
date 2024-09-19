using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float helth;

    protected Rigidbody2D rb;
    protected Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        helth -= damage;
        if (helth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
