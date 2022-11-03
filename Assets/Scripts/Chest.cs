using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private Text interactUI;
	private bool isInRange;

	public Animator animator;
	public int coinsToAdd;

	public AudioClip soundToPlay;

	private void Awake()
	{
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
		if (isInRange && Input.GetKeyDown(KeyCode.E))
		{
			OpenChest();
		}
	}

	private void OpenChest()
	{
		animator.SetTrigger("OpenChest");
		Inventory.instance.AddCoins(coinsToAdd);
		AudioManager.instance.PlayClipAt(soundToPlay, transform.position);
		GetComponent<BoxCollider2D>().enabled = false;
		interactUI.enabled = false;
		isInRange = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			interactUI.enabled = true;
			isInRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isInRange = false;
			interactUI.enabled = false;
		}
	}
}
