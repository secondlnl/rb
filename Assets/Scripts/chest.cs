using UnityEngine;

public class chest : MonoBehaviour
{
    [SerializeField] private GameObject chestPanel;
    [SerializeField] private GameObject chestPart;
    [SerializeField] private AudioClip pickup;
    [SerializeField] private Sprite open;
    private SpriteRenderer sr;
    private AudioSource audi;
    private bool isOpened = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audi = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        chestPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            chestPanel.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.X))
            {
                sr.sprite = open;
                isOpened = true;
                other.GetComponent<PlayerController>().GetSword();
                audi.PlayOneShot(pickup, 0.5f);
                Instantiate(chestPart, transform.position, Quaternion.identity);
            }

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && chestPanel.activeInHierarchy)
        {
            chestPanel.SetActive(false);
        }
    }
}
