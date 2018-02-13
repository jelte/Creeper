using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnyKeyStart : MonoBehaviour {
    public bool anyKeyTrigger;

    //for option panel
    public GameObject OptionPanel;
    public Slider backgroundMusicSlider;
    public Slider soundsEffectSlider;
    public GameManager gm;              //need a GameManager

    public void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetBackgroundMusicValu(gm.backgroundVolume);
        SetSoundsEffectSliderValue(gm.soundEfeectsVolume);
    }
    public void SetBackgroundMusicValu(float Volume)
    {
        backgroundMusicSlider.value = Volume;
    }

    public void SetSoundsEffectSliderValue(float Volume)
    {
        soundsEffectSlider.value = Volume;
    }

    public void StoryMode()
    {
        SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath("Scenes/Level1Scene"));
    }

    public void EndlessyMode()
    {
        //TODO: change name
        //SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath("Scenes/EndlesScene"));
    }

    public void OptionButtonShow()
    {
        OptionPanel.SetActive(true);
        OptionPanel.GetComponent<pauzerOption>().pauseOptionPanelShow = true;
    }

    public void OptionButtonHide()
    {
        OptionPanel.GetComponent<pauzerOption>().pauseOptionPanelShow = false;
        gm.SetSoundEffectsVolume(soundsEffectSlider.value);
        gm.SetBackGroundVolume(backgroundMusicSlider.value);
    }


    public void Quit()
    {
        Application.Quit();
    }


    // Update is called once per frame
    void Update () {
		if(Input.anyKey &&anyKeyTrigger)     
			SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath("Scenes/Level1Scene"));
	}
}
