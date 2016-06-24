using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("AnimationTimer");
	}
    IEnumerator AnimationTimer()
    {
        yield return new WaitForSeconds(.50F);
        GetComponent<Text>().CrossFadeAlpha(0, 1, true);
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
        this.enabled = false;
    }
}
