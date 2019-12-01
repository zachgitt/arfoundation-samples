using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SceneController : MonoBehaviour
{
	public GameObject ARCamera;
	private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float endTime = Time.time;
        float delta = endTime - startTime;
        if (3 < delta  && delta < 5) {
        	var videoPlayer = ARCamera.GetComponent<VideoPlayer>();
        	videoPlayer.targetCameraAlpha = 0.75F;
        	videoPlayer.Play();
        }
    }
}
