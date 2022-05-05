using UnityEngine;

public class Barrel : MonoBehaviour, ISaveableBarrel
{
    private new Rigidbody2D rigidbody;

    [SerializeField] private float speed = 5f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            rigidbody.AddForce(collision.transform.right * speed, ForceMode2D.Impulse);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void PopulateSaveData(SaveData.BarrelData saveData)
    {
        SaveData.BarrelData barrelData = new SaveData.BarrelData();

        barrelData.position = new float[2];
        barrelData.velocity = new float[2];

        barrelData.position[0] = rigidbody.transform.position.x;
        barrelData.position[1] = rigidbody.transform.position.y;

        barrelData.velocity[0] = rigidbody.velocity.x;
        barrelData.velocity[1] = rigidbody.velocity.y;
    }
    public void LoadFromSaveData(SaveData.BarrelData saveData)
    {
        Spawner.instance.AddBarrel(saveData.position);
    }

}