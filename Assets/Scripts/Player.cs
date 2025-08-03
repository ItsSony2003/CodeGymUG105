using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerInputSet input { get; private set; }
    public Vector2 moveInput { get; private set; }

    public float moveDuration = 0.1f;
    public float gridSize = 1f;

    private bool isMoving = false;
    private Vector3 targetPosition;

    private bool isDead = false;
    public static bool gameStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        input = new PlayerInputSet();
    }

    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();

    private void Start()
    {
        /*//input.Player.Movement.performed += ctx =>
        //{
        //    if (isMoving) return;

        //    Vector2 rawInput = ctx.ReadValue<Vector2>();
        //    Vector2 direction = Vector2.zero;

        //    // Snap to one axis
        //    if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
        //        direction = new Vector2(Mathf.Sign(rawInput.x), 0);
        //    else if (rawInput != Vector2.zero)
        //        direction = new Vector2(0, Mathf.Sign(rawInput.y));

        //    if (direction != Vector2.zero)
        //        TryMove(direction);
        //};

        //input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;*/

        int obstacleLayer = LayerMask.GetMask("Obstacle");

        input.Player.Movement.performed += ctx =>
        {   
            Vector2 rawInput = ctx.ReadValue<Vector2>();
            Vector2 direction = Vector2.zero;

            // Snap input to axis
            if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
            {
                direction = new Vector2(Mathf.Sign(rawInput.x), 0);
            }
            else if (rawInput != Vector2.zero)
            {
                direction = new Vector2(0, Mathf.Sign(rawInput.y));
            }

            // START THE GAME only if pressing forward (W or up)
            if (!gameStarted && direction == Vector2.up)
            {
                gameStarted = true;
                CameraFollowScript.lastPlayerMoveTime = Time.time;
            }

            Vector3 origin = transform.position + Vector3.up * 0.5f;
            Vector3 rayDirection = new Vector3(direction.x, 0, direction.y);

            if (Physics.Raycast(origin, rayDirection, out RaycastHit hit, 1f, obstacleLayer))
            {
                return;
            }


            if (!gameStarted || isMoving || direction == Vector2.zero)
                return;

            TryMove(direction);
        };
    }

    private void Update()
    {
        if (!isDead && transform.position.z < Camera.main.transform.position.z + 0.5f)
        {
            Death();
        }
    }


    private void Death()
    {
        isDead = true;
        GameManager.instance.EndGame();
    }

    private void TryMove(Vector2 direction)
    {
        if (isMoving) return;

        // Get Player last move input
        CameraFollowScript.lastPlayerMoveTime = Time.time;

        // Get movement vector
        Vector3 moveDir = new Vector3(direction.x, 0f, direction.y);
        targetPosition = transform.position + moveDir * gridSize;
        // Start smooth rotation and movement
        StartCoroutine(RotateTowards(moveDir));
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPosition, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        isMoving = false;
    }

    private IEnumerator RotateTowards(Vector3 direction)
    {
        if (direction == Vector3.zero) yield break;

        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.LookRotation(direction);
        float t = 0f;

        while (t < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            t += Time.deltaTime * 10f; // Adjust speed if needed
            yield return null;
        }

        transform.rotation = targetRot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Death();
        }
    }
}
