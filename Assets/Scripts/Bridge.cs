using NUnit.Framework;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private GameObject box;
    private Animator anim;
    private bool hasPlayed = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            box.SetActive(false);
            anim.SetTrigger("fall");
        }
    }
}
