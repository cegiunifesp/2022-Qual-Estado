using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	public CanvasGroup background;

	public Transform plantasCamada1;

	public Transform plantasCamada2;

	public Transform plantasCamada3;

	public Transform btnMenu;

	public Transform btnReiniciar;

	public Transform btnSom;

	public Transform btnFechar;

	public Transform poste;

	public Transform placaFinal;

	public Transform botaoContinuar;

	public CanvasGroup textFinal;

	public GameObject pauseMenu;

	public GameObject plantsBackground;

	public GameObject finalHolder;

	public Animator relogioAnimator;

	private Vector2 escalaInicialPlanta = new Vector2(3f, 3f);

	private void onEnablePlantsBackground()
	{
		background.alpha = 0f;
		plantasCamada1.localScale = escalaInicialPlanta;
		plantasCamada2.localScale = escalaInicialPlanta;
		plantasCamada3.localScale = escalaInicialPlanta;
	}

	private void onEnableBoard()
	{
		poste.localPosition = new Vector2(11f, -Screen.height);
		btnMenu.localScale = Vector2.zero;
		btnReiniciar.localScale = Vector2.zero;
		btnSom.localScale = Vector2.zero;
		btnFechar.localScale = Vector2.zero;
	}

	private void onEnableFinal()
	{
		placaFinal.localScale = Vector2.zero;
		botaoContinuar.localScale = Vector2.zero;
		textFinal.alpha = 0f;
	}

	public void playAnimacaoRelogio()
	{
		relogioAnimator.Play("relogio");
	}

	public void AnimateEnd()
	{
		onEnableFinal();
		onEnablePlantsBackground();
		finalHolder.SetActive(value: true);
		plantsBackground.SetActive(value: true);
		background.LeanAlpha(1f, 1f);
		plantasCamada3.LeanScale(Vector2.one, 0.3f);
		plantasCamada2.LeanScale(Vector2.one, 0.3f).delay = 0.3f;
		plantasCamada1.LeanScale(Vector2.one, 0.3f).delay = 0.6f;
		placaFinal.LeanScale(Vector2.one, 0.2f).setEaseOutBack().delay = 0.15f;
		botaoContinuar.LeanScale(Vector2.one, 0.1f).setEaseOutBack().delay = 0.35f;
		textFinal.LeanAlpha(1f, 0.5f);
	}

	public void AnimateContinueEnd()
	{
	}

	public void AnimatePause()
	{
		onEnablePlantsBackground();
		onEnableBoard();
		background.LeanAlpha(1f, 1f);
		plantasCamada3.LeanScale(Vector2.one, 0.3f);
		plantasCamada2.LeanScale(Vector2.one, 0.3f).delay = 0.3f;
		plantasCamada1.LeanScale(Vector2.one, 0.3f).delay = 0.6f;
		poste.LeanMoveLocalY(-60f, 0.3f).setEaseOutExpo();
		btnMenu.LeanScale(Vector2.one, 0.2f).setEaseOutBack().delay = 0.15f;
		btnReiniciar.LeanScale(Vector2.one, 0.2f).setEaseOutBack().delay = 0.3f;
		btnSom.LeanScale(Vector2.one, 0.2f).setEaseOutBack().delay = 0.45f;
		btnFechar.LeanScale(Vector2.one, 0.2f).setEaseOutBack().delay = 0.6f;
	}

	public void closeMenu()
	{
		background.LeanAlpha(0f, 1f);
		btnMenu.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
		btnReiniciar.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
		btnSom.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
		btnFechar.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
		plantasCamada3.LeanScale(escalaInicialPlanta, 0.3f).delay = 0.3f;
		plantasCamada2.LeanScale(escalaInicialPlanta, 0.3f).delay = 0.3f;
		plantasCamada1.LeanScale(escalaInicialPlanta, 0.3f).delay = 0.3f;
		poste.LeanMoveLocalY(-Screen.height, 0.3f).setEaseInExpo().setOnComplete(onComplete);
	}

	private void onComplete()
	{
		pauseMenu.SetActive(value: false);
		plantsBackground.SetActive(value: false);
	}
}
