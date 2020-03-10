using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastSceneController : MonoBehaviour
{
	public List<string> TextToWrite;
	public AudioSource Audio;
	public AudioSource AudioTalk;
	public GameObject TextObj;
	public GameObject TextObj2;
	public GameObject TextBack;
	public GameObject Player;
	public GameObject EndSceneCheckPoint;
	public GameObject TransferCamTo;
	public GameObject ImageScript;
	public int u = 0;
	private bool ready = true;
	private bool First = true;
	private bool puk = true;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Player.GetComponent<CharacterContScript>().can_walk = false;
        ImageScript.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    	if(Player.transform.position.x >= EndSceneCheckPoint.transform.position.x && puk){
    		puk = false;
    		StartCoroutine(Transfer());
    	}
    	if(ready && (Input.GetKeyDown("q") || First)){
    		AudioTalk.Play();
    		if(u == 3){
    			TextBack.SetActive(false);
    			TextObj.GetComponent<Text>().text = "";
    			TextObj.GetComponent<TextScript>().deactivate = true;
    			Player.GetComponent<CharacterContScript>().can_walk = true;
    			ready = false;
    			AudioTalk.Stop();
    		}
    		else{
    			First = false;
        		TextObj.GetComponent<TextScript>().DrawText(TextToWrite[u]);
        		ready = false;
        	}
    	}
    }

    public void Done(){
    	AudioTalk.Stop();
    	u++;
    	ready = true;
    }

    IEnumerator Transfer(){
    	Audio.Play();
    	yield return new WaitForSeconds(1);
    	Audio.Stop();
    	gameObject.transform.position = TransferCamTo.transform.position;
    	gameObject.GetComponent<Camera>().enabled = false;
    	Player.SetActive(false);
    	TextObj.SetActive(false);
    	TextObj2.SetActive(true);
    	ImageScript.SetActive(true);
    }
}