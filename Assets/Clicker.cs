using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (up)
            {
                StartCoroutine(scrollRotate(Vector3.up));
            }
            else if (down)
            {
                StartCoroutine(scrollRotate(Vector3.down));
            }
            else if (left)
            {
                StartCoroutine(scrollRotate(Vector3.left));
            }
            else if (right)
            {
                StartCoroutine(scrollRotate(Vector3.right));
            }
        }
    }

    IEnumerator scrollRotate(Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit) && hit.collider.tag == "clicker")
        {
            hit.rigidbody.AddForce(direction, ForceMode.Force);

            //if ()
            //{
            //    Unlocked.Invoke();
            //    doorInteract.locked = false;
            //}
        }

        yield return null;

    }
}
