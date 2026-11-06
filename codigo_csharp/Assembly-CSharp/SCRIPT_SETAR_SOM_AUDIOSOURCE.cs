using UnityEngine;

public class SCRIPT_SETAR_SOM_AUDIOSOURCE : MonoBehaviour
{
	public AudioClip audio_1;

	private void Start()
	{
		GetComponent<AudioSource>().clip = audio_1;
	}
}
