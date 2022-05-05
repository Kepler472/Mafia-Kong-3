using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISaveable
{
    public static Spawner instance;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float minTime = 3f;
    [SerializeField] private float maxTime = 5f;
    public List<GameObject> barrelList = new List<GameObject>();

    void Start()
    {
        instance = this;
        Spawn();
    }
    void Spawn()
    {

        GameObject barrel = Instantiate(prefab, transform.position, Quaternion.identity);
        barrelList.Add(barrel);

        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
    public void DeleteBarrel(GameObject barrel)
    {
        barrelList.Remove(barrel);
        Destroy(barrel);
    }
    public void AddBarrel(float[] position)
    {
        GameObject barrel = Instantiate(prefab, new Vector2(position[0], position[1]), Quaternion.identity);
        barrelList.Add(barrel);
    }

    public void PopulateSaveData(SaveData saveData)
    {
        foreach (GameObject barrel in barrelList)
        {
            SaveData.BarrelData barrelData = new SaveData.BarrelData();

            barrel.GetComponent<Barrel>().PopulateSaveData(barrelData);

            // barrelData.position = new float[2];
            // barrelData.velocity = new float[2];

            // barrelData.position[0] = rigidbody.transform.position.x;
            // barrelData.position[1] = rigidbody.transform.position.y;

            // barrelData.velocity[0] = rigidbody.velocity.x;
            // barrelData.velocity[1] = rigidbody.velocity.y;

            saveData.barreList.Add(barrelData);
        }
    }
    public void LoadFromSaveData(SaveData saveData)
    {
        foreach (SaveData.BarrelData barrelData in saveData.barreList)
        {
            Spawner.instance.AddBarrel(barrelData.position);
        }
    }
}
