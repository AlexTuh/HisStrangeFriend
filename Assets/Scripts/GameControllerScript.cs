using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameControllerScript : MonoBehaviour
{
	public string[] to_write;
	public bool write = false;

	public GameObject _Text;

    public GameObject PressE;

    public GameObject Fade;

	AudioSource audioData;

	public GameObject Sound;
	public GameObject TextBG;

	public int replica = 0;

	public GameObject Player;
	public float time_rem = 2f;
    public float time_rem_to_another_scene = 10f;
	public bool first = true;

	private int stage = 1;

	void Start()
	{
		TextBG.SetActive( false );
		Player = GameObject.FindWithTag("Player");
		audioData = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
		_Text = GameObject.FindWithTag("Text");
        _Text.GetComponent<TextScript>().deactivate = true;
	}

    void Update()
    {
        if(replica >= 5){
            if(time_rem_to_another_scene < 0)
                StartCoroutine(Transfer());
            else
                time_rem_to_another_scene -= Time.deltaTime;
        }
        if(Player.GetComponent<CharacterContScript>().times_ball_kicked > 2){
            PressE.SetActive(false);
        }
        if(replica == 0 && time_rem < 0){
            _Text.GetComponent<TextScript>().deactivate = false;
        }
        else if(replica == 5){
            _Text.GetComponent<TextScript>().deactivate = true;
        }
    	if(replica < 5){
    		Player.GetComponent<CharacterContScript>().can_walk = first;
    	}
    	else{
    		Player.GetComponent<CharacterContScript>().can_walk = true;
    	}
    	if(time_rem < 0){
    		if(stage == 2){
    			Player.GetComponent<CharacterContScript>().Emotion("happy", true);
    			Player.GetComponent<CharacterContScript>().Emotion("normal", false);
    			Player.GetComponent<CharacterContScript>().Emotion("sad", false);
    			stage = 3;
    			time_rem = 1f;
    		}
    		else if(replica == 5){
    			Player.GetComponent<CharacterContScript>().Emotion("happy", false);
    			Player.GetComponent<CharacterContScript>().Emotion("normal", false);
    			Player.GetComponent<CharacterContScript>().Emotion("sad", false);
    		}
    		else{
    			write = true;
    		}
    	}
    	else{
    		write = false;
    		time_rem -= Time.deltaTime;
    	}

        if(write == true && (Input.GetKeyDown("q") || first))
        {
        	first = false;
        	write = false;
        	time_rem = 1f;
        	TextBG.SetActive( true );
        	if(replica < 3 && stage == 1){
        		Player.GetComponent<CharacterContScript>().Emotion("sad", true);
        	}
        	else if(replica == 3){
        		stage = 2;
        		time_rem = 2f;
        	}
        	else{
        		TextBG.SetActive( false );
        	}
        	_Text.GetComponent<TextScript>().DrawText(to_write[replica]);
        	audioData.Play();
        }
        if(stage == 2){
        	Player.GetComponent<CharacterContScript>().Emotion("happy", true);
        	Player.GetComponent<CharacterContScript>().Emotion("normal", false);
        	Player.GetComponent<CharacterContScript>().Emotion("sad", false);
        }

    }

    public void Done()
    {
    	audioData.Stop();
    	if(replica >= to_write.Length){
    		replica = to_write.Length;
    	}
    	else{
    		replica++;
    	}
    	write = true;
    	time_rem = 0.1f;
    }
    IEnumerator Transfer(){
        Fade.SendMessage("Fade");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(1);
    }
}