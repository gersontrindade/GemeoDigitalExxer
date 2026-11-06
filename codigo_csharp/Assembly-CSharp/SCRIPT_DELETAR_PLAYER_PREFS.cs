using UnityEngine;

public class SCRIPT_DELETAR_PLAYER_PREFS : MonoBehaviour
{
	private void Start()
	{
		PlayerPrefs.DeleteAll();
	}
}
