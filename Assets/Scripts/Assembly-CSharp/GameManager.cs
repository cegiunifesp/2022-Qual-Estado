using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
	public Question[] centroOeste;

	public Question[] nordeste;

	public Question[] norte;

	public Question[] sudeste;

	public Question[] sul;

	private static List<Question> unansweredQuestions;

	[HideInInspector]
	public Question currentQuestion;

	[HideInInspector]
	public string currentRegion;

	public Timer timer;

	[SerializeField]
	private GameObject Intro;

	[SerializeField]
	private CanvasGroup IntroCanvasGroup;

	[SerializeField]
	private CanvasGroup LayoutCanvasGroup;

	[SerializeField]
	private GameObject Layout;

	[SerializeField]
	private GameObject AlternativasCentro;

	[SerializeField]
	private GameObject AlternativasNordeste;

	[SerializeField]
	private GameObject AlternativasNorte;

	[SerializeField]
	private GameObject AlternativasSudeste;

	[SerializeField]
	private GameObject AlternativasSul;

	[SerializeField]
	private Text currentLevel;

	[SerializeField]
	private VideoPlayer vid;

	[SerializeField]
	private float timeBetweenQuestions = 2f;

	[SerializeField]
	public Text selectedState;

	[SerializeField]
	public Color corCerto;

	[SerializeField]
	public Color corErrado;

	[SerializeField]
	public Color corNormal;

	[SerializeField]
	private Sprite CentroCompleto;

	[SerializeField]
	private Sprite NordesteCompleto;

	[SerializeField]
	private Sprite NorteCompleto;

	[SerializeField]
	private Sprite SudesteCompleto;

	[SerializeField]
	private Sprite SulCompleto;

	[SerializeField]
	private GameObject RegiaoCentro;

	[SerializeField]
	private GameObject RegiaoNordeste;

	[SerializeField]
	private GameObject RegiaoNorte;

	[SerializeField]
	private GameObject RegiaoSudeste;

	[SerializeField]
	private GameObject RegiaoSul;

	[SerializeField]
	private GameObject mapaCompleto;

	[SerializeField]
	private CanvasGroup mapaCompletoCanvasGroup;

	public Animator Zooms;

	public AnimationManager animationManager;

	public AudioManager audioManager;

	[SerializeField]
	private Text nomeRegiao;

	[SerializeField]
	private Text tempoCorrido;

	private SpriteState ligado;

	private SpriteState desligado;

	[SerializeField]
	private Button soundButton;

	[SerializeField]
	private Sprite sp_somLigado;

	[SerializeField]
	private Sprite sp_somLigado_highlight;

	[SerializeField]
	private Sprite sp_somDesligado;

	[SerializeField]
	private Sprite sp_somDesligado_highlight;

	[SerializeField]
	private AudioMixer audioMixer;

	[SerializeField]
	private Text textoFinal;

	private int regioesCompletadas;

	[SerializeField]
	private GameObject firework;

	private int totalMinutes;

	private int totalSeconds;

	[SerializeField]
	private GameObject tempoFinal;

	public void playVideo()
	{
		vid.Play();
	}

	public void pauseVideo()
	{
		vid.Pause();
	}

	public void mudaCena(string local)
	{
		SceneManager.LoadScene("Scenes/" + local);
	}

	public void muteAndUnmute()
	{
		if (PlayerPrefs.GetString("audio") == "on")
		{
			Debug.Log("entrou aqui");
			PlayerPrefs.SetString("audio", "off");
			soundButton.spriteState = desligado;
			soundButton.image.sprite = sp_somDesligado;
		}
		else if (PlayerPrefs.GetString("audio") == "off")
		{
			PlayerPrefs.SetString("audio", "on");
			soundButton.spriteState = ligado;
			soundButton.image.sprite = sp_somLigado;
		}
		UpdateVolumeGM();
	}

	private IEnumerator waitToActivate(GameObject obj)
	{
		getCurrentQuestion();
		yield return new WaitForSeconds(1f);
		obj.SetActive(value: true);
		LayoutCanvasGroup.LeanAlpha(1f, 0.5f);
		LayoutCanvasGroup.blocksRaycasts = true;
		IntroCanvasGroup.blocksRaycasts = false;
		yield return new WaitForSeconds(0.5f);
		mapaCompleto.SetActive(value: false);
		StartVideo();
		timer.startTimer();
	}

	private IEnumerator waitToDeactivate(GameObject obj)
	{
		LayoutCanvasGroup.LeanAlpha(0f, 0.5f);
		LayoutCanvasGroup.blocksRaycasts = false;
		IntroCanvasGroup.blocksRaycasts = true;
		yield return new WaitForSeconds(0.5f);
		obj.SetActive(value: false);
	}

	private void StartVideo()
	{
		vid.url = Path.Combine(Application.streamingAssetsPath, currentQuestion.videoName + ".mp4");
		playVideo();
	}

	private void getCurrentQuestion()
	{
		int index = Random.Range(0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions[index];
		currentLevel.text = currentRegion.ToUpper();
	}

	private void validateQuestions()
	{
		if (currentRegion == "Norte")
		{
			unansweredQuestions = norte.ToList();
		}
		else if (currentRegion == "Nordeste")
		{
			unansweredQuestions = nordeste.ToList();
		}
		else if (currentRegion == "Centro-Oeste")
		{
			unansweredQuestions = centroOeste.ToList();
		}
		else if (currentRegion == "Sudeste")
		{
			unansweredQuestions = sudeste.ToList();
		}
		else if (currentRegion == "Sul")
		{
			unansweredQuestions = sul.ToList();
		}
	}

	private void activateGameObjectStart()
	{
		if (currentRegion == "Norte")
		{
			audioManager.PlaySubTrack(currentRegion);
			Zooms.Play("ZoomInNorte");
			StartCoroutine(waitToActivate(AlternativasNorte));
		}
		else if (currentRegion == "Nordeste")
		{
			audioManager.PlaySubTrack(currentRegion);
			Zooms.Play("ZoomInNordeste");
			StartCoroutine(waitToActivate(AlternativasNordeste));
		}
		else if (currentRegion == "Centro-Oeste")
		{
			audioManager.PlaySubTrack("Centro");
			Zooms.Play("ZoomInCentro");
			StartCoroutine(waitToActivate(AlternativasCentro));
		}
		else if (currentRegion == "Sudeste")
		{
			audioManager.PlaySubTrack(currentRegion);
			Zooms.Play("ZoomInSudeste");
			StartCoroutine(waitToActivate(AlternativasSudeste));
		}
		else if (currentRegion == "Sul")
		{
			audioManager.PlaySubTrack(currentRegion);
			Zooms.Play("ZoomInSul");
			StartCoroutine(waitToActivate(AlternativasSul));
		}
	}

	private void changeSpriteRegion()
	{
		if (currentRegion == "Norte")
		{
			RegiaoNorte.GetComponent<Image>().sprite = NorteCompleto;
			RegiaoNorte.GetComponent<Button>().enabled = false;
		}
		if (currentRegion == "Nordeste")
		{
			RegiaoNordeste.GetComponent<Image>().sprite = NordesteCompleto;
			RegiaoNordeste.GetComponent<Button>().enabled = false;
		}
		if (currentRegion == "Centro-Oeste")
		{
			RegiaoCentro.GetComponent<Image>().sprite = CentroCompleto;
			RegiaoCentro.GetComponent<Button>().enabled = false;
		}
		if (currentRegion == "Sudeste")
		{
			RegiaoSudeste.GetComponent<Image>().sprite = SudesteCompleto;
			RegiaoSudeste.GetComponent<Button>().enabled = false;
		}
		if (currentRegion == "Sul")
		{
			RegiaoSul.GetComponent<Image>().sprite = SulCompleto;
			RegiaoSul.GetComponent<Button>().enabled = false;
		}
	}

	public void getRegiao(string regiaoSelecionada)
	{
		currentRegion = regiaoSelecionada;
		mapaCompletoCanvasGroup.blocksRaycasts = false;
		validateQuestions();
		activateGameObjectStart();
	}

	public void changeSelectedState(string selectedStateName)
	{
		selectedState.text = selectedStateName.ToUpper();
	}

	public void EndGame()
	{
		changeSpriteRegion();
		mapaCompleto.SetActive(value: true);
		timer.stopTimer();
		regioesCompletadas++;
		audioManager.Play("Map_Animation");
		if (currentRegion == "Norte")
		{
			audioManager.StopSubTrack(currentRegion);
			Zooms.Play("ZoomOutNorte");
			StartCoroutine(waitToDeactivate(AlternativasNorte));
		}
		if (currentRegion == "Nordeste")
		{
			audioManager.StopSubTrack(currentRegion);
			Zooms.Play("ZoomOutNordeste");
			StartCoroutine(waitToDeactivate(AlternativasNordeste));
		}
		if (currentRegion == "Centro-Oeste")
		{
			audioManager.StopSubTrack("Centro");
			Zooms.Play("ZoomOutCentro");
			StartCoroutine(waitToDeactivate(AlternativasCentro));
		}
		if (currentRegion == "Sudeste")
		{
			audioManager.StopSubTrack(currentRegion);
			Zooms.Play("ZoomOutSudeste");
			StartCoroutine(waitToDeactivate(AlternativasSudeste));
		}
		if (currentRegion == "Sul")
		{
			audioManager.StopSubTrack(currentRegion);
			Zooms.Play("ZoomOutSul");
			StartCoroutine(waitToDeactivate(AlternativasSul));
		}
		mapaCompletoCanvasGroup.blocksRaycasts = true;
		if (regioesCompletadas == 5)
		{
			totalMinutes += totalSeconds / 60;
			totalSeconds %= 60;
			tempoFinal.GetComponent<Text>().text = "TEMPO TOTAL: " + totalMinutes + ":" + $"{totalSeconds:00}";
			textoFinal.text = "PARABÉNS!MAPA COMPLETO";
			firework.SetActive(value: true);
			tempoFinal.SetActive(value: true);
		}
		currentRegion = "";
	}

	public IEnumerator TransitionToNextQuestion()
	{
		unansweredQuestions.Remove(currentQuestion);
		yield return new WaitForSeconds(timeBetweenQuestions);
		if (unansweredQuestions.Count == 0)
		{
			pauseVideo();
			timer.pauseUnpauseTimer();
			audioManager.Play("Foliage");
			audioManager.Play("Victory");
			nomeRegiao.text = currentRegion.ToUpper();
			if (timer.minutes == 1)
			{
				tempoCorrido.text = "1 MINUTO E " + $"{timer.seconds:00}" + " SEGUNDOS";
			}
			else if (timer.minutes > 1)
			{
				tempoCorrido.text = timer.minutes + "MINUTOS E " + $"{timer.seconds:00}" + " SEGUNDOS";
			}
			else
			{
				tempoCorrido.text = timer.seconds + " SEGUNDOS";
			}
			totalMinutes += timer.minutes;
			totalSeconds += timer.seconds;
			animationManager.AnimateEnd();
		}
		else
		{
			getCurrentQuestion();
			StartVideo();
		}
	}

	public void restartLevel()
	{
		validateQuestions();
		getCurrentQuestion();
		StartVideo();
		timer.currentTime = 0f;
		timer.startTimer();
	}

	private void Start()
	{
		regioesCompletadas = 0;
		totalMinutes = 0;
		totalSeconds = 0;
		audioMixer.SetFloat("volumePrincipal", 0f);
		audioManager.PlayMainTrack();
		audioManager.Play("Ambiencia");
		ligado.highlightedSprite = sp_somLigado_highlight;
		ligado.pressedSprite = sp_somLigado_highlight;
		desligado.highlightedSprite = sp_somDesligado_highlight;
		desligado.pressedSprite = sp_somDesligado_highlight;
		if (PlayerPrefs.GetString("audio") == "on")
		{
			soundButton.image.sprite = sp_somLigado;
			soundButton.spriteState = ligado;
		}
		else if (PlayerPrefs.GetString("audio") == "off")
		{
			soundButton.image.sprite = sp_somDesligado;
			soundButton.spriteState = desligado;
		}
	}

	private void UpdateVolumeGM()
	{
		Sound[] mainTrack = audioManager.mainTrack;
		for (int i = 0; i < mainTrack.Length; i++)
		{
			_ = mainTrack[i];
			if (PlayerPrefs.GetString("audio") == "on")
			{
				if (currentRegion == "Centro-Oeste")
				{
					audioManager.PlaySubTrack("Centro");
				}
				else
				{
					audioManager.PlaySubTrack(currentRegion);
				}
			}
			else if (currentRegion == "Centro-Oeste")
			{
				audioManager.StopSubTrack("Centro");
			}
			else
			{
				audioManager.StopSubTrack(currentRegion);
			}
		}
		audioManager.UpdateVolume();
		if (PlayerPrefs.GetString("audio") == "on")
		{
			audioManager.ambienceSoundRef.source.volume = audioManager.ambienciaVolume;
		}
	}
}
