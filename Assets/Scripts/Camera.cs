using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	public float minX = -8.37f;
	public float maxX = 8.36f;
	public float minY = -3.68f;
	public float maxY = 3.68f;

	public GameObject Player;
	public Transform Stop;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	public float lastPosX;
	public float lastPosY;

	public bool PlayerStop;

	private bool MoveTrue = false;

    void FixedUpdate()
    {
    	Vector3 smoothedPosition = new Vector3(0,0,0); //init
    	Vector3 desiredPosition = new Vector3(0,0,0); //init

    	if(lastPosX - Player.transform.position.x == 0 && lastPosY - Player.transform.position.y == 0){
    		PlayerStop = true;
    		StartCoroutine(WhaitWhenStop());
    	}
    	else{
    		PlayerStop = false;
    		MoveTrue = false;
    	}

    	//Smooth position left right by stop
    	if(MoveTrue == true){ //stop
    		desiredPosition = Stop.transform.position + offset; //right
    		smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.02f);
    	}
    	else{
    		desiredPosition = Player.transform.position + offset;
    		smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    	}

    	//END OF CAMERA
    	if(smoothedPosition.x < minX){
    		smoothedPosition.x = minX;
    	}
    	else if(smoothedPosition.x > maxX){
    		smoothedPosition.x = maxX;
    	}
    	if(smoothedPosition.y < minY){
    		smoothedPosition.y = minY;
    	}
    	else if(smoothedPosition.y > maxY){
    		smoothedPosition.y = maxY;
    	}
        transform.position = smoothedPosition; //CAMERA TRANSFORM POSITION
        lastPosX = Player.transform.position.x;
        lastPosY = Player.transform.position.y;
    }
    IEnumerator WhaitWhenStop(){//whait to move camera
    	yield return new WaitForSeconds(0.1f);
    	if(PlayerStop == true){
    		MoveTrue = true;
    	}
    }
}
