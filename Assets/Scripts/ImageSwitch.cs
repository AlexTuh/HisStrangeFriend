using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageSwitch : MonoBehaviour
{
	//you are awesome :-)
	public List<GameObject> Images;
	public List<string> Text;
	public GameObject TextObj;
	public AudioSource Audio;
	public AudioClip Distorted;
    public AudioClip Normal;
	public GameObject BackText;
	public GameObject PressButton;
	public AudioSource BackAudio;

    public GameObject Fade;

	public bool First = true;

	public float time_rem = 6;

	public int stage = 0;

    public int scene = 0;

    // Start is called before the first frame update
    void Start()
    {
    	BackText.SetActive(false);
    	for(int i = 0; i < Images.Count; i++){
    		if(i != 0){
    			Images[i].SetActive(false);
    		}
    	}
    }

    void Update(){
        if(time_rem > 0){
            time_rem -= Time.deltaTime;
        }
        else if(Input.GetKeyDown("q") || First){
            if(First){
                BackText.SetActive(true);
            }
            First = false;
            if(scene == 0){ //first scene
                if(stage == 2){
                    ChangeImage(1);
                }
                else if(stage == 4){
                    ChangeImage(2);
                    BackAudio.Stop();
                }
                else if(stage == 6){
                    ChangeImage(3);
                } 
                else if(stage == 9){
                    BackText.SetActive(false);
                    ChangeImage(4);
                    Audio.clip = Distorted;
                    StartCoroutine(LoadSceneWait(2, 2, false));
                }
            }
            else if(scene == 1){ //second
                if(stage == 0){
                    BackText.SetActive(false);
                }
                else if(stage == 1){
                    ChangeImage(1);
                }
                else if(stage == 5){
                    ChangeImage(2);
                }
                else if(stage == 8){
                    ChangeImage(3);
                }
                else if(stage == 12){
                    ChangeImage(4);
                }
                else if(stage == 17){
                    ChangeImage(5);
                    BackText.SetActive(false);
                    BackAudio.Pause();
                }
                else if(stage == 18){
                    ChangeImage(6);
                    BackText.SetActive(false);
                    Audio.clip = Distorted;
                    Audio.time = 4.5f;
                }
                else if(stage == 19){
                    ChangeImage(7);
                    Audio.clip = Normal;
                }
                else if(stage == 23){
                    Audio.mute = true;
                    StartCoroutine(LoadSceneWait(4, 1.3f));
                }
            }
            else if(scene == 2){
                if(stage == 0){
                    ChangeImage(1);
                }
                else if(stage == 2){
                    ChangeImage(2);
                    First = true;
                    BackText.SetActive(false);
                }
                else if(stage == 3){ //s
                    ChangeImage(2);
                    First = true;
                    time_rem = 2f;
                    BackText.SetActive(false);
                }
                else if(stage == 4){
                    ChangeImage(3);
                    First = true;
                    time_rem = 2f;
                    BackText.SetActive(false);
                }
                else if(stage == 5){
                    ChangeImage(4);
                    First = true;
                    time_rem = 2f;
                    BackText.SetActive(false);
                }
                else if(stage == 6){
                    StartCoroutine(LoadSceneWait(5, 1.3f));
                }
            }
            Audio.Play();
            TextObj.GetComponent<TextScript>().DrawText(Text[stage]);
            stage++;
        }
    }

    public void Done()
    {
        if(scene == 0){ //first
    	   if(stage != 10){
    	       Audio.Stop();
    	   }
    	   if(stage == 6){
    	       time_rem = 0.0001f;
    	   }
    	   else{
    	       time_rem = 0.5f;
    	   }
        }
        else if(scene == 1){
            if(stage == 1){
                time_rem = 1;
                Audio.Stop();
                First = true;
            }
            else if(stage == 18){ //pause face
                time_rem = 3f;
                Audio.Stop();
                First = true;
            }
            else if(stage == 19){ //glitch
                time_rem = 3f;
                StartCoroutine(StopMusic());
                First = true;
            }
            else{
                time_rem = 0.5f;
                Audio.Stop();
            }
        }
        else if(scene == 2){
            Audio.Stop();
            if(stage > 2){
                time_rem = 2f;
                First = true;
            }
            if(stage == 7){
                SceneManager.LoadScene(5);
            }
        }
    }
    void ChangeImage(int num){
    	for(int i = 0; i < Images.Count; i++){
    		if(num == i){
    			Images[i].SetActive(true);
    		}
    		else{
    			Images[i].SetActive(false);
    		}
    	}
    }
    IEnumerator LoadSceneWait(int number, float wait, bool fade = true)
    {
        if(fade){
            Fade.SendMessage("Fade");
        }
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(number);
    }
    IEnumerator StopMusic()
    {
        yield return new WaitForSeconds(3f);
        Audio.Stop();
    }
}
