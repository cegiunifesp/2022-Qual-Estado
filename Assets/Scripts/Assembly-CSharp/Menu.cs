using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public AudioManager am;

	public Transform logo;

	public Transform placa;

	public GameObject menu;

	public GameObject plantaOverlay;

	public Transform plantas;

	public GameObject creditos;

	public CanvasGroup background;

	private Vector2 escalaInicialPlanta = new Vector2(3f, 3f);

	public Button menuSound;

	public Sprite somLigado;

	public Sprite somDesligado;

	private void desativarObjetos()
	{
		menu.SetActive(value: false);
		creditos.SetActive(value: true);
		plantaOverlay.SetActive(value: true);
	}

	private void ativarObjetos()
	{
		plantaOverlay.SetActive(value: false);
		creditos.SetActive(value: false);
		menu.SetActive(value: true);
	}

	public void ativaCreditos()
	{
		desativarObjetos();
		plantas.LeanScale(Vector2.one, 0.5f);
		background.LeanAlpha(1f, 0.5f);
	}

	public void desativaCreditos()
	{
		background.LeanAlpha(0f, 0.3f);
		plantas.LeanScale(escalaInicialPlanta, 0.3f).setOnComplete(ativarObjetos);
	}

	public void mudaCena(string local)
	{
		SceneManager.LoadScene("Scenes/" + local);
	}

	public void quit()
	{
		Debug.Log("QUIT");
		Application.Quit();
	}

	public void MenuSound()
	{
		if (PlayerPrefs.GetString("audio") == "on")
		{
			PlayerPrefs.SetString("audio", "off");
			menuSound.image.sprite = somDesligado;
		}
		else if (PlayerPrefs.GetString("audio") == "off")
		{
			PlayerPrefs.SetString("audio", "on");
			menuSound.image.sprite = somLigado;
		}
		am.UpdateVolume();
	}

	private void Start()
	{
		am.Play("MenuInicialIntro");
		am.Play("Ambiencia");
		if (!PlayerPrefs.HasKey("isFirstTime"))
		{
			PlayerPrefs.SetString("audio", "on");
			PlayerPrefs.SetString("isFirstTime", "false");
		}
		am.UpdateVolume();
		if (PlayerPrefs.GetString("audio") == "on")
		{
			menuSound.image.sprite = somLigado;
		}
		else if (PlayerPrefs.GetString("audio") == "off")
		{
			menuSound.image.sprite = somDesligado;
		}
		plantas.localScale = escalaInicialPlanta;
		background.alpha = 0f;
	}
}
