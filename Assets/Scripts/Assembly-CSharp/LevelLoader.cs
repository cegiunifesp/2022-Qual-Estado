using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
	[SerializeField]
	private GameObject telaLoading;

	[SerializeField]
	private Image barraLoading;

	private float target;

	public void LoadScene(string name)
	{
		StartCoroutine(LoadAsynchronously(name));
	}

	private IEnumerator LoadAsynchronously(string name)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(name);
		telaLoading.SetActive(value: true);
		while (!operation.isDone)
		{
			float num = Mathf.Clamp01(operation.progress / 0.9f);
			target = num;
			yield return null;
		}
	}

	private void Update()
	{
		barraLoading.fillAmount = Mathf.MoveTowards(barraLoading.fillAmount, target, 3f * Time.deltaTime);
	}
}
