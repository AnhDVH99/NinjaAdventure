using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttack : FSMAction
{
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenAttacks;
    private EnemyAI enemyAI;
    private float timer;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }
    public override void Act()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {

        if (enemyAI.Player == null) return;
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            IDamageable player = enemyAI.Player.GetComponent<IDamageable>();
            player.TakeDamage(damage);
            timer = timeBetweenAttacks;   
        }
    }

}
