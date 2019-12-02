using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerUp : MonoBehaviour
{
    public float rotationRate = 9f;
    private bool rotated = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!rotated)
        {
            StartCoroutine(UpdatePowerUpCube());
        }
        
    }

    IEnumerator UpdatePowerUpCube()
    {
        rotated = true;

        float startTime = 0.0f;
        float endTime = 1.0f;

        Quaternion initRotation = transform.rotation;
        float sourceAngle;
        Vector3 sourceAxis;
        initRotation.ToAngleAxis(out sourceAngle, out sourceAxis);

        int numSpins = Random.Range(8, 20);
        printFace(numSpins % 4);
        float targetAngle = sourceAngle + 90 * numSpins;

        while (startTime <= endTime)
        {
            startTime += Time.deltaTime;
            float fractionOfJourney = startTime / endTime;
            float currentAngle = Mathf.Lerp(sourceAngle, targetAngle, fractionOfJourney);
            transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.left);

            yield return null;
        }
    }

    void printFace(int faceNum)
    {
        switch(faceNum) {
            case (0):
                Debug.Log("Magnet");
                break;
            case (1):
                Debug.Log("Arrow");
                break;
            case (2):
                Debug.Log("Ink");
                break;
            case (3):
                Debug.Log("Clock");
                break;
            default:
                Debug.Log("YOU FUCKED UP");
                break;
        }
    }
}
