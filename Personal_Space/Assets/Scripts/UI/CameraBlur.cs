using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Shader/Blur")]
public class CameraBlur : MonoBehaviour
{
	#region Variables
	public Shader SCShader;
	private float TimeX = 1.0f;
	private Vector4 ScreenResolution;
	private Material SCMaterial;
	[Range(0, 1000)]
	private float Radius = 0f;
	[Range(0, 1000)]
	private float Factor = 100.0f;
	[Range(0, 8)]
	private int FastFilter = 0;

	[Range(0, 10)]
	public int blurFactor = 5;

	private GameObject Player;
	private PlayerController Health;

	public static float ChangeRadius;
	public static float ChangeFactor;
	public static int ChangeFastFilter;
	private bool pauseBlur;

	#endregion

	#region Properties
	Material material
	{
		get
		{
			if (SCMaterial == null)
			{
				SCMaterial = new Material(SCShader);
				SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return SCMaterial;
		}
	}
	#endregion
	void Start()
	{
		ChangeRadius = Radius;
		ChangeFactor = Factor;
		ChangeFastFilter = FastFilter;
		SCShader = Shader.Find("Shader/Blur");
		Player = GameObject.Find("Player");
		Health = Player.GetComponent<PlayerController>();
		
	}

	void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{

		if (SCShader != null)
		{
			int DownScale = 1;
			TimeX += Time.deltaTime;
			if (TimeX > 100) TimeX = 0;
			material.SetFloat("_TimeX", TimeX);
			material.SetFloat("_Radius", Radius / DownScale);
			material.SetFloat("_Factor", Factor);
			material.SetVector("_ScreenResolution", new Vector2(Screen.width / DownScale, Screen.height / DownScale));
			int rtW = sourceTexture.width / DownScale;
			int rtH = sourceTexture.height / DownScale;

			if (FastFilter > 1)
			{
				RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);
				Graphics.Blit(sourceTexture, buffer, material);
				Graphics.Blit(buffer, destTexture);
				RenderTexture.ReleaseTemporary(buffer);
			}
			else
			{
				Graphics.Blit(sourceTexture, destTexture, material);
			}
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}


	}
	void OnValidate()
	{
		ChangeRadius = Radius;
		ChangeFactor = Factor;
		ChangeFastFilter = FastFilter;
	}
	// Update is called once per frame
	void Update()
	{
		if (pauseBlur)
		{
			Radius = 800 * blurFactor;
		}
		else
		{
			if (Health.health > 0)
			{
				Radius = (100 - Health.health) * blurFactor;
			}
			else
			{
				Radius = 0;
			}
		}
	}

	void OnDisable()
	{
		if (SCMaterial)
		{
			DestroyImmediate(SCMaterial);
		}

	}

	public void SetPause()
	{
		pauseBlur = !pauseBlur;
	}

}