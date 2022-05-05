using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : Observer
{
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.DeleteAll();
        var poi = FindObjectOfType<PlayerLife>();
        poi.RegisterObserver(this);
    }

    public override void OnNotify(object value, NotificationType nt)
    {
        if(nt == NotificationType.AchievementUnlocked)
        {
            string achievementKey = "achievement-" + value;

            if(PlayerPrefs.GetInt(achievementKey) == 1){
                return;
            }
            PlayerPrefs.SetInt(achievementKey, 1);
            Debug.Log("Unlocked " + achievementKey);

        }
        if(nt == NotificationType.AchievementProgress)
        {
            string achievementKey = "achievement-progress-" + value;

            if(PlayerPrefs.GetInt(achievementKey) < 10){
                int progress = PlayerPrefs.GetInt(achievementKey);
                progress++;
                PlayerPrefs.SetInt(achievementKey, progress);
                Debug.Log("Progress "+ progress);

                return;
            }else{
                OnNotify(value, NotificationType.AchievementUnlocked);
            }

        }
    }
}
