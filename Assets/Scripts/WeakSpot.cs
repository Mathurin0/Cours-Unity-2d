using UnityEngine;

public class WeakSpot : MonoBehaviour
{
	public GameObject objectToDestroy;
	public GameObject player;
	public float force = 400;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Destroy(objectToDestroy);
			GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().AddForce(transform.up * force);
		}
	}
}
