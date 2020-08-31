using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
    public Vector3 distance;
    public float speed;

    public void Translate()
    {
        StartCoroutine(LerpTranslate(distance));
    }

    public void Untranslate()
    {
        StartCoroutine(LerpTranslate(-distance));
    }

    // Update is called once per frame
    private IEnumerator LerpTranslate(Vector3 distance)
    {
        Vector3 start = gameObject.transform.position;

        gameObject.transform.position = Vector3.Lerp(start, start + distance, speed);

        yield return null;

    }
}
