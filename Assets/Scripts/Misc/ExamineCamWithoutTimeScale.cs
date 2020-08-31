using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ExamineCamWithoutTimeScale : MonoBehaviour
{

    Camera mainCam;//Camera Object Will Be Placed In Front Of
    GameObject clickedObject;//Currently Clicked Object

    //Holds Original Postion And Rotation So The Object Can Be Replaced Correctly
    Vector3 originaPosition;
    Vector3 originalRotation;

    Rigidbody diceRigidbody;

    CinemachineBrain cinemachineBrain;
    CinemachineVirtualCamera virtualCamera;
    Vector3 originalLookAt;

    public float distanceFromCam = 0.5f;

    //If True Allow Rotation Of Object
    bool examineMode;

    void Start()
    {
        mainCam = Camera.main;
        examineMode = false;

        cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    private void Update()
    {

        ClickObject();//Decide What Object To Examine

        TurnObject();//Allows Object To Be Rotated

        ExitExamineMode();//Returns Object To Original Postion
    }


    void ClickObject()
    {
        if (Input.GetMouseButtonDown(0) && examineMode == false)
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //ClickedObject Will Be The Object Hit By The Raycast
                clickedObject = hit.transform.gameObject;

                if (clickedObject.tag == "Examinable")
                {
                    //Save The Original Postion And Rotation
                    originaPosition = clickedObject.transform.position;
                    originalRotation = clickedObject.transform.rotation.eulerAngles;
                    //originalLookAt = cinemachineBrain.ActiveVirtualCamera.LookAt.transform.position;

                    //Disable gravity and constraint position on all axis
                    diceRigidbody = clickedObject.GetComponent<Rigidbody>();
                    diceRigidbody.useGravity = false;
                    diceRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

                    //Now Move Object In Front Of Camera and LookAt Focus Point
                    clickedObject.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 4, Screen.height / 4, mainCam.nearClipPlane)) + (transform.forward * distanceFromCam);
                    //cinemachineBrain.ActiveVirtualCamera.LookAt.transform.position = clickedObject.transform.position;

                    //Pause The Game
                    //Time.timeScale = 0;

            
                    //Turn Examine Mode To True
                    examineMode = true;
                }
            }
        }
    }

    void TurnObject()
    {
        if (Input.GetMouseButton(0) && examineMode)
        {
            float rotationSpeed = 15;

            float xAxis = Input.GetAxis("Mouse X") * rotationSpeed;
            float yAxis = Input.GetAxis("Mouse Y") * rotationSpeed;

            clickedObject.transform.Rotate(Vector3.up, -xAxis, Space.World);
            clickedObject.transform.Rotate(Vector3.right, -yAxis, Space.World);
        }
    }

    void ExitExamineMode()
    {
        if (Input.GetMouseButtonDown(1) && examineMode)
        {
            //Reset Object To Original Position
            clickedObject.transform.position = originaPosition;
            clickedObject.transform.eulerAngles = originalRotation;

            diceRigidbody.useGravity = true;

            //and also reset Cinemachine LookAt
            //cinemachineBrain.ActiveVirtualCamera.LookAt.transform.position = originalLookAt;

            //Unpause Game
            Time.timeScale = 1;

            //Return To Normal State
            examineMode = false;
        }
    }
}