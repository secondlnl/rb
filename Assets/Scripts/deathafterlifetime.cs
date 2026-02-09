using UnityEngine;

public class deathafterlifetime : MonoBehaviour
{
    [SerializeField] private float Lifetime = 1f;

    void Start()
    {
        Destroy(gameObject, Lifetime);
    }
}
