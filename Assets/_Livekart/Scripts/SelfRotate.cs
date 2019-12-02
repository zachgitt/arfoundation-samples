using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SelfRotate : MonoBehaviour
{
    public float m_rotateSpeed = 32.0f;
    public GameObject m_shadowPrefab;
    public GameObject m_currentShadow;

    private List<ARRaycastHit> m_hits = new List<ARRaycastHit>();
    private GameObject m_arSessionOrigin;
    private ARRaycastManager m_raycastManager;

    public bool rotateVertical;
    public bool rotateHorizontal;


    // Start is called before the first frame update
    void Start() {
        m_arSessionOrigin = GameObject.Find("AR Session Origin");
        m_raycastManager = m_arSessionOrigin.GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update() {
        int x = 0, y = 0;
        if (rotateVertical) {
            x = 90;
        }
        if (rotateHorizontal) {
            y = 90;
        }

        float step = m_rotateSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, new Vector3(x, y,0), step);

        // Create a shadow to give better depth cues
        Ray ray = new Ray(transform.position, -transform.up);
        if (m_raycastManager.Raycast(ray, m_hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon)) {
            if (m_currentShadow == null) {
                Debug.Log("Shadow null, creating it");
                m_currentShadow = Instantiate(m_shadowPrefab);
            }
            m_currentShadow.transform.position = m_hits[0].pose.position;
            m_currentShadow.transform.rotation = transform.rotation;
            m_currentShadow.transform.localScale = new Vector3(transform.localScale.x, 0.0001f, transform.localScale.z);
        }
    }
}
