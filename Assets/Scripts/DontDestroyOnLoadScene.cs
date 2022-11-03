using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
	public GameObject[] objects;

	public static DontDestroyOnLoadScene instance;

	void Awake()
	{
		foreach (var element in objects)
		{
			DontDestroyOnLoad(element);
		}

		if (instance != null)
		{
			Debug.LogWarning("Il y a plus d'une instance de DontDestroyOnLoadScene dans la scène");
			return;
		}

		instance = this;
	}

	public void RemoveFromDontDestroyOnLoad()
	{
		foreach (var element in objects)
		{
			SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
		}
	}
}
