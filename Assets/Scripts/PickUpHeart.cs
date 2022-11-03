using UnityEngine;
using System.Collections;

public class PickUpHeart : MonoBehaviour
{
	public Animator HeartBroke;
	private int healthAmount = 20;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			StartCoroutine(AnimationBeforeToDisapear());
		}
	}

	public IEnumerator AnimationBeforeToDisapear()
	{
		if(PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth)
		{
			HeartBroke.SetTrigger("pickedUp");
			yield return new WaitForSeconds(.2f);
			PlayerHealth.instance.HealPlayer(healthAmount);
			Destroy(gameObject);
		}	
	}
}
