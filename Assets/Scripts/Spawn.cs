using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject entityGO;
    public bool hasSpawned;
    
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        enemySpawner = GetComponentInParent<EnemySpawner>();
    }

    private void Update()
    {
        if (hasSpawned == true)
        {
            InstantiateEntity();
            hasSpawned = false;
        }
    }

    public void InstantiateEntity()
    {
        GameObject entity = Instantiate(entityGO, transform.position,Quaternion.identity);
        entity.transform.SetParent(this.transform);
        entity.transform.localScale = new Vector3(1, 1, 1);
        enemySpawner.entityOnVawe.Add(entity.GetComponent<Entity>());
        entityGO = null;
    }
}