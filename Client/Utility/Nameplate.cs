using TMPro;
using UnityEngine;
using VRC;
using Object = UnityEngine.Object;


namespace Daydream.Client.Utility
{
	public class Nameplate
    {
		private Player _player;
		private string _text;
		private Color _color;
		private Vector3 _position;

		/// <summary>
		/// Plate constructor.
		/// https://github.com/tetra-fox/VRCMods/blob/master/ProPlates/PlateAPI.cs
		/// </summary>
		/// <param name="player">The player to create a Plate for</param>
		/// <param name="text">Text to display</param>
		/// <param name="color">Text color</param>
		/// <param name="position">Local position</param>
		/// <param name="containerPrefix">GameObject name prefix. Formatted as "{containerName} Container". Default value is PlateAPI.</param>

		/// <summary>
		/// <summary>
		/// GameObject representing the Plate.
		/// </summary>
		public GameObject GameObject { get; }
		public Nameplate(Player player, string text, Color color, Vector3? position = null, string containerPrefix = "PlateAPI")
		{
			this.Player = player;
			this.Text = text;
			this.Color = color;
			if (position == null) this.Position = new Vector3(0f, -60f, 0f); // default pos, below nameplate

			PlayerNameplate nameplate;
			try
			{
				nameplate = this.Player._vrcplayer.field_Public_PlayerNameplate_0;
			}
			catch
			{
				nameplate = this.Player._vrcplayer.gameObject.transform.Find("Player Nameplate/Canvas/Nameplate").GetComponent<PlayerNameplate>();
			}
			if (nameplate.transform.Find($"Contents/{containerPrefix} Container")) return; // prevent duplicates

			Transform newPlate = Object.Instantiate(nameplate.transform.Find("Contents/Quick Stats"),
				nameplate.transform.Find("Contents"), false);

			newPlate.name = $"{containerPrefix} Container";
			newPlate.localPosition = this.Position;
			newPlate.gameObject.active = true;

			// this component matches the plate opacity to the 
			//OpacityListener opacityListener = newPlate.gameObject.AddComponent<OpacityListener>();
			//opacityListener.reference = nameplate.transform.Find("Contents/Main/Background").GetComponent<ImageThreeSlice>();
			//opacityListener.target = newPlate.gameObject.GetComponent<ImageThreeSlice>();

			// remove unnecessary gameobjects and set pronoun text
			for (int i = newPlate.childCount; i > 0; i--)
			{
				Transform c = newPlate.GetChild(i - 1);
				if (c.name == "Trust Text")
				{
					c.name = "Text";
					c.GetComponent<TextMeshProUGUI>().text = this.Text;
					c.GetComponent<TextMeshProUGUI>().color = this.Color;
					continue;
				}

				Object.DestroyImmediate(c.gameObject);
			}

			this.GameObject = newPlate.gameObject;
		}
		public Player Player
		{
			get => this._player;
			set
			{
				this._player = value;
				if (this.GameObject == null) return;
				PlayerNameplate newPlayerNameplate;
                try
                {
					newPlayerNameplate = value._vrcplayer.field_Public_PlayerNameplate_0;
				}
				catch
                {
					newPlayerNameplate = value._vrcplayer.gameObject.transform.Find("Player Nameplate/Canvas/Nameplate").GetComponent<PlayerNameplate>();
				}
				this.GameObject.transform.SetParent(newPlayerNameplate.transform.Find("Contents"), false);
			}
		}

		/// <summary>
		/// Displayed text.
		/// </summary>
		public string Text
		{
			get => this._text;
			set
			{
				this._text = value;
				if (this.GameObject == null) return;
				this.GameObject.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = value;
			}
		}

		/// <summary>
		/// Text color.
		/// </summary>
		public Color Color
		{
			get => this._color;
			set
			{
				this._color = value;
				if (this.GameObject == null) return;
				this.GameObject.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().color = value;
			}
		}

		/// <summary>
		/// Plate position.
		/// </summary>
		public Vector3 Position
		{
			get => this._position;
			set
			{
				this._position = value;
				if (this.GameObject == null) return;
				this.GameObject.transform.localPosition = value;
			}
		}
	}
}
