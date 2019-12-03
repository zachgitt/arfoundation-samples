using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatePowerUp : MonoBehaviour
{
    public float rotationRate = 9f;
    private bool rotating;
    //private int numSpins;
    //private string faceValue;

    // Start is called before the first frame update
    void Start()
    {
        rotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotating)
        {
            int numSpins = GetSpins(8, 20);
            string face = returnFace(numSpins);
            StartCoroutine(UpdatePowerUpCube(numSpins));
        }

        /*if (doneRotating) {
            faceValue = returnFace(numSpins % 4);
        }*/
    }

    // Spin N times
    int GetSpins(int min, int max) {
        return Random.Range(min, max);
    }

    // Update spinner
    IEnumerator UpdatePowerUpCube(int numSpins)
    {
        rotating = true;

        float startTime = 0.0f;
        float endTime = 1.0f;

        Quaternion initRotation = transform.rotation;
        float sourceAngle;
        Vector3 sourceAxis;
        initRotation.ToAngleAxis(out sourceAngle, out sourceAxis);

        //int numSpins = Random.Range(8, 20);
        //numSpins = Random.Range(8, 20);
        //printFace(numSpins % 4);
        float targetAngle = sourceAngle + 90 * numSpins;

        while (startTime <= endTime)
        {
            startTime += Time.deltaTime;
            float fractionOfJourney = startTime / endTime;
            float currentAngle = Mathf.Lerp(sourceAngle, targetAngle, fractionOfJourney);
            transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.left);

            yield return null;
        }

        // /returnFace(numSpins % 4);
        //doneRotating = true;
    }

    string returnFace(int numSpins)
    {
        int faceNum = numSpins % 4;

        string returnVal;
        switch(faceNum) {
            case (0):
                returnVal = "Magnet";
                break;
            case (1):
                returnVal = "Arrow";
                break;
            case (2):
                returnVal = "Ink";
                break;
            case (3):
                returnVal = "Clock";
                break;
            default:
                returnVal = "YOU FUCKED UP";
                break;
        }
        return returnVal;
    }
}
