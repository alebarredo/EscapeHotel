using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class InteractionController : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Variables    
    [Space, Header("Data")]
    [SerializeField] private InteractionInputData interactionInputData = null;
    [SerializeField] private InteractionData interactionData = null;
    [SerializeField] private PickableInputData pickableInputData = null;
    [SerializeField] private PickableData pickableData = null;


    [Space, Header("UI")]
    [SerializeField] private InteractionUIPanel uiPanel;

    [Space, Header("Ray Settings")]
    [SerializeField] private float rayDistance = 0f;
    [SerializeField] private float raySphereRadius = 0f;
    [SerializeField] private LayerMask interactableLayer = ~0;


    #region Private
    private CinemachineVirtualCamera m_cam;

    private bool m_interacting;
    private bool m_holding;
    private float m_holdTimer = 0f;

    #endregion

    #endregion

    #region Built In Methods      
    void Awake()
    {
        if (!photonView.IsMine)
        {
            uiPanel.gameObject.SetActive(false);
            return;
        }
        else
        {
            m_cam = FindObjectOfType<CinemachineVirtualCamera>();
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        else
        {
            CheckForInteractable();
            CheckForInteractableInput();
            CheckForPickupInput();
        }

    }
    #endregion


    #region Custom methods         
    void CheckForInteractable()
    {
        Ray _ray = new Ray(m_cam.transform.position, m_cam.transform.forward);
        RaycastHit _hitInfo;

        bool _hitSomething = Physics.SphereCast(_ray, raySphereRadius, out _hitInfo, rayDistance, interactableLayer);

        if (_hitSomething)
        {

            InteractableBase _interactable = _hitInfo.transform.GetComponent<InteractableBase>();
            Pickable _pickable = _hitInfo.transform.GetComponent<Pickable>();

            if (_interactable != null)
            {
                if (interactionData.IsEmpty())
                {
                    interactionData.Interactable = _interactable;
                    uiPanel.SetTooltip(_interactable.TooltipMessage);
                }
                else
                {
                    if (!interactionData.IsSameInteractable(_interactable))
                    {
                        interactionData.Interactable = _interactable;
                        uiPanel.SetTooltip(_interactable.TooltipMessage);
                    }
                }
            }

            else if (_pickable != null)
            {
                if (pickableData.IsSamePickable(_pickable))
                {
                    return;
                }

                if (pickableData.IsEmpty())
                {
                    pickableData.PickableItem = _pickable;
                    uiPanel.SetTooltip(_pickable.TooltipMessage);

                }
                else
                {
                    if (!pickableData.IsSamePickable(_pickable))
                    {
                        pickableData.PickableItem = _pickable;
                        uiPanel.SetTooltip(_pickable.TooltipMessage);
                    }
                }
            }
        }
        else
        {
            uiPanel.ResetUI();
            interactionData.ResetData();
            pickableData.ResetData();
        }


        Debug.DrawRay(_ray.origin, _ray.direction * rayDistance, _hitSomething ? Color.green : Color.red);
    }

    void CheckForInteractableInput()
    {
        if (interactionData.IsEmpty())
            return;

        if (interactionInputData.InteractedClicked)
        {
            m_interacting = true;
            m_holdTimer = 0f;
        }

        if (interactionInputData.InteractedReleased)
        {
            m_interacting = false;
            m_holdTimer = 0f;
            uiPanel.UpdateProgressBar(0f);
        }

        if (m_interacting)
        {
            if (!interactionData.Interactable.IsInteractable)
                return;

            if (interactionData.Interactable.HoldInteract)
            {
                m_holdTimer += Time.deltaTime;

                float heldPercent = m_holdTimer / interactionData.Interactable.HoldDuration;
                uiPanel.UpdateProgressBar(heldPercent);

                if (heldPercent > 1f)
                {
                    interactionData.Interact();
                    m_interacting = false;
                }
            }
            else
            {
                interactionData.Interact();
                m_interacting = false;
            }
        }
    }

    void CheckForPickupInput()
    {
        if (pickableData.IsEmpty())
            return;

        if (pickableInputData.PickClicked || pickableInputData.PickHold)
        {
            m_holding = true;
        }

        if (pickableInputData.PickReleased)
        {
            m_holding = false;
        }

        if (m_holding)
        {
            if (!pickableData.PickableItem.IsPickable)
                return;

            if (!pickableData.PickableItem.Picked)
            {
                pickableData.Pick();
                m_holding = false;
            }
            else
            {
                pickableData.Release();
                m_holding = false;

            }
        }

    }
    #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(m_interacting);
        }
        else
        {
            // Network player, receive data
            this.m_interacting = (bool)stream.ReceiveNext();
        }
    }
}
