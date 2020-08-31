using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    TriangleExplosion explosion;
    public float collisionMagnitude;
    
    // Start is called before the first frame update
    void Awake()
    {
        explosion = GetComponent<TriangleExplosion>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > collisionMagnitude)
        {
            explosion.StartCoroutine("SplitMesh");
        }
    }
}
