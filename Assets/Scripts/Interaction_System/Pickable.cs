using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody))]
public class Pickable : MonoBehaviour, IPickable
{
    #region Variables    
    [Space, Header("Pickable Settings")]

    [Space]
    [SerializeField] private bool multipleUse = true;
    [SerializeField] private bool isPickable = true;

    [SerializeField] public string tooltipMessage = "interact";

    [SerializeField] public float distance;
    [SerializeField] public float smoothing;
    #endregion


    #region Properties    
    private Rigidbody rigid;
    public Rigidbody Rigid { get => rigid; set => rigid = value; }
    public bool Picked { get; set; }
    public bool IsPickable => isPickable;
    public string TooltipMessage => tooltipMessage;
    #endregion

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    #region Methods
    public virtual void OnHold()
    {
        Debug.Log("HOLDING: " + gameObject.name);
    }

    public virtual void OnPickUp()
    {
        Debug.Log("PICKING: " + gameObject.name);
    }

    public virtual void OnRelease()
    {
        Debug.Log("DROPPING: " + gameObject.name);
    }

    //public virtual void OnPlace()
    //{
    //    Debug.Log("PLACED: " + gameObject.name);
    //}
    #endregion
}