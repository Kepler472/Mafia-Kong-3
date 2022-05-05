using UnityEngine;

public class PlayerLife : Subject
{
    private PowerUpState powerUps;

    [SerializeField] private string achievmentName = "barrels";

    void Start()
    {
        powerUps = GetComponent<PowerUpState>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Manager manager = FindObjectOfType<Manager>();
        if (manager != null)
        {
            if (collision.gameObject.CompareTag("Objective"))
            {
                Debug.Log("Completed Level");
                manager.LevelComplete();
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                // rb.bodyType = RigidbodyType2D.Static;
                Debug.Log("Barrel Collision");
                if (collision.gameObject.layer == LayerMask.NameToLayer("Barrel"))
                {
                    Spawner.instance.DeleteBarrel(collision.gameObject);
                    Debug.Log("Notification sending");
                    Notify(achievmentName, NotificationType.AchievementProgress);
                }
                if(powerUps.Shield){
                    powerUps.DestroyShield();
                }else{
                    manager.LevelFailed(collision.gameObject.layer == LayerMask.NameToLayer("Tonny"), false);
                }
            }
        }
    }

    void OnBecameInvisible()
    {
        Manager manager = FindObjectOfType<Manager>();
        if(!manager.levelFinished){
            Destroy(gameObject);
            Debug.Log("Player is out of view");
            manager.LevelFailed(false, true);
        }
    }
}
