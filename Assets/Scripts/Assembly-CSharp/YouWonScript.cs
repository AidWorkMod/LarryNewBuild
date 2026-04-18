using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class YouWonScript : MonoBehaviour
{
	private void Update()
	{
		this.delay -= Time.deltaTime;
		if (this.delay <= 0f)
		{
			StartCoroutine(ShowResults());
		}
		IEnumerator ShowResults()
		{
			float it = PlaytimeFloat;
			YouWonMessage.SetActive(true);
			yield return new WaitForSeconds(ResultsDelay);
			Mode.SetActive(true);
			Mode.GetComponent<TMP_Text>().text = "Mode: " + PlayerPrefs.GetString("CurrentMode");
            yield return new WaitForSeconds(ResultsDelay);
			PlayTime.SetActive(true);
			PlayTime.GetComponent<TMP_Text>().text = "Your playtime: PLACEHOLDER";
			yield return new WaitForSeconds(ResultsDelay);
			ObjectsCollected.SetActive(true);
			ObjectsCollected.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("TargetCollect");

        }
	}

    [SerializeField]
    float delay;

	[SerializeField]
	GameObject YouWonMessage;

	[SerializeField]
	GameObject Mode;

	[SerializeField]
	GameObject PlayTime;

	[SerializeField]
	GameObject ObjectsCollected;

	[SerializeField]
	float ResultsDelay;

	float PlaytimeFloat;
}
