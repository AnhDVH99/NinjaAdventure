using UnityEngine;
using UnityEngine.Pool;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Config")] 
    [SerializeField] private float health;

    private Animator _animator;
    private EnemyAI _enemyAI;
    private EnemyLoot enemyLoot;
    // private IObjectPool<EnemyHealth> enemyPool;

    // public void SetPool(IObjectPool<EnemyHealth> pool)
    // {
    //     enemyPool = pool;
    // }

    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyAI = GetComponent<EnemyAI>();
        enemyLoot = GetComponent<EnemyLoot>();
    }

    private void Update()
    {
        CurrentHealth = health;
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0f)
        {
            health = 0f;
            DisableEnemy();
            QuestManager.Instance.AddProgress("Hunt2Monster", 1);
            QuestManager.Instance.AddProgress("Hunt5Monster", 1);
            QuestManager.Instance.AddProgress("Hunt10Monster", 1);
        }
        else
        {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }
    

    public void DisableEnemy()
    {
        _animator.SetTrigger("Dead");
        _enemyAI.enabled = false;
        gameObject.tag = "Loot";
        GameManager.Instance.AddPlayerExp(enemyLoot.ExpDrop);
        // enemyPool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LootManager.Instance.ShowLootPanel(enemyLoot);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LootManager.Instance.CloseLootPanel();
        }
    }
}