using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CAMERA_LOCALIZACAO : MonoBehaviour
{
	[Space]
	[SerializeField]
	private bool _active = true;

	[Space]
	[SerializeField]
	private bool _enableRotation = true;

	[SerializeField]
	private float _mouseSense = 1.8f;

	[Space]
	[SerializeField]
	private bool _enableTranslation = true;

	[SerializeField]
	private float _translationSpeed = 55f;

	[Space]
	[SerializeField]
	private bool _enableMovement = true;

	[SerializeField]
	private float _movementSpeed = 10f;

	[SerializeField]
	private float _boostedSpeed = 50f;

	[Space]
	[SerializeField]
	private bool _enableSpeedAcceleration = true;

	[SerializeField]
	private float _speedAccelerationFactor = 1.5f;

	[Space]
	[SerializeField]
	private KeyCode _initPositonButton = KeyCode.R;

	public bool LIMITAR_LOCALIZACAO;

	public float LIMITE_POSICAO_X_MINIMO;

	public float LIMITE_POSICAO_X_MAXIMO;

	public float LIMITE_POSICAO_Y_MINIMO;

	public float LIMITE_POSICAO_Y_MAXIMO;

	public float LIMITE_POSICAO_Z_MINIMO;

	public float LIMITE_POSICAO_Z_MAXIMO;

	public float t = 0.5f;

	public float startFOV = 60f;

	public float minFOV = 60f;

	public float maxFOV = 90f;

	public Camera myCamera;

	public float ZOOM_DESEJADO;

	public float ZOOM_VELOCIDADE = 10f;

	private CursorLockMode _wantedMode;

	private float _currentIncrease = 1f;

	private float _currentIncreaseMem;

	private Vector3 _initPosition;

	private Vector3 _initRotation;

	private void Start()
	{
		_initPosition = base.transform.position;
		_initRotation = base.transform.eulerAngles;
	}

	private void SetCursorState()
	{
		Input.GetKeyDown(KeyCode.Escape);
		if (Input.GetMouseButtonDown(0))
		{
			_wantedMode = CursorLockMode.None;
		}
		Cursor.lockState = _wantedMode;
		Cursor.visible = true;
	}

	private void CalculateCurrentIncrease(bool moving)
	{
		_currentIncrease = Time.deltaTime;
		if (!_enableSpeedAcceleration || (_enableSpeedAcceleration && !moving))
		{
			_currentIncreaseMem = 0f;
			return;
		}
		_currentIncreaseMem += Time.deltaTime * (_speedAccelerationFactor - 1f);
		_currentIncrease = Time.deltaTime + Mathf.Pow(_currentIncreaseMem, 3f) * Time.deltaTime;
	}

	private void Update()
	{
		if (LIMITAR_LOCALIZACAO)
		{
			if (base.transform.position.x <= LIMITE_POSICAO_X_MINIMO)
			{
				base.transform.position = new Vector3(LIMITE_POSICAO_X_MINIMO, base.transform.position.y, base.transform.position.z);
			}
			if (base.transform.position.x >= LIMITE_POSICAO_X_MAXIMO)
			{
				base.transform.position = new Vector3(LIMITE_POSICAO_X_MAXIMO, base.transform.position.y, base.transform.position.z);
			}
			if (base.transform.position.y <= LIMITE_POSICAO_Y_MINIMO)
			{
				base.transform.position = new Vector3(base.transform.position.x, LIMITE_POSICAO_Y_MINIMO, base.transform.position.z);
			}
			if (base.transform.position.y >= LIMITE_POSICAO_Y_MAXIMO)
			{
				base.transform.position = new Vector3(base.transform.position.x, LIMITE_POSICAO_Y_MAXIMO, base.transform.position.z);
			}
			if (base.transform.position.z <= LIMITE_POSICAO_Z_MINIMO)
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, LIMITE_POSICAO_Z_MINIMO);
			}
			if (base.transform.position.z >= LIMITE_POSICAO_Z_MAXIMO)
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, LIMITE_POSICAO_Z_MAXIMO);
			}
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			ZOOM_DESEJADO -= ZOOM_VELOCIDADE;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			ZOOM_DESEJADO += ZOOM_VELOCIDADE;
		}
		if (ZOOM_DESEJADO >= 75f)
		{
			ZOOM_DESEJADO = 75f;
		}
		if (ZOOM_DESEJADO <= 20f)
		{
			ZOOM_DESEJADO = 20f;
		}
		myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, ZOOM_DESEJADO, t);
		if (!_active)
		{
			return;
		}
		SetCursorState();
		if (Cursor.visible && _enableTranslation)
		{
			base.transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * Time.deltaTime * _translationSpeed);
		}
		if (_enableMovement)
		{
			Vector3 zero = Vector3.zero;
			float movementSpeed = _movementSpeed;
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
			{
				zero += base.transform.forward;
			}
			if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
			{
				zero -= base.transform.forward;
			}
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			{
				zero -= base.transform.right;
			}
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			{
				zero += base.transform.right;
			}
			if (Input.GetMouseButton(1) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
			{
				Object[] array = Object.FindObjectsOfType(typeof(GameObject));
				for (int i = 0; i < array.Length; i++)
				{
					((GameObject)array[i]).SendMessage("MENSAGEM_CAMERA_LIVRE", SendMessageOptions.DontRequireReceiver);
				}
			}
			CalculateCurrentIncrease(zero != Vector3.zero);
			base.transform.position += zero * movementSpeed * _currentIncrease;
		}
		if (_enableRotation && Input.GetMouseButton(1))
		{
			base.transform.rotation *= Quaternion.AngleAxis((0f - Input.GetAxis("Mouse Y")) * _mouseSense, Vector3.right);
			base.transform.rotation = Quaternion.Euler(base.transform.eulerAngles.x, base.transform.eulerAngles.y + Input.GetAxis("Mouse X") * _mouseSense, base.transform.eulerAngles.z);
		}
		if (Input.GetKeyDown(_initPositonButton))
		{
			base.transform.position = _initPosition;
			base.transform.eulerAngles = _initRotation;
		}
	}

	private void MENSAGEM_CAMERA_POS_BANCADA_1()
	{
		GetComponent<Animation>().CrossFade("CAMERA_POS_BANCADA_1", 0.1f);
		MonoBehaviour.print("CAMERA_POS_BANCADA_1");
	}

	private void MENSAGEM_CAMERA_POS_BANCADA_2()
	{
		GetComponent<Animation>().CrossFade("CAMERA_POS_BANCADA_2", 0.1f);
		MonoBehaviour.print("CAMERA_POS_BANCADA_2");
	}

	private void MENSAGEM_CAMERA_POS_BANCADA_3()
	{
		GetComponent<Animation>().CrossFade("CAMERA_POS_BANCADA_3", 0.1f);
		MonoBehaviour.print("CAMERA_POS_BANCADA_3");
	}

	private void MENSAGEM_CAMERA_POS_BANCADA_4()
	{
		GetComponent<Animation>().CrossFade("CAMERA_POS_BANCADA_4", 0.1f);
		MonoBehaviour.print("CAMERA_POS_BANCADA_4");
	}
}
