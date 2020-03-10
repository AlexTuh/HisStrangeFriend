using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContScript : MonoBehaviour
{
    public bool scene = false; // if we are in 2 scene
    public bool ball_ready = true; // if payer can kick ball

	public float speed = 10f; // walking speed
	public float dist_to_interact = 5f; // distance where player can interact with ball
	public float ball_force = 20; //force that applied to ball

	public GameObject Ball;
	public GameObject Legs; // player legs to calculate distance from plater to ball

	public float time_rem = 0f; //how much time script need to whait
	private float time_rem_ball = 0f; //how much time script need to whait to ball need to be ready

	public Animator anim; //Animator

	public bool can_walk = true; //If player can walk
	public bool kicking = false; //If player is cicking ball

    public int times_ball_kicked = 0;

	void Start()
	{
		anim = gameObject.GetComponent<Animator>();
	}
    void Update()
    {
    	if(can_walk == false){ // cant walk
    		return;
    	}
    	if(time_rem_ball > 0)
    	{
    		time_rem_ball -= Time.deltaTime;
    		anim.SetBool("walk", false);
    		anim.SetBool("kickdel", true); // some animation fixes
    	}
    	else{
    		kicking = false;
    		anim.SetBool("kickdel", false);
    	}
        if(ball_ready == false){
            anim.SetBool("walk", false);
            anim.SetBool("kick", false);
            return;
        }
        if(Vector2.Distance(gameObject.transform.position, Ball.transform.position) < 2 && scene == true && ball_ready == true){
            dist_to_interact = 2; 
            anim.SetBool("walk", false); //if we are in second scene
            if(Input.GetKeyDown("e") && kicking == false){
                anim.SetBool("walk", false); //stop
                ball_ready = false;
                Use();
                kicking = true;
            }
            return;
        }
    	if(time_rem > 0)
    	{
    		time_rem -= Time.deltaTime;
    	}
    	else{
    		anim.SetBool("kick", false);
    	}
    	if(Input.GetKeyDown("e") && kicking == false){ // kick da boll
        	Use();
        	kicking = true;
        }
        if(Input.GetKey("a") && kicking == false && scene == false) //go left
        {
        	transform.Translate(Vector2.right * Time.deltaTime * speed);
        	Flip(true);
        	anim.SetBool("walk", true);
        }
        else if(Input.GetKey("d") && kicking == false) // go right
        {
        	transform.Translate(Vector2.right * Time.deltaTime * speed);
        	Flip(false);
        	anim.SetBool("walk", true);
        }
        else{ // stop
        	anim.SetBool("walk", false);
        }
    }

    void Use() //kicking ball void
    {
        anim.SetBool("kick", true);
        anim.SetBool("walk", false);
        time_rem = 0.02f;
        time_rem_ball = 0.6f;
    	if(Mathf.Abs(Legs.transform.position.x - Ball.transform.position.x) < dist_to_interact && Ball.transform.position.y - Legs.transform.position.y < 1)
    	{ //if player neer ball he can kick it
            times_ball_kicked++;
    		if(Legs.transform.position.x < Ball.transform.position.x){
    			Ball.GetComponent<BallScript>().AddForce(ball_force);
    		}
    		else{
    			Ball.GetComponent<BallScript>().AddForce(ball_force, true); //reverce hit
    		}
    	}
    }

    void Flip(bool right) //character flip
    {
    	if(right){
    		gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
    	}
    	else{
    		gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    	}
    }
    public void Emotion(string name, bool state){ //void for other scripts
    	anim.SetBool(name, state);
    }
}
