using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using BayatGames.SaveGameFree;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private Weapon initialWeapon;

    [SerializeField] private Transform[] attackPosition;
    [SerializeField] private PlayerStats stats;
    [Header("Melee Cofig")] [SerializeField]
    private ParticleSystem slashFX;
    
    [SerializeField] private float minDistanceMeleeAttack;
    public Weapon CurrentWeapon { get; set; }
    
    private PlayerActions actions;
    private PlayerAnimations playerAnimations;
    private Coroutine attackCoroutine;
    private PlayerMovement playerMovement;
    private PlayerMana playerMana;
    
    private Transform currentAttackPosition;
    private float currentAttackRotation;
    private float timer;

    private void Awake()
    {
        actions = new PlayerActions();
        playerMana = GetComponent<PlayerMana>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Start()
    {
        WeaponManager.Instance.EquipWeapon(initialWeapon);
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        GetFirePosition();
    }

    private void Attack()
    {
        if (playerMovement.isPaused)
        {
            return; 
        }
        if (attackCoroutine != null && timer < 0f)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = StartCoroutine(IEAttack());
    }

    private IEnumerator IEAttack()
    {
        if (timer <= 0)
        {
            if (currentAttackPosition == null) yield break;
            if (CurrentWeapon.weaponType == WeaponType.Magic)
            {
                if (playerMana.currentMana < CurrentWeapon.requiredMana) yield break;
                MagicAttack();
            }
            else
            {
                MeleeAttack();
            }

            playerAnimations.SetAttackAnimation(true);
            yield return new WaitForSeconds(0.1f);
            playerAnimations.SetAttackAnimation(false);
            timer = 0.5f;
        }
    }

    private void MagicAttack()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));
        Projectile projectile = Instantiate(CurrentWeapon.projectilePrefab, currentAttackPosition.position, rotation);
        projectile.Direction = Vector3.up;
        projectile.Damage = GetAttackDamage();
        playerMana.UseMana(CurrentWeapon.requiredMana);
    }

    private void MeleeAttack()
    {
        slashFX.transform.position = currentAttackPosition.position;
        slashFX.Play();
        slashFX.GetComponent<ParticleFX>().Damage = CurrentWeapon.damage;
        // float currentDistanceToEnemy = Vector3.Distance(enemyTarget.transform.position, transform.position);
        // if (currentDistanceToEnemy <= minDistanceMeleeAttack)
        // {

        // }
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        CurrentWeapon = newWeapon;
        stats.TotalDamage = stats.BaseDamage + CurrentWeapon.damage;
    }

    private float GetAttackDamage()
    {
        float damage = stats.BaseDamage;
        damage += CurrentWeapon.damage;
        float randomPercent = Random.Range(0f, 100);
        if (randomPercent <= stats.CriticalChance)
        {
            damage += damage * (stats.CriticalChance / 100f);
        }

        return damage;
    }

    private void GetFirePosition()
    {
        Vector2 moveDirection = playerMovement.MoveDirection;
        switch (moveDirection.x)
        {
            case > 0f:
                currentAttackPosition = attackPosition[2];
                currentAttackRotation = -90f;
                break;
            case < 0f:
                currentAttackPosition = attackPosition[3];
                currentAttackRotation = -270f;
                break;
        }

        switch (moveDirection.y)
        {
            case > 0f:
                currentAttackPosition = attackPosition[0];
                currentAttackRotation = 0f;
                break;
            case < 0f:
                currentAttackPosition = attackPosition[1];
                currentAttackRotation = -180f;
                break;
        }
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}