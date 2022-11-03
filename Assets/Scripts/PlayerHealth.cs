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
			Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la sc�ne");
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

			//v�rifier si le joueur est toujours vivant
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

		//Jouer l'animation d'�limination
		PlayerMovement.instance.animator.SetTrigger("IsDeath");

		//Emp�cher les interactions physiques avec les autres �l�ments de la sc�ne
		PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
		PlayerMovement.instance.rb.velocity = Vector3.zero;
		PlayerMovement.instance.playerCollider.enabled = false;

		GameOverManager.instance.OnPlayerDeath();
	}

	public void Respawn()
	{
		//Reactiver les mouvements du personnage
		PlayerMovement.instance.enabled = true;

		//Desactiver l'animation d'�limination et r�activer celle par d�faut
		PlayerMovement.instance.animator.SetTrigger("Respawn");

		//Reactiver les interactions physiques avec les autres �l�ments de la sc�ne
		PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
		PlayerMovement.instance.playerCollider.enabled = true;

		//Redonner de la vie au personnage
		currentHealth = maxHealth;
		healthBar.SetHealth(currentHealth);

		//Reinitianiser le nombre de pi�ces
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
