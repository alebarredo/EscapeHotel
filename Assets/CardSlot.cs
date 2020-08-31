using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public Vector3 cardOffset;

    [Space]
    public string CardSlotId;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "KeyCard")
        {
            KeyCard keyCard = other.gameObject.GetComponent<KeyCard>();

            if(keyCard.Id == CardSlotId)
            {
                Grab pickable = other.gameObject.GetComponent<Grab>();
                pickable.OnRelease();
                pickable.Picked = false;
                pickable.Rigid.isKinematic = true;
                //IsPickable = false;

                other.gameObject.transform.SetParent(gameObject.transform);
                other.transform.position = gameObject.gameObject.transform.position + cardOffset;
                other.transform.rotation = gameObject.gameObject.transform.rotation;

                KeyCardManager keyCardManager = other.gameObject.GetComponentInParent<KeyCardManager>();
                keyCardManager.UpdateCredentials(keyCard.Id);
            }
          
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject)
    //}

}
