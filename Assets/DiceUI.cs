using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceUI : MonoBehaviour {

    public Animator diceSpriteAnimator;

	// Use this for initialization
	void OnMouseDown () {
        StartCoroutine("ShowUI");
	}
	
	// Update is called once per frame
    public IEnumerator ShowUI () {
        yield return new WaitForSeconds(2.0f);
        diceSpriteAnimator.SetBool("UI", true);
		
	}
}
