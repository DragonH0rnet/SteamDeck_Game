using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private GameObject core;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float hp = 5;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float attackKnockback = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        core = GameObject.Find("Core Obj");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(core.transform);
        transform.up = core.transform.position - transform.position;
        this.transform.position += transform.up * Time.deltaTime * speed;
        rb.AddForce(transform.up * speed);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        
        if (collision.gameObject.tag == "Core")
        {
            collision.gameObject.GetComponent<core>().TakeDamage(damage);
            rb.AddForce(transform.up * -1 * attackKnockback, ForceMode2D.Impulse);
            //transform.position += (transform.up * -1 * attackKnockback);
        }
    }
    
}
