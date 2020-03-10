using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
	Rigidbody2D rb;
    GameObject Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Physics2D.IgnoreCollision(Player.GetComponent<CircleCollider2D>(), gameObject.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(Player.GetComponent<BoxCollider2D>(), gameObject.GetComponent<CircleCollider2D>());
    }

    public void AddForce(float force, bool reverce = false)
    {
    	if(reverce){
    		rb.velocity = new Vector2(-0.5f, 0.5f) * force;
    	}
    	else{
    		rb.velocity = new Vector2(0.5f, 0.5f) * force;
    	}
    }
}
