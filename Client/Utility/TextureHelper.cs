using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Daydream.Client.Utility
{
	public abstract class TextureHelper
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000108B4 File Offset: 0x0000EAB4
		// (set) Token: 0x06000270 RID: 624 RVA: 0x000108BB File Offset: 0x0000EABB

		// Token: 0x06000273 RID: 627
		protected internal abstract byte[] Internal_EncodeToPNG(Texture2D tex);

		// Token: 0x06000274 RID: 628 RVA: 0x000108E1 File Offset: 0x0000EAE1

		// Token: 0x06000275 RID: 629
		protected internal abstract Texture2D Internal_NewTexture2D(int width, int height);

		// Token: 0x06000276 RID: 630 RVA: 0x000108EF File Offset: 0x0000EAEF


		// Token: 0x06000277 RID: 631
		protected internal abstract void Internal_Blit(Texture2D tex, RenderTexture rt);

		// Token: 0x06000278 RID: 632 RVA: 0x000108FE File Offset: 0x0000EAFE


		// Token: 0x06000279 RID: 633
		protected internal abstract Sprite Internal_CreateSprite(Texture2D texture);

		// Token: 0x0600027A RID: 634 RVA: 0x0001090B File Offset: 0x0000EB0B

		// Token: 0x0600027B RID: 635
		protected internal abstract Sprite Internal_CreateSprite(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, Vector4 border);

		// Token: 0x0600027C RID: 636 RVA: 0x00010920 File Offset: 0x0000EB20
		public static bool IsReadable(Texture2D tex)
		{
			bool result;
			try
			{
				tex.GetPixel(0, 0);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00010954 File Offset: 0x0000EB54
		public static Texture2D Copy(Texture2D orig)
		{
			return TextureHelper.Copy(orig, new Rect(0f, 0f, (float)orig.width, (float)orig.height));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0001097C File Offset: 0x0000EB7C
		public static Texture2D Copy(Texture2D orig, Rect rect)
		{
			bool flag = !TextureHelper.IsReadable(orig);
			Texture2D result;
			if (flag)
			{
				result = TextureHelper.ForceReadTexture(orig, rect);
			}
			else
			{
				Texture2D texture2D = new Texture2D((int)rect.width, (int)rect.height);
				texture2D.SetPixels(orig.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
				result = texture2D;
			}
			return result;
		}
		// Token: 0x0600027F RID: 639 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public static Texture2D ForceReadTexture(Texture2D origTex, Rect dimensions = default(Rect))
		{
			Texture2D result;
			try
			{
				bool flag = dimensions == default(Rect);
				if (flag)
				{
					dimensions = new Rect(0f, 0f, (float)origTex.width, (float)origTex.height);
				}
				FilterMode filterMode = origTex.filterMode;
				RenderTexture active = RenderTexture.active;
				origTex.filterMode = 0;
				RenderTexture temporary = RenderTexture.GetTemporary(origTex.width, origTex.height, 0, RenderTextureFormat.Default);
				temporary.filterMode = 0;
				RenderTexture.active = temporary;
				Graphics.Blit(origTex, temporary);
				Texture2D texture2D = new Texture2D((int)dimensions.width, (int)dimensions.height);
				texture2D.ReadPixels(dimensions, 0, 0);
				texture2D.Apply(false, false);
				RenderTexture.active = active;
				origTex.filterMode = filterMode;
				result = texture2D;
			}
			catch (Exception ex)
			{
				Utility.Logger.errorlog("Exception on ForceReadTexture: " + ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00010AEC File Offset: 0x0000ECEC
	}
}

