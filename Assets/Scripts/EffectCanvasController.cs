using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCanvasController : MonoBehaviour {

    public GameObject LR;
    public GameObject GU;
    public GameObject GD;

    public GameObject puasePanel;
    bool isPausePanelShow;
    Animator ani;

	private TraitManager traitManager;

	// Use this for initialization
	void Start () {
        isPausePanelShow = false;
        ani = puasePanel.GetComponent<Animator>();
	}

    IEnumerator switchPauseState(bool showOrNot, float waitTime)
    {
        
        Time.timeScale = 1;
        isPausePanelShow = showOrNot;
        yield return new WaitForSeconds(waitTime);        
        if (showOrNot)
            Time.timeScale =  0;  
        
    }

	// Update is called once per frame
	void Update () {

        // trigger Pause panel
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPausePanelShow)
                StartCoroutine(switchPauseState(false, 0.36f));
            else
            {                
                StartCoroutine(switchPauseState(true, 0.36f));
            }
        }

        Debug.Log(isPausePanelShow);

		if (traitManager == null && GameObject.FindGameObjectWithTag ("Player")) {
			traitManager = GameObject.FindGameObjectWithTag ("Player").GetComponent<TraitManager> ();
		}
		if (traitManager != null) {
			LR.SetActive (traitManager.isInvert);
			GU.SetActive (traitManager.isHeavy);
			GD.SetActive (traitManager.isBouncy);
		}
	}

    private void FixedUpdate()
    {
        ani.SetBool("show", isPausePanelShow);   
    }
}
