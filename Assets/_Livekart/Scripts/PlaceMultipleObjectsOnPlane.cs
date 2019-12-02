using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceMultipleObjectsOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_powerUpPlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject powerUpPlacedPrefab
    {
        get { return m_powerUpPlacedPrefab; }
        set { m_powerUpPlacedPrefab = value; }
    }

    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_coinPlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject coinPlacedPrefab
    {
        get { return m_coinPlacedPrefab; }
        set { m_coinPlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    /*
    THIS IS OUR SHIT
    */
    public List<GameObject> Coins;
    public List<GameObject> PowerUps;
    public GameObject sceneController;
    private SceneController sceneScript;

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Start() {
         sceneScript = sceneController.GetComponent<SceneController>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    //spawnedObject = Instantiate(m_powerUpPlacedPrefab, hitPose.position, hitPose.rotation);
                    
                    if (sceneScript.currStateIsSetup1()) {
                        spawnedObject = Instantiate(m_coinPlacedPrefab, hitPose.position, hitPose.rotation);
                        Coins.Add(spawnedObject);
                    }
                    else if (sceneScript.currStateIsSetup2()) {
                        spawnedObject = Instantiate(m_powerUpPlacedPrefab, hitPose.position, hitPose.rotation);
                        PowerUps.Add(spawnedObject);
                    }

                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
        }
    }
}
