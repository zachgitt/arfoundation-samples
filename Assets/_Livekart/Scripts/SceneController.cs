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

	// Gifs
	public GameObject moveGif;
	public GameObject tapGif;

	// Pictures
	public GameObject movePic;
	public GameObject tapPic;

	// Setup UI
	public GameObject setupPanel;
	public GameObject setupText;
	public GameObject setupCoins;
	public GameObject setupPowerUps;
	public GameObject instructionPanel;
	public GameObject instructionText;

	// Gameplay UI
	private int totalCoins;
	private float clock;
	private int countdown;
	public GameObject playPanel;
	public GameObject playCoins;
	public GameObject timer;
	public GameObject countdownText;

	// Determines state of the game
	public enum State {Setup1, Setup2, Setup3, Play, GameOver};
	public State currState;

    // Start is called before the first frame update
    void Start()
    {
    	Debug.Log("START");

    	// Activate correct panels
    	setupPanel.SetActive(true);
    	instructionPanel.SetActive(true);
    	playPanel.SetActive(false);
    	countdownText.SetActive(false);

        startTime = Time.time;
        arPlaneManager = ARSessionOrigin.GetComponent<ARPlaneManager>();
        planeScript = ARSessionOrigin.GetComponent<PlaceMultipleObjectsOnPlane>();
        setupText.GetComponent<Text>().text = "Setup: 0/3";
        countdownText.GetComponent<Text>().text = "10";
        currState = State.Setup1;

        // Gifs
        // moveGif.SetActive(false);
        // tapGif.SetActive(false);
        float duration1 = 2, delay1 = 3;
        float duration2 = 2, delay2 = 8;
        StartCoroutine(showPic(movePic, duration1, delay1));
        StartCoroutine(showPic(tapPic, duration2, delay2));


        // moveGif.GetComponent<Animator>().Play("MoveLoop");
        // if (moveGif.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MoveLoop")) {
        // 	Debug.Log("MOVING");
        // }
        // else {
        // 	Debug.Log("NOT MOVING");
        // }
     //    StartCoroutine(PlayGif(moveGif, duration1, delay1));
    	// StartCoroutine(PlayGif(tapGif, duration2, delay2));
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
    		StartCoroutine(RemovePics());
    	}

    	switch (currState) {
    		case State.Setup1:
    		    UpdateSetup(1);
    			UpdateCoinSetup();
    			UpdateInstructions("Place Coins", Color.red);
    			break;

    		case State.Setup2:
    			UpdatePowerUpSetup();
    			UpdateInstructions("Place PowerUps", Color.yellow);
    			break;

    		case State.Setup3:
    			UpdateInstructions("Place Phone\nOn RC Car", Color.green);
    			break;

    		case State.Play:
    			UpdateCoinPlay();
    			UpdateTimer();
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
    		instructionPanel.SetActive(false);
    	}
    	setupText.GetComponent<Text>().text = "Setup: " + state.ToString() + "/3";
    }

    void UpdateCoinSetup() {
    	setupCoins.GetComponent<Text>().text = "Coins: " + planeScript.Coins.Count.ToString();
    	totalCoins = planeScript.Coins.Count;
    }


    void UpdatePowerUpSetup() {
    	setupPowerUps.GetComponent<Text>().text = "PowerUps: " + planeScript.PowerUps.Count.ToString();
    }


    void UpdateInstructions(string msg, Color color) {
    	instructionText.GetComponent<Text>().text = msg;
    	instructionText.GetComponent<Text>().color = color;
    }

    void UpdateCoinPlay() {
    	playCoins.GetComponent<Text>().text = "Coins: " + (totalCoins - planeScript.Coins.Count).ToString() + "/" + totalCoins.ToString();
    }

    void UpdateTimer() {
    	if (countdown == 0) {
            clock += Time.deltaTime;
            int seconds = Mathf.RoundToInt(clock);
            timer.GetComponent<Text>().text = "Time: " + seconds.ToString() + "s";
        }
    }

    IEnumerator Countdown() {
    	countdown = 10;
    	while (countdown > 0) {
    		countdown--;
    		countdownText.GetComponent<Text>().text = countdown.ToString();
    		yield return new WaitForSeconds(1.0f);
    	}

    	countdownText.SetActive(false);
    }

    IEnumerator showPic(GameObject pic, float duration, float delay) {

    	// Delay
    	while (delay > 0) {
    		delay -= Time.deltaTime;
    		yield return null;
    	}

    	// Show pic
    	pic.SetActive(true);
    	while (duration > 0) {
        	duration -= Time.deltaTime;
        	yield return null;
    	}

    	// Remove pic
    	pic.SetActive(false);
    }

    // Plane Gif
    IEnumerator PlayGif(GameObject gif, float duration, float delay) {

        // TODO: && arPlaneManager.planeCount() == 0 does not work
        // List<ARPlane> allPlanes = new List<ARPlane>();
        // arPlaneManager.GetAllPlanes(allPlanes);

    	Debug.Log("PLAYING GIF");

    	// Determine gameobject
    	// GameObject gif = null;
    	// if (gifName == "move") {
    	// 	gif = moveGif;
    	// }
    	// else if (gifName == "tap") {
    	// 	gif = tapGif;
    	// }

    	// Delay
    	while (delay > 0) {
    		delay -= Time.deltaTime;
    		yield return null;
    	}


    	// Play gif
    	// string stateName = "";
    	// if (gif == moveGif) {
    	// 	stateName = "MoveLoop";
    	// }
    	// else if (gif == tapGif) {
    	// 	stateName = "TapLoop";
    	// }

    	gif.GetComponent<Animator>().enabled = true;
    	while (duration > 0) {
    		// foreach (VideoPlayer vsource in ARCamera.GetComponents<VideoPlayer>()) {
	     //    	if (vsource.clip.name.Equals(gifName)) {
	     //    		vsource.Play();
	     //    	}
      //   	}
        	duration -= Time.deltaTime;
        	yield return null;
    	}


    	// Stop video
		// foreach (VideoPlayer vsource in ARCamera.GetComponents<VideoPlayer>()) {
  //       	if (vsource.clip.name.Equals(gifName)) {
  //       		vsource.Stop();
  //       	}
  //   	}
        gif.GetComponent<Animator>().enabled = false;

    	yield return null;
    }

    IEnumerator RemovePics() {
    	// foreach (VideoPlayer vsource in ARCamera.GetComponents<VideoPlayer>()) {
     //    	vsource.Stop();
     //    	yield return null;
    	// }
    	movePic.SetActive(false);
    	tapGif.SetActive(false);
    	yield return null;
    }


    public void Continue() {

    	StartCoroutine(RemovePics());

    	switch (currState) {

    		case State.Setup1:
    		    if (planeScript.Coins.Count == 0) {
    		    	float duration = 2, delay = 0;
    		    	StartCoroutine(showPic(tapPic, duration, delay));
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
    			playPanel.SetActive(true);
    			countdownText.SetActive(true);
    			StartCoroutine(Countdown());
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
