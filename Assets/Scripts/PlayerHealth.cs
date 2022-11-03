using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;

	public float invincibilityTimeAfterHeat = 2f;
	public float invicibilityFlashDelay = 0.15f;
	public HealthBar healthBar;
	
	public SpriteRenderer graphics;
	public bool isInvicible = false;

	public static PlayerHealth instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
			return;
		}

		instance = this;
	}

	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
			TakeDamage(50);
		}
	}

	public void TakeDamage(int damage)
	{
		if (isInvicible == false)
		{
			currentHealth -= damage;
			healthBar.SetHealth(currentHealth);

			//vérifier si le joueur est toujours vivant
			if(currentHealth <= 0)
			{
				Die();
				return;
			}

			isInvicible = true;
			StartCoroutine(InvicibilityFlash());
			StartCoroutine(HandleInvicibilityDelay());
		}
	}

	public void Die()
	{
		//Bloquer les mouvements du personnage
		PlayerMovement.instance.enabled = false;

		//Jouer l'animation d'élimination
		PlayerMovement.instance.animator.SetTrigger("IsDeath");

		//Empêcher les interactions physiques avec les autres éléments de la scène
		PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
		PlayerMovement.instance.rb.velocity = Vector3.zero;
		PlayerMovement.instance.playerCollider.enabled = false;

		GameOverManager.instance.OnPlayerDeath();
	}

	public void Respawn()
	{
		//Reactiver les mouvements du personnage
		PlayerMovement.instance.enabled = true;

		//Desactiver l'animation d'élimination et réactiver celle par défaut
		PlayerMovement.instance.animator.SetTrigger("Respawn");

		//Reactiver les interactions physiques avec les autres éléments de la scène
		PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
		PlayerMovement.instance.playerCollider.enabled = true;

		//Redonner de la vie au personnage
		currentHealth = maxHealth;
		healthBar.SetHealth(currentHealth);

		//Reinitianiser le nombre de pièces
		Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);
	}

	public void HealPlayer(int health)
	{
		if(currentHealth + health > maxHealth)
		{
			currentHealth = maxHealth;
		}
		else
		{
			currentHealth += health;
		}

		healthBar.SetHealth(currentHealth);
	}

	public IEnumerator InvicibilityFlash()
	{
		while (isInvicible)
		{
			graphics.color = new Color(1f, 1f, 1f, 0.1f);
			yield return new WaitForSeconds(invicibilityFlashDelay);
			graphics.color = new Color(1f, 1f, 1f, 1f);
			yield return new WaitForSeconds(invicibilityFlashDelay);
		}
	}

	public IEnumerator HandleInvicibilityDelay()
	{
		yield return new WaitForSeconds(invincibilityTimeAfterHeat);
		isInvicible = false;
	}
}
