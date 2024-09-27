using UnityEngine;
using UnityEngine.Serialization;

public class InputHandler : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    public float MoveAmount;
    public float MouseX;
    public float MouseY;

    private PlayerControls _inputActions;

    private Vector2 _movementInput;
    private Vector2 _cameraInput;

    public void OnEnable()
    {
        if (_inputActions is null)
        {
            _inputActions = new PlayerControls();
            
            _inputActions.PlayerMovement.Movement.performed +=
                inputActions => _movementInput = inputActions.ReadValue<Vector2>();
            
            _inputActions.PlayerMovement.Camera.performed +=
                inputActions => _cameraInput = inputActions.ReadValue<Vector2>();
        }
        
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
    }

    private void MoveInput(float delta)
    {
        Horizontal = _movementInput.x;
        Vertical = _movementInput.y;

        MoveAmount = Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));
        MouseX = _cameraInput.x;
        MouseY = _cameraInput.y;
    }
}
