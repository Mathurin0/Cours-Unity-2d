using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
	public static bool gameIsPaused = false;
	public static bool isSettingsWindowOpen = false;

	public GameObject pauseMenuUI;
	public GameObject settingsWindow;

	public static PauseMenu instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
			return;
		}

		instance = this;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !isSettingsWindowOpen)
		{
			if (gameIsPaused == true)
			{
				Resume();
			}
			else
			{
				Paused();
			}
		}
	}

	void Paused()
	{
		// on désactive l'instance de PlayerMovement pour qu'il ne recoive pas les appuis clavier
		PlayerMovement.instance.enabled = false;
		// activer notre menu pause / l'afficher
		pauseMenuUI.SetActive(true);
		// arrêter le temps
		Time.timeScale = 0;
		// changer le statut du jeu
		gameIsPaused = true;
	}

	public void Resume()
	{
		// l'inverse de la fonction Paused()
		PlayerMovement.instance.enabled = true;
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1;
		gameIsPaused = false;
	}

	public void SettingsButton()
	{
		settingsWindow.SetActive(true);
		isSettingsWindowOpen = true;
	}

	public void CloseSettingsWindow()
	{
		settingsWindow.SetActive(false);
		isSettingsWindowOpen = false;
	}

	public void LoadMainMenu()
	{
		DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
		Resume();
		SceneManager.LoadScene("MainMenu");
	}
}
