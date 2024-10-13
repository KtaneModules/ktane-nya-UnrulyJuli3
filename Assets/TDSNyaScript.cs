using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDSNyaScript : MonoBehaviour
{
	public KMBombModule Module;
	public KMSelectable YesButton;
	public KMSelectable NoButton;
	public KMAudio Audio;
	public List<Texture2D> CatPictures;
	public List<Texture2D> BobmPictures;
	public GameObject CatPictureObject;
	public GameObject BobmPictureObject;

	private static int midcount;
	private int mid;

	private bool isCat;

	void Start()
	{
		mid = ++midcount;
		isCat = UnityEngine.Random.value > 0.5f;
		Debug.LogFormat("[nya~ #{0}] the shown image {1} a cat~ ;3", mid, isCat ? "IS" : "is NOT");
		CatPictureObject.SetActive(isCat);
		BobmPictureObject.SetActive(!isCat);
		if (isCat) CatPictureObject.GetComponent<Renderer>().material.mainTexture = CatPictures.PickRandom();
		else BobmPictureObject.GetComponent<Renderer>().material.mainTexture = BobmPictures.PickRandom();
		YesButton.OnInteract += YesPressed;
		NoButton.OnInteract += NoPressed;
	}

	bool YesPressed()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, YesButton.transform);
		YesButton.AddInteractionPunch(1f);
		HandleAnswer(isCat);
		return false;
	}

	bool NoPressed()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, NoButton.transform);
		NoButton.AddInteractionPunch(1f);
		HandleAnswer(!isCat);
		return false;
	}

	private bool solved;

	void HandleAnswer(bool correct)
	{
		if (!solved)
		{
			if (correct)
			{
				solved = true;
				Debug.LogFormat("[nya~ #{0}] module passed~ uwu", mid);
				Module.HandlePass();
			}
			else
			{
				Debug.LogFormat("[nya~ #{0}] module striked~!", mid);
				Module.HandleStrike();
			}
		}
	}
}
