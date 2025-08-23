using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class FreezableObstacle : MonoBehaviour, IFreezable
{
    [Header("Disable these while frozen (e.g., ObstacleMover, PathFollower)")]
    [SerializeField] private List<Behaviour> disableWhileFrozen = new();

    private Animator animator;
    private Rigidbody rb;

    private float savedAnimatorSpeed = 1f;
    private Vector3 savedVel, savedAngVel;
    private RigidbodyConstraints savedConstraints;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() => FreezeManager.Register(this);
    private void OnDisable() => FreezeManager.Unregister(this);

    public void OnFreeze()
    {
        // disable movement scripts
        foreach (var b in disableWhileFrozen)
            if (b) b.enabled = false;

        // pause animator
        if (animator)
        {
            savedAnimatorSpeed = animator.speed;
            animator.speed = 0f;
        }

        // pause rigidbody
        if (rb)
        {
            savedVel = rb.velocity;
            savedAngVel = rb.angularVelocity;
            savedConstraints = rb.constraints;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void OnUnfreeze()
    {
        // re-enable movement scripts
        foreach (var b in disableWhileFrozen)
            if (b) b.enabled = true;

        // resume animator
        if (animator)
            animator.speed = savedAnimatorSpeed;

        // resume rigidbody
        if (rb)
        {
            rb.constraints = savedConstraints;
            rb.velocity = savedVel;
            rb.angularVelocity = savedAngVel;
        }
    }

    // Helper if you want the pool to auto-fill this list
    public void SetDisableList(List<Behaviour> behaviours)
    {
        disableWhileFrozen = behaviours;
    }
}
