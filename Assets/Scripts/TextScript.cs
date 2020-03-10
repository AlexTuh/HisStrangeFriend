using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
	public GameObject PressButton;

	public Text text;
	public float delay = 0.01f;
	public float time_rem = 0;
	public float timer_from;
	public bool ready = true;
	public string txt_draw = "";
	public int i = -1;

	public bool new_text = false;

	public bool deactivate = false;

	public GameObject GameController;

	void Update(){
		if(deactivate){
			return;
		}
		if(!new_text){
			timer_from += Time.deltaTime;
			if(timer_from > 5){
        		PressButton.SetActive(true);
    		}
			return;
		}
		else{
			timer_from = 0;
			PressButton.SetActive(false);
		}
		if(time_rem > 0){
			time_rem -= Time.deltaTime;
		}
		else{
			text.text = txt_draw.Substring(0, i);
			time_rem = delay;
			i++;
		}
		if(i > txt_draw.Length){
			new_text = false;
			GameController.SendMessage("Done");
        	timer_from += Time.deltaTime;
		}
	}

    public void DrawText(string text_to_draw, float drawspeed = 0.01f){
    	new_text = true;
    	i = 0;
    	txt_draw = text_to_draw;
    	delay = drawspeed;
    }
}
