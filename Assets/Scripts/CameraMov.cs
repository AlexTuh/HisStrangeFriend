using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraMov : MonoBehaviour
{
	AudioSource audioData;

	public GameObject Player;
	public GameObject Ball;
	public GameObject TextDrawer;
	public Transform LastCamPos;
	public Transform CheckPoint1;
	public Transform CheckPoint2;
	public bool camera_lock = true;
	public List<GameObject> BallMove;
	public List<GameObject> PositionToBall;

	public GameObject Fade;

	public string[] replicas;
	public int u = 0;
	public bool can_wrt_txt = true;
	public bool First = true;
	public bool[] done = new bool[3]{false, false, false};
	public GameObject TextBack;

	public int stage = 0;
	public bool last = false;

    // Start is called before the first frame update
    void Start()
    {
    	TextBack.SetActive(false);
        Player = GameObject.FindWithTag("Player");
        Player.GetComponent<CharacterContScript>().scene = true;
        audioData = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(Player.transform.position.x >= CheckPoint1.position.x && stage == 1){
    		Player.GetComponent<CharacterContScript>().can_walk = false;
    		Player.GetComponent<CharacterContScript>().anim.SetBool("walk", false);
    		stage = 2;
    	}
    	if(Player.transform.position.x >= CheckPoint2.position.x && stage == 3){
    		StartCoroutine(Transfer());
    	}
    	if(stage == 0 && can_wrt_txt == true && (Input.GetKeyDown("q") || First) && done[0] == false){ //FIRST STAGE
    		TextDrawer.GetComponent<TextScript>().deactivate = false;
    		TextBack.SetActive(true);
    		audioData.Play();
    		TextDrawer.GetComponent<TextScript>().DrawText(replicas[u]);
    		Player.GetComponent<CharacterContScript>().can_walk = false;
    		can_wrt_txt = false;
    		First = false;
    	}
    	else if(stage == 1 && can_wrt_txt == true && (Input.GetKeyDown("q") || First) && done[1] == false){ //SECOND STAGE
    		TextDrawer.GetComponent<TextScript>().deactivate = false;
    		TextBack.SetActive(true);
    		audioData.Play();
    		TextDrawer.GetComponent<TextScript>().DrawText(replicas[u]);
    		Player.GetComponent<CharacterContScript>().can_walk = false;
    		can_wrt_txt = false;
    		First = false;
    	}
    	else if(stage == 2 && can_wrt_txt == true && (Input.GetKeyDown("q") || First) && done[2] == false){ //THIRD STAGE
    		TextDrawer.GetComponent<TextScript>().deactivate = false;
    		TextBack.SetActive(true);
    		audioData.Play();
    		TextDrawer.GetComponent<TextScript>().DrawText(replicas[u]);
    		Player.GetComponent<CharacterContScript>().can_walk = false;
    		can_wrt_txt = false;
    		First = false;
    	}
    	else if(((stage == 0 && done[0] == true) || (stage == 1 && done[1] == true) || done[2] == true) && Input.GetKeyDown("q")){ //NONE
    		TextDrawer.GetComponent<TextScript>().deactivate = true;
    		TextBack.SetActive(false);
    		TextDrawer.GetComponent<Text>().text = "";
    	}
    	if(last == false){
    		if(BallMove[stage].transform.position.x < Ball.transform.position.x && camera_lock == true)
    		{
    			Ball.transform.position = PositionToBall[stage].transform.position;
    			Ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    			Player.GetComponent<CharacterContScript>().ball_ready = true;
    			Ball.GetComponent<Rigidbody2D>().angularVelocity = 0;
    			camera_lock = false;
    			stage++;
    			if(stage == 1){
    				last = true;
    			} 
    		}
    		if(camera_lock == false){
    			if(gameObject.transform.position.x > BallMove[stage-1].transform.position.x){
    				camera_lock = true;
    			}
    		}
    	}
    	else if(gameObject.transform.position.x >= LastCamPos.position.x){
    		Player.GetComponent<CharacterContScript>().can_walk = false;
    		Player.GetComponent<CharacterContScript>().anim.SetBool("walk", false);
    	}
    }

    public void Done(){
    	u++;
    	can_wrt_txt = true;
    	if(u == 7){
    		done[0] = true;
    		Player.GetComponent<CharacterContScript>().can_walk = true;
    		First = true;
    	}
    	if(u == 10){
    		done[1] = true;
    		Player.GetComponent<CharacterContScript>().can_walk = true;
    		First = true;
    	}
    	if(u == 15){
    		done[2] = true;
    		Player.GetComponent<CharacterContScript>().can_walk = true;
    		First = true;
    		stage = 3;
    	}
    	if(u >= 5 && u <= 6){
    		Player.GetComponent<CharacterContScript>().Emotion("sad", true);
    	}
    	else{
    		Player.GetComponent<CharacterContScript>().Emotion("sad", false);
    	}
    	audioData.Stop();
    }
    IEnumerator Transfer(){
        Fade.SendMessage("Fade");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(3);
    }
}
