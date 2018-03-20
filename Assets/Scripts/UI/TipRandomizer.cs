using UnityEngine;
using UnityEngine.UI;

namespace ProjectFTP.UI
{
    public class TipRandomizer : MonoBehaviour
    {
        public string[] messages = {
            //hurt messages
            "Don't worry, be happy :) ",
            "Please keep your device safe :p",
            "If you can overcome this issue, then you can start to overcome the next issue :)",
            "No mather how frustrated, don't throw your controller at the screen.",
            //Motivate sentence
            "“The Pessimist Sees Difficulty In Every Opportunity.\nThe Optimist Sees Opportunity In Every Difficulty.”\nWinston Churchill",
            "“Don’t Let Yesterday Take Up Too Much Of Today.”\n-Will Rogers",
            "“You Learn More From Failure Than From Success.\nDon’t Let It Stop You. Failure Builds Character.”",
            "“It’s Not Whether You Get Knocked Down,\nIt’s Whether You Get Up.”\nVince Lombardi",
            "“If You Are Working On Something That You Really Care About,\nYou Don’t Have To Be Pushed. The Vision Pulls You.”\nSteve Jobs",
            "“Failure Will Never Overtake Me If My Determination To Succeed Is Strong Enough.”\n-Og Mandino",
            "“We May Encounter Many Defeats But We Must Not Be Defeated.”\n-Maya Angelou",
            "“Imagine Your Life Is Perfect In Every Respect;\nWhat Would It Look Like?”\n-Brian Tracy",
            "“Security Is Mostly A Superstition.\nLife Is Either A Daring Adventure Or Nothing.”\n-Helen Keller",
            "“You Are Never Too Old To Set Another Goal Or To Dream A New Dream.”\n-C.S. Lewis",
            //Reference https://www.briantracy.com/blog/personal-success/26-motivational-quotes-for-success/
        };
        
        void Start()
        {
            GetComponent<Text>().text = messages[Random.Range(0, messages.Length - 1)];
        }
    }

}