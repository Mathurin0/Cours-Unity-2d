using UnityEngine;

public class WeakSpot : MonoBehaviour
{
	public GameObject objectToDestroy;
	public GameObject player;
	public float force = 400;
	public AudioClip killSound;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			AudioManager.instance.PlayClipAt(killSound, transform.position);
			Destroy(objectToDestroy);
			GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().AddForce(transform.up * force);
		}
	}
}
