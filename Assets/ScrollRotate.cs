using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRotate : MonoBehaviour
{
    public bool horizontal;
    public bool vertical;
    public float scrollSpeed;

    void Update()
    {
        if (vertical)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                StartCoroutine(scrollRotate(Vector3.up));

            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                StartCoroutine(scrollRotate(Vector3.down));
            }
        }
        else if (horizontal)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                StartCoroutine(scrollRotate(Vector3.left));

            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                StartCoroutine(scrollRotate(Vector3.right));
            }
        }
       
    }

    IEnumerator scrollRotate(Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit) && hit.collider.tag == "scrollRotator")
        {
            hit.collider.transform.Rotate(direction * scrollSpeed, Space.Self);

            //if ()
            //{
            //    Unlocked.Invoke();
            //    doorInteract.locked = false;
            //}

        }

        yield return null;

    }
}
