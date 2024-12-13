using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }
    public float currentHealth;
    public float maxHealth;

    public float currentCalories;
    public float maxCalories;

    public float distanceTravelled = 0;

    Vector3 lastPosition;

    public GameObject playerBody;

    public float currentHydration;
    public float maxHydration;
    public bool isHydrationActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydration = maxHydration;

        StartCoroutine(decreaseHydration());
    }

    IEnumerator decreaseHydration()
    {
        while (true)
        {
            currentHydration -= 1;
            yield return new WaitForSeconds(10);
        }
    }

    void Update()
    {
        // Test calories
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if (distanceTravelled >= 5)
        {
            distanceTravelled = 0;
            currentCalories -= 1;
        }

        // Test health
        if (Input.GetKeyDown(KeyCode.N))
        {
            TakeDamage(10); // Simulate damage with N key
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");
    }

    public void Die()
    {
        Debug.Log("Player has died!");
        // Add any additional death logic (e.g., game over screen, respawn, etc.)
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log($"Player healed by {amount}. Current health: {currentHealth}");
    }

    public void setHealth(float newHealth)
    {
        currentHealth = newHealth;
    }

    public void setCalories(float newCalories)
    {
        currentCalories = newCalories;
    }

    public void setHydration(float newHydration)
    {
        currentHydration = newHydration;
    }
}
