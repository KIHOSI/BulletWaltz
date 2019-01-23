using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class HUE_Rotate : MonoBehaviour {
#region Variables
public Shader SCShader;
private Vector4 ScreenResolution;
private Material SCMaterial;
[Range(0, 6.3f)]
public float RotateValue = 0;
#endregion
#region Properties
Material material
{
get
{
if(SCMaterial == null)
{
SCMaterial = new Material(SCShader);
SCMaterial.hideFlags = HideFlags.HideAndDontSave;	
}
return SCMaterial;
}
}
#endregion
void Start () 
{
		SCShader = Shader.Find("PostEffect/Colors_HUE_Rotate");
if(!SystemInfo.supportsImageEffects)
{
enabled = false;
return;
}
}
void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
{
if(SCShader != null)
{
material.SetFloat("_Rotation", RotateValue);
material.SetVector("_ScreenResolution",new Vector2(Screen.width,Screen.height));
Graphics.Blit(sourceTexture, destTexture, material);
}
else
{
Graphics.Blit(sourceTexture, destTexture);	
}
}
void Update () 
{
#if UNITY_EDITOR
if (Application.isPlaying!=true)
{
	SCShader = Shader.Find("PostEffect/Colors_HUE_Rotate");
}
#endif
}
void OnDisable ()
{
if(SCMaterial)
{
DestroyImmediate(SCMaterial);	
}
}
}