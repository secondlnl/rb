using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private bool got = false;
    [SerializeField] private GameObject checkPart;
    [SerializeField] private AudioClip Sound;
    private AudioSource audi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && got == false)
        {
            GameObject.FindGameObjectWithTag("Spawn").transform.position = transform.position;
            audi.PlayOneShot(Sound, 0.5f);
            Instantiate(checkPart, transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().color = Color.red;
            got = true;
            GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
