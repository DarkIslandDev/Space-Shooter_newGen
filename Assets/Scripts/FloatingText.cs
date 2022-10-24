using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 3f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}