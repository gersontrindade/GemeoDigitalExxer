using System;
using TMPro;
using UnityEngine;

public class SCRIPT_FPS_COUNTER : MonoBehaviour
{
	public bool FPS_MOSTRAR;

	public bool FPS_LIMITAR;

	public int FPS_LIMITE;

	private int m_frameCounter;

	private float m_timeCounter;

	private float m_lastFramerate;

	public float m_refreshTime = 0.5f;

	private TextMeshPro texto;

	public GUISkin skin_1;

	private void Start()
	{
		if (FPS_LIMITAR)
		{
			Application.targetFrameRate = FPS_LIMITE;
		}
	}

	private void Update()
	{
		if (FPS_MOSTRAR)
		{
			if (m_timeCounter < m_refreshTime)
			{
				m_timeCounter += Time.deltaTime;
				m_frameCounter++;
			}
			else
			{
				m_lastFramerate = (float)m_frameCounter / m_timeCounter;
				m_frameCounter = 0;
				m_timeCounter = 0f;
			}
			m_lastFramerate = (float)Math.Round(m_lastFramerate, 0);
		}
	}

	private void OnGUI()
	{
		if (FPS_MOSTRAR)
		{
			GUI.skin = skin_1;
			GUI.Box(new Rect(Screen.width - 100, 0f, 50f, 25f), "FPS\n" + m_lastFramerate);
		}
	}
}
