using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBlink : MonoBehaviour {
    public Light blink;
    public float maxIntensity = 30f;
	// Use this for initialization
	void Start () {
        StartCoroutine(Blink());
	}

    private IEnumerator Blink()
    {
        while (true)
        {
            blink.intensity = maxIntensity;
            float blinkDuration = 0f;
            while (blink.intensity > 0)
            {
                blink.intensity = Mathf.Lerp(maxIntensity, 0f, blinkDuration*5f);
                blinkDuration += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
