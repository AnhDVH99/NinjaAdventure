using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRangedAttack : FSMAction
{
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletParent;

    private Player player;
    private EnemyAI enemyAI;
    private float timer;

    private void Awake()
    {
        player = GetComponent<Player>();
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
        if (timer <= 0f)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            timer = timeBetweenAttacks;
        }
    }
}