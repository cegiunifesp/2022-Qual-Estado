using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
	[Serializable]
	public class Estado
	{
		public string NOME_ESTADO;

		public Button buttonComponent;

		public Sprite normalImage;

		public Sprite completeImage;
	}

	public GameManager gm;

	public AudioManager am;

	public Image confirmButton;

	public RawImage stateNameBackground;

	[Header("Botoes")]
	public Estado[] btns_Centro;

	public Estado[] btns_Nordeste;

	public Estado[] btns_Norte;

	public Estado[] btns_Sudeste;

	public Estado[] btns_Sul;

	private void restartArts(Estado[] estadosParaReiniciar)
	{
		foreach (Estado estado in estadosParaReiniciar)
		{
			estado.buttonComponent.enabled = true;
			estado.buttonComponent.interactable = true;
			estado.buttonComponent.image.sprite = estado.normalImage;
		}
	}

	public void restartButtons()
	{
		switch (gm.currentRegion)
		{
		case "Centro-Oeste":
			restartArts(btns_Centro);
			break;
		case "Nordeste":
			restartArts(btns_Nordeste);
			break;
		case "Norte":
			restartArts(btns_Norte);
			break;
		case "Sudeste":
			restartArts(btns_Sudeste);
			break;
		case "Sul":
			restartArts(btns_Sul);
			break;
		}
	}

	private IEnumerator reactivateButtons(Estado[] array)
	{
		yield return new WaitForSeconds(1f);
		foreach (Estado estado in array)
		{
			if (estado.buttonComponent.enabled)
			{
				estado.buttonComponent.interactable = true;
				estado.buttonComponent.image.color = gm.corNormal;
			}
		}
		confirmButton.color = gm.corNormal;
		stateNameBackground.color = gm.corNormal;
		gm.selectedState.text = "";
	}

	public void ClearSelection()
	{
		gm.selectedState.text = "";
	}

	private void checkAnswer(Estado sb, string stateName)
	{
		if (gm.selectedState.text == stateName)
		{
			if (gm.currentQuestion.state.ToUpper() == gm.selectedState.text)
			{
				sb.buttonComponent.image.sprite = sb.completeImage;
				sb.buttonComponent.enabled = false;
				am.Play("Estado_Certo");
				confirmButton.color = gm.corCerto;
				stateNameBackground.color = gm.corCerto;
				StartCoroutine(gm.TransitionToNextQuestion());
			}
			else
			{
				am.Play("Estado_Errado");
				sb.buttonComponent.image.color = gm.corErrado;
				confirmButton.color = gm.corErrado;
				stateNameBackground.color = gm.corErrado;
			}
		}
	}

	private void buttonActions(Estado[] array)
	{
		Estado[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].buttonComponent.interactable = false;
		}
		array2 = array;
		foreach (Estado estado in array2)
		{
			checkAnswer(estado, estado.NOME_ESTADO);
		}
		StartCoroutine(reactivateButtons(array));
	}

	public void definitiveConfirm()
	{
		if (gm.selectedState.text != "")
		{
			switch (gm.currentRegion)
			{
			case "Centro-Oeste":
				buttonActions(btns_Centro);
				break;
			case "Nordeste":
				buttonActions(btns_Nordeste);
				break;
			case "Norte":
				buttonActions(btns_Norte);
				break;
			case "Sudeste":
				buttonActions(btns_Sudeste);
				break;
			case "Sul":
				buttonActions(btns_Sul);
				break;
			}
		}
	}
}
