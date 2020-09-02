using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Cinemachine;

public class ColorLock : MonoBehaviour
{
    public int goodDigit_1;
    public int goodDigit_2;
    public int goodDigit_3;

    private int[] digit;
    private float angleStep = 60f;

    public UnityEvent Unlocked;
    public DoorInteract doorInteract;

    public string roomLock;
    public CinemachineVirtualCamera lockCam;
    private int oldMask;

    void Start()
    {
        digit = new int[4];
        roomLock = gameObject.tag;
        oldMask = Camera.main.cullingMask;

        Randomize();
    }

    void Randomize()
    {
        goodDigit_1 = Random.Range(0, 5);
        goodDigit_2 = Random.Range(0, 5);
        goodDigit_3 = Random.Range(0, 5);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("digitRotate");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine("exitLock");
        }
    }

    IEnumerator exitLock()
    {

        CamOff();

        //RaycastHit hit;
        ////Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        //if (Physics.Raycast(ray, out hit) && hit.collider.tag == "lockTag")
        //{
        //    CamOff();
        //}

        yield return null;

    }

    IEnumerator digitRotate()
    {
        RaycastHit hit;
        //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit) && hit.collider.tag == "lockTag")
        {

            ColorLock buttonPad = hit.collider.GetComponentInParent<ColorLock>();

            if (buttonPad.gameObject.tag == roomLock)
            {
                hit.collider.transform.Rotate(Vector3.up * angleStep);

                var digitNumber = int.Parse(hit.collider.name.Substring(hit.collider.name.Length - 1));

                digit[digitNumber] = digit[digitNumber] + Mathf.FloorToInt(1 * Mathf.Sign(angleStep));


                if (digit[digitNumber] == -1)
                {
                    digit[digitNumber] = 5;
                }

                if (digit[digitNumber] == 6)
                {
                    digit[digitNumber] = 0;
                }

                if (digit[1] == goodDigit_1 && digit[2] == goodDigit_2 && digit[3] == goodDigit_3)
                {
                    Unlocked.Invoke();
                    doorInteract.locked = false;
                    CamOff();
                }

                Debug.Log(digit[digitNumber]);
            }

        }

        yield return null;

    }

    public void CamOn()
    {
        if (lockCam != null)
        {
            lockCam.Priority = 11;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Camera.main.cullingMask = oldMask & ~(1 << 15);
        }
    }

    public void CamOff()
    {
        if (lockCam != null)
        {
            lockCam.Priority = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Camera.main.cullingMask = oldMask | (1 << 15);
        }
    }
}
