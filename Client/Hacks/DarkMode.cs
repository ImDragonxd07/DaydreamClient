using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Networking;
using UnityEngine.UI;
namespace Daydream.Client.Hacks
{
    internal class DarkMode
    {
		private static Cubemap c;
		private static string getfromdropbox(string dir)
        {
			return "https://dl.dropboxusercontent.com/s/"+dir+"?dl=1";
        }
		public static IEnumerator setskybox()
        {

			yield return new WaitUntil((Func<bool>)(() => Assets.Resources.loadedassets == true));
			GameObject SkyBox = null;
			GameObject SkyBox2 = null;
			try { SkyBox = GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music/SkyCube_Baked"); } catch { };
			try { SkyBox2 = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked"); } catch { };
			var mat = new Material(Shader.Find("Skybox/6 Sided"));
			//RenderSettings.skybox = Assets.Resources.skybox;
			mat.name = "Daydream Mat";

			mat.SetTexture("_FrontTex", Assets.Resources.front);
			mat.SetTexture("_BackTex", Assets.Resources.back);
			mat.SetTexture("_LeftTex", Assets.Resources.left);
			mat.SetTexture("_RightTex", Assets.Resources.right);
			mat.SetTexture("_UpTex", Assets.Resources.up);
			mat.SetTexture("_DownTex", Assets.Resources.down);
			RenderSettings.skybox = mat;
			
			yield break;

			//string drivedownload = "https://drive.google.com/uc?export=download&id="; 

			//Texture2D[] atlasTextures = new Texture2D[6];
			//atlasTextures[0] = Assets.Resources.back;
			//atlasTextures[1] = Assets.Resources.down;
			//atlasTextures[2] = Assets.Resources.front;
			//atlasTextures[3] = Assets.Resources.left ;
			//atlasTextures[4] = Assets.Resources.right;
			//atlasTextures[5] = Assets.Resources.up;



			//SkyBox.GetComponent<MeshRenderer>().material.SetTexture("_Tex", content); // cube
			//MelonCoroutines.Start(createCubemap(atlasTextures));

		}
		public static int LoadedAssets = 0; 
		private static Color[] imageColors;
		private static int size = 1024;
		private static IEnumerator createCubemap(Texture2D[] atlasTextures)
        {

			//c = new Cubemap(1024, TextureFormat.RGBA32, false);
			//Color[] CubeMapColors;

			//for (int i = 0; i < atlasTextures.Count(); i++)
			//{
			//	Utility.Logger.log("Setting pixels ["+i+"] " + c.isReadable);
			//	Utility.Logger.log("Setting source to " + atlasTextures[i].name);
			//	source = atlasTextures[i];
			//CubeMapColors = CreateCubemapTexture(1024, (CubemapFace)i);
			//	Utility.Logger.log("Set pixels");
			//	c.SetPixels(CubeMapColors, (CubemapFace)i);

			//}
			//// we set the cubemap from the texture pixel by pixel
			//c.Apply();

			//Utility.Logger.log("setting skybox");
			yield break;

		}
		private static AudioClip loadingsound = null;
		public static void playcustomloadingmusic()
        {
			if(loadingsound != null)
            {
				AudioSource sound = GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music/LoadingSound").GetComponent<AudioSource>();

			AudioSource sound2 = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/LoadingSound").GetComponent<AudioSource>();
			if (sound)
			{

				sound.clip = null;

				sound.clip = loadingsound;
				sound.Play();
			}
			if (sound2)
			{
				
				sound2.clip = null;

				sound2.clip = loadingsound;
				sound2.Play();
			}
            }
		}
		public static IEnumerator loadcustomaudio()
        {
            if (!loadingsound)
            {
				DirectoryInfo dir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"));
				FileInfo[] info = dir.GetFiles("*.*");
				foreach (FileInfo f in info)
				{
					if (f.Extension.ToLower() == ".mp3")
					{
						var filename = Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + ("/" + f.Name);
						if (!File.Exists(filename))
						{
							Utility.Logger.errorlog("Couldent get custom audio");
						}
						Utility.Logger.log("Found custom song " + filename);

						var www = UnityWebRequest.Get(filename);
						Utility.Logger.log("Downloading song from files ");

						yield return www.SendWebRequest();
						yield return new WaitUntil((Func<bool>)(() => www.isDone == true));

						if (www.isNetworkError || www.isHttpError)
						{
							Utility.Logger.errorlog("Download error" + www.error);


						}
						else
						{
							Utility.Logger.log("Load customs song");
							loadingsound = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, false, false, AudioType.UNKNOWN);
							playcustomloadingmusic();
						}

					}
				}
			}

		}
		private static Vector3[][] faces =
{
		new Vector3[] {
			new Vector3(1.0f, 1.0f, -1.0f),
			new Vector3(1.0f, 1.0f, 1.0f),
			new Vector3(1.0f, -1.0f, -1.0f),
			new Vector3(1.0f, -1.0f, 1.0f)
		},
		new Vector3[] {
			new Vector3(-1.0f, 1.0f, 1.0f),
			new Vector3(-1.0f, 1.0f, -1.0f),
			new Vector3(-1.0f, -1.0f, 1.0f),
			new Vector3(-1.0f, -1.0f, -1.0f)
		},
		new Vector3[] {
			new Vector3(-1.0f, 1.0f, 1.0f),
			new Vector3(1.0f, 1.0f, 1.0f),
			new Vector3(-1.0f, 1.0f, -1.0f),
			new Vector3(1.0f, 1.0f, -1.0f)
		},
		new Vector3[] {
			new Vector3(-1.0f, -1.0f, -1.0f),
			new Vector3(1.0f, -1.0f, -1.0f),
			new Vector3(-1.0f, -1.0f, 1.0f),
			new Vector3(1.0f, -1.0f, 1.0f)
		},
		new Vector3[] {
			new Vector3(-1.0f, 1.0f, -1.0f),
			new Vector3(1.0f, 1.0f, -1.0f),
			new Vector3(-1.0f, -1.0f, -1.0f),
			new Vector3(1.0f, -1.0f, -1.0f)
		},
		new Vector3[] {
			new Vector3(1.0f, 1.0f, 1.0f),
			new Vector3(-1.0f, 1.0f, 1.0f),
			new Vector3(1.0f, -1.0f, 1.0f),
			new Vector3(-1.0f, -1.0f, 1.0f)
		}
	};
		private static Color[] CreateCubemapTexture(int resolution, CubemapFace face)
		{
			Texture2D texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, false);

			Vector3 texelX_Step = (faces[(int)face][1] - faces[(int)face][0]) / resolution;
			Vector3 texelY_Step = (faces[(int)face][3] - faces[(int)face][2]) / resolution;

			float texelSize = 1.0f / resolution;
			float texelIndex = 0.0f;

			//Create textured face
			Color[] cols = new Color[resolution];
			for (int y = 0; y < resolution; y++)
			{
				Vector3 texelX = faces[(int)face][0];
				Vector3 texelY = faces[(int)face][2];
				for (int x = 0; x < resolution; x++)
				{
					cols[x] = Project(Vector3.Lerp(texelX, texelY, texelIndex).normalized);
					texelX += texelX_Step;
					texelY += texelY_Step;
				}
				texture.SetPixels(0, y, resolution, 1, cols);
				texelIndex += texelSize;
			}
			texture.wrapMode = TextureWrapMode.Clamp;
			texture.Apply();

			Color[] colors= texture.GetPixels();
			return colors;
		}
		private static Texture2D source;

		private static Color Project(Vector3 direction)
		{
			if(source == null)
            {
				Utility.Logger.errorlog("source is null");
            }
			float theta = (float)(Mathf.Atan2(direction.z, direction.x) + Math.PI / 180.0f);
			float phi = Mathf.Acos(direction.y);

			int texelX = (int)(((theta / Math.PI) * 0.5f + 0.5f) * source.width);
			if (texelX < 0) texelX = 0;
			if (texelX >= source.width) texelX = source.width - 1;
			int texelY = (int)((phi / Math.PI) * source.height);
			if (texelY < 0) texelY = 0;
			if (texelY >= source.height) texelY = source.height - 1;

			return source.GetPixel(texelX, source.height - texelY - 1);
		}
		public static void run()
        {
			Utility.Logger.log("Set skybox");
			//GameObject SkyBox = GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music/SkyCube_Baked");
			//SkyBox.GetComponent<MeshRenderer>().material = Assets.Resources.skybox;
			Utility.Logger.log("Changing Colors");
			GameObject.Find("/UserInterface/MenuContent/Screens/Authentication/LoginUserPass/ButtonAboutUs").SetActive(false);
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton").GetComponent<Image>().color = new Color(0f, 1f, 0f, 1f);
			GameObject.Find("/UserInterface/MenuContent/Backdrop/Backdrop/Background").SetActive(false);
			GameObject.Find("/UserInterface/MenuContent/Backdrop/Backdrop/Image").SetActive(false);
			GameObject.Find("/_Application/CursorManager/BlueFireballMouse").GetComponent<VRCSpaceUiCursor>().field_Public_Color_0 = new Color(0f, 1f, 0f, 0.1267f);
			GameObject.Find("/_Application/CursorManager/BlueFireballMouse").GetComponent<VRCSpaceUiCursor>().field_Public_Color_1 = new Color(0f, 0.4528f, 0.46f, 1f);
			GameObject.Find("/_Application/CursorManager/BlueFireballMouse").GetComponent<VRCSpaceUiCursor>().field_Public_Color_2 = new Color(0f, 0f, 0.3962f, 1f);
			GameObject.Find("/UserInterface/LoadingBackground_TealGradient_Music/SkyCube_Baked").SetActive(false);
			GameObject.Find("/_Application/CursorManager/BlueFireballMouse/Ball").GetComponent<ParticleSystem>().emissionRate = 25f;
			GameObject.Find("/_Application/CursorManager/BlueFireballMouse/Ball").GetComponent<ParticleSystem>().startColor = new Color(0f, 1f, 0f, 1f);
			GameObject.Find("/_Application/CursorManager/BlueFireballMouse/Trail").GetComponent<ParticleSystem>().startColor = new Color(0f, 1f, 0f, 1f);
			GameObject.Find("/_Application/CursorManager/BlueFireballMouse/Trail").GetComponent<ParticleSystem>().gravityModifier = 0.05f;
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/SCREEN/mainFrame").SetActive(false);
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").SetActive(false);
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").SetActive(false);
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_Lighting (1)/Reflection Probe").SetActive(false);
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").SetActive(false);
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").SetActive(false);
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR_BG").GetComponent<Image>().color = new Color(0f, 1f, 0.245f, 0.5f);
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR").GetComponent<Image>().color = new Color(0f, 1f, 0f, 1f);
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ButtonMiddle").GetComponent<Image>().color = new Color(1f, 0f, 0f, 0.18f);
			GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles/FX_snow").GetComponent<ParticleSystem>().startColor = new Color(0.1333f, 1f, 0.1098f, 0.1667f);
			GameObject.Find("/UserInterface/LoadingBackground_TealGradient_Music/_FX_ParticleBubbles/FX_snow").GetComponent<ParticleSystem>().startColor = new Color(0.1333f, 1f, 0.1098f, 0.1667f);
				
		}

		internal static IEnumerator Ini()
		{
			while (GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup") == null)
			{
				yield return new WaitForSeconds(1f);
			}
		}

		// Token: 0x040000C0 RID: 192
	}
}
