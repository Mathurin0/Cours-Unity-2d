using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	public int coinsCount;
	public Text CoinsCountText;

	public static Inventory instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
			return;
		}

		instance = this;
	}

	public void AddCoins(int count)
	{
		coinsCount += count;
	    CoinsCountText.text = coinsCount.ToString();
	}

	public void RemoveCoins(int count)
	{
		coinsCount -= count;
		CoinsCountText.text = coinsCount.ToString();
	}
}
