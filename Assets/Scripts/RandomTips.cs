using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomTips : MonoBehaviour {
    public string[] messages = {
                //hurt messages
                "Don't worry, be happy :) ",
                "Please keep your device safe please :p",
                "If you can overcome this issue, then you can start to overcome the next issue :)",
                //Motivate sentence
                "“The Pessimist Sees Difficulty In Every Opportunity. The Optimist Sees Opportunity In Every Difficulty.” -Winston Churchill",
                "“Don’t Let Yesterday Take Up Too Much Of Today.” -Will Rogers",
                "“You Learn More From Failure Than From Success. Don’t Let It Stop You. Failure Builds Character.”- Unknown",
                "“It’s Not Whether You Get Knocked Down, It’s Whether You Get Up.” – Inspirational Quote By Vince Lombardi",
                "“If You Are Working On Something That You Really Care About, You Don’t Have To Be Pushed. The Vision Pulls You.”- Steve Jobs",
                "“Failure Will Never Overtake Me If My Determination To Succeed Is Strong Enough.”- Og Mandino",
                "“We May Encounter Many Defeats But We Must Not Be Defeated.”- Maya Angelou",
                "“Imagine Your Life Is Perfect In Every Respect; What Would It Look Like?”- Brian Tracy",
                "“Security Is Mostly A Superstition. Life Is Either A Daring Adventure Or Nothing.”- Helen Keller",
                "“You Are Never Too Old To Set Another Goal Or To Dream A New Dream.”- C.S. Lewis",
                //Reference https://www.briantracy.com/blog/personal-success/26-motivational-quotes-for-success/
                };

    int size;
    public Text showingText;



    // Use this for initialization
    void Start () {
		size = messages.Length;
        int r = Random.Range(0, size - 1);
        showingText.text = "Tip of the day:" +messages[r];
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
