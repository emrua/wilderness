using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform target; // The player or target the bear chases
    public float speed = 5f; // Movement speed
    public float attackRange = 2f; // Range to trigger attacks
    public Animator animator; // Reference to the Animator component
    public int attackDamage = 10; // Damage per attack

    private List<Node> path; // Path provided by A* algorithm
    private int currentPathIndex;
    private int currentAttackIndex = 1; // Tracks the current attack animation
    private bool isAttacking = false; // Prevents overlapping attacks

    private PlayerState playerState; // Reference to the PlayerState script

    private void Start()
    {
        if (target != null)
        {
            playerState = target.GetComponent<PlayerState>();
            if (playerState == null)
            {
                Debug.LogError("Target does not have a PlayerState component!");
            }
        }
    }

    private void Update()
    {
        if (path != null && path.Count > 0 && !isAttacking)
        {
            // Pathfinding movement logic
            Vector3 nextPosition = path[currentPathIndex].worldPosition;
            Vector3 direction = (nextPosition - transform.position).normalized;

            // Move the bear
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

            // Face the movement direction
            if (direction != Vector3.zero)
                transform.forward = direction;

            // Advance to the next node if close enough
            if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
            {
                currentPathIndex++;
                if (currentPathIndex >= path.Count)
                {
                    path = null;
                }
            }

            // Update animation parameters for movement
            animator.SetBool("WalkForward", true);
        }
        else
        {
            // Idle if no path is left
            animator.SetBool("WalkForward", false);
        }

        // Check for attack range
        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange && !isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    public void SetPath(List<Node> newPath)
    {
        path = newPath;
        currentPathIndex = 0;
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        // Trigger the current attack animation
        animator.SetTrigger($"Attack{currentAttackIndex}");
        Debug.Log($"Bear performs Attack{currentAttackIndex}");

        // Deal damage to the player
        if (playerState != null)
        {
            playerState.TakeDamage(attackDamage);
            Debug.Log($"Player takes {attackDamage} damage!");
        }

        // Get the animation length dynamically
        float animationLength = GetAnimationLength($"Attack{currentAttackIndex}");
        yield return new WaitForSeconds(animationLength);

        // Cycle to the next attack animation
        currentAttackIndex++;
        if (currentAttackIndex > 5) // Assuming there are 5 attack animations
        {
            currentAttackIndex = 1;
        }

        isAttacking = false;
    }

    private float GetAnimationLength(string animationName)
    {
        // Retrieve the runtime animation clip by name
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning($"Animation {animationName} not found!");
        return 1f; // Default fallback duration
    }
}
//comment
