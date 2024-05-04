using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionAttackRange : FSMDecision
{
    [Header("Config")]
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerMask;

    private EnemyAI enemyAI;
    

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }


    public override bool Decide()
    {
       return PlayerInRangeOfAttack();
    }

    private bool PlayerInRangeOfAttack()
    {
        if (enemyAI.Player == null) return false;
        Collider2D playerCollider = Physics2D.OverlapCircle(enemyAI.transform.position, attackRange, playerMask);
        if(playerCollider != null)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
