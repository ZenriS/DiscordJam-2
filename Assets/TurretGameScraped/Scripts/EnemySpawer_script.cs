using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawer_script : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public float Offsett;

    void Start()
    {
        SpawnEnemies(5,5);
    }

    public void SpawnEnemies(int w, int h)
    {
        for (int x = 0; x <= w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                GameObject e = Instantiate(EnemyPrefabs[0], this.transform.position, Quaternion.identity, this.transform);
                Vector2 pos = new Vector2(this.transform.position.x +(x*Offsett), this.transform.position.y + (y *Offsett));
                e.transform.position = pos;

            }
        }
    }
}
