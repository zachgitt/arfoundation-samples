using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public GameObject ARCamera;
	public GameObject ARSessionOrigin;
	private float startTime;
	private ARPlaneManager arPlaneManager;
	private PlaceMultipleObjectsOnPlane planeScript;

	public GameObject setupPanel;
	public GameObject coins;
	public GameObject powerUps;
	public GameObject setup;

	public enum State {Setup1, Setup2, Setup3, Play, GameOver};
	public State currState;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        arPlaneManager = ARSessionOrigin.GetComponent<ARPlaneManager>();
        planeScript = ARSessionOrigin.GetComponent<PlaceMultipleObjectsOnPlane>();
        setup.GetComponent<Text>().text = "Setup: 0/3";
        currState = State.Setup1;

        // Play Gifs
        float duration1 = 6, delay1 = 3;
        float duration2 = 3, delay2 = 12;
        StartCoroutine(PlayGif("plane", duration1, delay1));
    	StartCoroutine(PlayGif("tap", duration2, delay2));
    }

    public bool currStateIsSetup1() {
    	return (currState == State.Setup1);
    }

    public bool currStateIsSetup2() {
    	return (currState == State.Setup2);
    }

    // Update is called once per frame
    void Update()
    {
    	if (planeScript.Coins.Count > 0) {
    		StartCoroutine(StopGifs());
    	}

    	switch (currState) {
    		case State.Setup1:
    		    UpdateSetup(1);
    			UpdateCoins();
    			break;

    		case State.Setup2:
    			UpdatePowerUps();
    			break;

    		case State.Setup3:
    			break;

    		case State.Play:
    			break;

    		case State.GameOver:
    			break;

    		default:
    			Debug.Log("STATE NOT FOUND");
    			break;
    	}
    }

    void UpdateSetup(int state) {
    	if (state == -1) {
    		setupPanel.SetActive(false);
    	}
    	setup.GetComponent<Text>().text = "Setup: " + state.ToString() + "/3";
    }

    void UpdateCoins() {
    	coins.GetComponent<Text>().text = "Coins: " + planeScript.Coins.Count.ToString();
    }

    void UpdatePowerUps() {
    	powerUps.GetComponent<Text>().text = "PowerUps: " + planeScript.PowerUps.Count.ToString();
    }



    // Plane Gif
    IEnumerator PlayGif(string gifName, float duration, float delay) {

        // TODO: && arPlaneManager.planeCount() == 0 does not work
        // List<ARPlane> allPlanes = new List<ARPlane>();
        // arPlaneManager.GetAllPlanes(allPlanes);

    	Debug.Log("PLAYING GIF");

    	// Delay
    	while (delay > 0) {
    		delay -= Time.deltaTime;
    		yield return null;
    	}


    	// Play video
    	while (duration > 0) {
    		foreach (VideoPlayer vsource in ARCamera.GetComponents<VideoPlayer>()) {
	        	if (vsource.clip.name.Equals(gifName)) {
	        		vsource.Play();
	        	}
        	}
        	duration -= Time.deltaTime;
        	yield return null;
    	}


    	// Stop video
		foreach (VideoPlayer vsource in ARCamera.GetComponents<VideoPlayer>()) {
        	if (vsource.clip.name.Equals(gifName)) {
        		vsource.Stop();
        	}
    	}
        
    	yield return null;
    }

    IEnumerator StopGifs() {
    	foreach (VideoPlayer vsource in ARCamera.GetComponents<VideoPlayer>()) {
        	vsource.Stop();
        	yield return null;
    	}
    }


    public void Continue() {

    	StartCoroutine(StopGifs());

    	switch (currState) {

    		case State.Setup1:
    		    if (planeScript.Coins.Count == 0) {
    		    	float duration = 3, delay = 0;
    		    	StartCoroutine(PlayGif("tap", duration, delay));
    		    }
    		    else {
    		    	UpdateSetup(2);
    		    	currState++;
    		    }

    			break;

    		case State.Setup2:
    		    currState++;
    			UpdateSetup(3);
    			break;

    		case State.Setup3:
    		    currState++;
    			UpdateSetup(-1);
    			break;

    		case State.Play:
    			break;

    		case State.GameOver:
    			break;

    		default:
    			Debug.Log("STATE NOT FOUND");
    			break;
    	}

    }

    public void Restart() {
    	SceneManager.LoadScene("SetUp");
    }
}
