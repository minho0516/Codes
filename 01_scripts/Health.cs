using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionFab;
    public delegate void OnHealthChange(int prev, int now);

    public OnHealthChange HealthChangeEvent;

    public int maxHealth;
    public int currentHealth;

    private bool _isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        HealthChangeEvent?.Invoke(0, maxHealth);
    }

    public void OnDamage(int damage)
    {
        if(_isDead) return;


        int before = currentHealth;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if(before != currentHealth)
        {
            HealthChangeEvent?.Invoke(before, currentHealth);
        }

        if(currentHealth <= 0)
        {
            _isDead = true;
            Debug.Log("Die");
            StartCoroutine(DeadProcess());
        }
    }

    private IEnumerator DeadProcess()
    {
        for(int i = 0; i< 8; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * 1.2f;
            ParticleManager.Instance.CreateParticle();
            float waitTime = Random.Range(0.01f, 0.15f);
            yield return new WaitForSeconds(waitTime);
        }


        Destroy(gameObject);
    }
}
