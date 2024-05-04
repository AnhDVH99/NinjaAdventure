using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DecisionDetectPlayer : FSMDecision
{
    [Header("Config")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerMask;
    private bool isDead;

    private EnemyAI enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyAI>();
    }

    public override bool Decide()
    {
        return DetectPlayer();
    }

    private bool DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(enemy.transform.position, range, playerMask);
        if(playerCollider != null)
        {
            enemy.Player = playerCollider.transform;
            return true;
        }

        enemy.Player = null;
        GetComponent<DecisionDetectPlayer>().enabled = false;
        return false;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
