using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public Pauzer pz;
    public pauzerOption pzo;
    public GameObject pauseOptionPanel;

    public Slider backgroundMusicSlider;
    public Slider soundsEffectSlider;

    public GameManager gm;

    public void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetBackgroundMusicValu(gm.backgroundVolume);
        SetSoundsEffectSliderValue(gm.soundEfeectsVolume);
    }

    public void Update()
    {
        if (pz == null)
            pz = GameObject.Find("GameManager").GetComponent<Pauzer>();

        if (pzo == null)
            pzo = pauseOptionPanel.GetComponent<pauzerOption>();
    }

    public void SetBackgroundMusicValu(float Volume)
    {
        backgroundMusicSlider.value = Volume;
    }

    public void SetSoundsEffectSliderValue(float Volume)
    {
        soundsEffectSlider.value = Volume;
    }   

    public void Resum()
    {
        pz.Pause();
    }

    public void Option()
    {
        //TODO: load Option menu
        pauseOptionPanel.SetActive(true);
        pzo.pauseOptionPanelShow = true;
    }

    public void HideOption()
    {
        StartCoroutine(HidePauzerOptionPanel(1.2f));
        gm.SetSoundEffectsVolume(soundsEffectSlider.value);
        gm.SetBackGroundVolume(backgroundMusicSlider.value);
    }
    IEnumerator HidePauzerOptionPanel(float waitTime)
    {
        pzo.pauseOptionPanelShow = false;
        yield return new WaitForSeconds(waitTime);
        pauseOptionPanel.SetActive(false);
    }

    public void Quit()
    {
        //TODO: quit, back to menu
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
