using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickableData", menuName = "InteractionSystem/PickableData")]
public class PickableData : ScriptableObject
{
    private Pickable pickable;

    public Pickable PickableItem
    {
        get => pickable;
        set => pickable = value;
    }

    public void Pick()
    {
        pickable.OnPickUp();
    }

    public void Hold()
    {
        pickable.OnHold();
    }

    public void Release()
    {
        pickable.OnRelease();
    }

    public bool IsSamePickable(Pickable _newPickable) => pickable == _newPickable;
    public bool IsEmpty() => pickable == null;
    public void ResetData() => pickable = null;
}