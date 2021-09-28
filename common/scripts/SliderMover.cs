using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderMover : MonoBehaviour
{

    public Slider xSlider;
    public Slider ySlider;
    public Slider zSlider;
    public Slider xRotSlider;
    public Slider yRotSlider;
    public Slider zRotSlider;
   


    
    public GameObject moverAxis;

    private GameObject depthCursor;
    private GameObject previousParent;
    private GameObject selectedChild;

    public GameObject movingObject;
    private GameObject fixedMover;
    private GameObject[] moverList = new GameObject[2]; //[fixObject.mover.hitobject,previous parent]
    public Text debugText;


    void Start()
    {
        //depthCursor = GameObject.Find("Reticle");
    }

    public void SelectObject() // dt = 0.02 sec
    {
        debugText.text = "cant hityt";
        Vector2 ScreenPosition = new Vector2(0.5f, 0.5f);
        var xPixel = Camera.main.pixelWidth * ScreenPosition.x;
        var yPixel = Camera.main.pixelHeight * ScreenPosition.y;

        Vector3 screenPos = new Vector3(xPixel, yPixel, 0.0f);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out hit)) //&& hit.transform.gameObject.tag == "WayPoint")
        {
  
            selectedChild = hit.transform.parent.gameObject; //takeparent of hitted object
            if (selectedChild.transform.parent != null) //if parent exist
            {
                previousParent = selectedChild.transform.parent.gameObject; //save previous parent
            }
            else
            {
                previousParent = null;
            }
            debugText.text = "cantInitiate";
            var mover = Instantiate(moverAxis, selectedChild.transform.position, selectedChild.transform.rotation);
            var fixedFrame = Instantiate(moverAxis, selectedChild.transform.position, selectedChild.transform.rotation);

            debugText.text = "initiatted";
            movingObject = mover;
            fixedMover = fixedFrame;
            moverList[0] = movingObject;
            moverList[1] = fixedMover;

            selectedChild.transform.parent = movingObject.transform;

            }
  
    }


    public void MoveOnValueChanged()
    {
        Vector3 moveVector = new Vector3(xSlider.value, ySlider.value, zSlider.value);
        movingObject.transform.position = fixedMover.transform.position
                                            + moveVector.x * fixedMover.transform.right
                                            + moveVector.y * fixedMover.transform.up
                                            + moveVector.z * fixedMover.transform.forward;
    }

    public void RotateOnClicked(Vector3 rotationVector)
    {
        movingObject.transform.Rotate(rotationVector);
    }



    public void FinishMove()
    {
        if (previousParent.transform != null)
        { 
            selectedChild.transform.parent = previousParent.transform;
        }
        else
        {
            selectedChild.transform.parent = null;
        }

        foreach (GameObject move in moverList)
        {
            Destroy(move);
        }


    }



    public void SetZero()
    {
        xSlider.value = 0.0f;
        ySlider.value = 0.0f;
        zSlider.value = 0.0f;
    }
}