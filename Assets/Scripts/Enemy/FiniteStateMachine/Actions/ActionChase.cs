using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : FSMAction
{
    [SerializeField] private float chaseSpeed;
    private EnemyAI enemyAI;
    private bool playerIsDead = false;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }
    public override void Act()
    {
        ChasePlayer();
        if (playerIsDead) return;
    }

    private void ChasePlayer()
    {
        if (enemyAI.Player == null) return;
        Vector3 dirToPlayer = enemyAI.Player.position - transform.position;
        if (dirToPlayer.magnitude >= 1.3)
        {
            transform.Translate(dirToPlayer.normalized * (chaseSpeed * Time.deltaTime));
        }
    }


}
