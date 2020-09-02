using UnityEngine;
using NaughtyAttributes;
using Photon.Pun;

public class InputHandler : MonoBehaviourPun
{
    #region Data
    [Space, Header("Input Data")]
    [SerializeField] private CameraInputData cameraInputData = null;
    [SerializeField] private MovementInputData movementInputData = null;
    [SerializeField] private InteractionInputData interactionInputData = null;
    [SerializeField] private PickableInputData pickableInputData = null;
    #endregion

    #region BuiltIn Methods
    void Start()
    {
        if (photonView.IsMine)
        {
            cameraInputData.ResetInput();
            movementInputData.ResetInput();
            interactionInputData.ResetInput();
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            GetCameraInput();
            GetMovementInputData();
            GetInteractionInputData();
        }
    }
    #endregion

    #region Custom Methods
    void GetInteractionInputData()
    {
        interactionInputData.InteractedClicked = Input.GetKeyDown(KeyCode.E);
        interactionInputData.InteractedReleased = Input.GetKeyUp(KeyCode.E);
        pickableInputData.PickClicked = Input.GetMouseButtonDown(0);
        pickableInputData.PickReleased = Input.GetMouseButtonDown(1);
        pickableInputData.PickHold = Input.GetKeyDown(KeyCode.E);
    }

    void GetCameraInput()
    {
        cameraInputData.InputVectorX = Input.GetAxis("Mouse X");
        cameraInputData.InputVectorY = Input.GetAxis("Mouse Y");

        cameraInputData.ZoomClicked = Input.GetMouseButtonDown(1);
        cameraInputData.ZoomReleased = Input.GetMouseButtonUp(1);
    }

    void GetMovementInputData()
    {
        movementInputData.InputVectorX = Input.GetAxisRaw("Horizontal");
        movementInputData.InputVectorY = Input.GetAxisRaw("Vertical");

        movementInputData.RunClicked = Input.GetKeyDown(KeyCode.LeftShift);
        movementInputData.RunReleased = Input.GetKeyUp(KeyCode.LeftShift);

        if (movementInputData.RunClicked)
            movementInputData.IsRunning = true;

        if (movementInputData.RunReleased)
            movementInputData.IsRunning = false;

        movementInputData.JumpClicked = Input.GetKeyDown(KeyCode.Space);
        movementInputData.CrouchClicked = Input.GetKeyDown(KeyCode.C);
    }
    #endregion
}