using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    Rigidbody Rigid { get; set; }

    void OnPickUp();
    void OnHold();
    void OnRelease();
    //void OnPlace();

    string TooltipMessage { get; }

}