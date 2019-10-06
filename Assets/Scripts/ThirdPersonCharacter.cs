using UnityEngine;
using InputManager;

public class ThirdPersonCharacter : MonoBehaviour
{
    [SerializeField]
    float GroundCheckDistance = 0.2f;
    
    private Animator _animator;
    private Transform _transform;
    
    public bool isGrounded = true;
    private float OrigGroundCheckDistance;

    private float TurnAmount;
    private float ForwardAmount;
    private Vector3 GroundNormal;

    private Transform cam;                  // A reference to the main camera in the scenes transform
    private Vector3 camForward;             // The current forward direction of the camera
                                            // the world-relative desired move direction, calculated from the camForward and user input.

    private RaycastHit hitInfo = new RaycastHit();

    [Header("Animator values speed")]
    [Range(0.0f, 0.5f), SerializeField]
    private float ForwardAmountSpeed = 0.45f;
    [Range(0.0f, 0.5f), SerializeField]
    private float TurnAmountSpeed = 0.1f;
    
    public LayerMask layerMask;
    
    void Start()
    {
        _transform = transform;
        
        if (Camera.main != null) cam = Camera.main.transform;
        else Debug.LogWarning("Warning: no main camera found");
        
        _animator = GetComponent<Animator>();
        
        OrigGroundCheckDistance = GroundCheckDistance;
    }
    
    private void FixedUpdate()
    {
        float h = IM.AxisH;
        float v = IM.AxisV;

        Vector3 move;
        
        if (cam != null)
        {
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            move = v * camForward + h * cam.right;
        }
        else
        {
            move = v * Vector3.forward + h * Vector3.right;
        }

        // Ñonvert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired
        // direction.
        if (move.magnitude > 1f) move.Normalize();
        
        Move(move);
    }
    
    public void Move(Vector3 move)
    {
        move = _transform.InverseTransformDirection(move);
        
        CheckGroundStatus();

        move = Vector3.ProjectOnPlane(move, GroundNormal);

        TurnAmount = Mathf.Atan2(move.x, move.z);

        if (IM.AxisV != 0 || IM.AxisH != 0)
        {
            ForwardAmount = 1;
        }
        else
        {
            ForwardAmount = 0;
        }
        
        if (IM.AxisH != 0 || IM.AxisV != 0)
        {
            if (IM.Walk)
            {
                ForwardAmount *= 0.5f;
            }
            else if (IM.Sprint)
            {
                ForwardAmount *= 1.5f;
            }
        }
        
        UpdateAnimator(move);
    }
    
    void UpdateAnimator(Vector3 move)
    {
        _animator.SetFloat("Forward", ForwardAmount, ForwardAmountSpeed, Time.smoothDeltaTime);
        _animator.SetFloat("Turn", TurnAmount, TurnAmountSpeed, Time.smoothDeltaTime);
        
        _animator.SetBool("OnGround", isGrounded);
    }
    
    void CheckGroundStatus()
    {
        if (Physics.Raycast(_transform.position + Vector3.up * 0.25f, Vector3.down, out hitInfo, GroundCheckDistance, layerMask))
        {
            GroundNormal = hitInfo.normal;
            isGrounded = true;
            
            GroundCheckDistance = OrigGroundCheckDistance;
        }
        else
        {
            GroundNormal = Vector3.up;
            isGrounded = false;
            
            GroundCheckDistance = 0.35f;
        }
    }
}
