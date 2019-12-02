using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;

public class SceneController : MonoBehaviour
{
	public GameObject ARCamera;
	public GameObject ARSessionOrigin;
	private float startTime;
	private ARPlaneManager arPlaneManager;
	private PlaceMultipleObjectsOnPlane planeScript;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        arPlaneManager = ARSessionOrigin.GetComponent<ARPlaneManager>();
        planeScript = ARSessionOrigin.GetComponent<PlaceMultipleObjectsOnPlane>();
    }

    // Update is called once per frame
    void Update()
    {
    	StartCoroutine(PlayPlaneGif());
    	StartCoroutine(PlayTapGif());
    }

    // Plane Gif
    // Play 3 seconds after start, if no plane detected.
    IEnumerator PlayPlaneGif() {
        float delta = Time.time - startTime;
        float begin = 3;
        float end = 9;

        // TODO: && arPlaneManager.planeCount() == 0 does not work
        // List<ARPlane> allPlanes = new List<ARPlane>();
        // arPlaneManager.GetAllPlanes(allPlanes);

        if (begin < delta) {
        	foreach (VideoPlayer vsource in ARCamera.GetComponents<VideoPlayer>()) {
        		if (vsource.clip.name.Equals("plane")) {
        			if (delta < end) {
        				vsource.Play();
        			}
        			else {
        				vsource.Stop();
        			}
        		}

        	}
        }

        yield return null;
    }

    // Tap Gif
    // Play 3 seconds after plane detected, if no cubes placed.
    IEnumerator PlayTapGif() {
        float delta = Time.time - startTime;
        float begin = 12;
        float end = 15;

        if (begin < delta) {
        	foreach (VideoPlayer vsource in ARCamera.GetComponents<VideoPlayer>()) {
        		if (vsource.clip.name.Equals("tap")) {
        			if (delta < end && planeScript.spawnedObjects.Count == 0) {
        				vsource.Play();
        			}
        			else {
        				vsource.Stop();
        			}
        		}

        	}
        }

        yield return null;
    }
}
