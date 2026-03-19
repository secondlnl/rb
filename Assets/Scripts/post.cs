using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

public class post : MonoBehaviour
{
    [SerializeField] private GameObject lanternPanel, light;
    [SerializeField] private AudioClip pickup, fire;
    [SerializeField] private Color invis;
    private ParticleSystem part;
    private SpriteRenderer sr;
    private AudioSource audi;
    private bool isLit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        audi = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        lanternPanel.SetActive(false);
        isLit = light.activeInHierarchy;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !lanternPanel.activeInHierarchy)
        {
            lanternPanel.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.X))
            {
                if (!isLit && other.GetComponentInChildren<Plinventory>().currentItem == "Lantern")
                {
                    part.Play();
                    audi.PlayOneShot(fire, 0.5f);
                    sr.color = Color.white;
                    isLit = true;
                    light.SetActive(true);
                    other.GetComponentInChildren<Plinventory>().haveLantern = false;
                    other.GetComponentInChildren<Plinventory>().SetCurrentItem("empty", null);
                }
                else if (isLit && other.GetComponentInChildren<Plinventory>().currentItem == "empty")
                {
                    part.Stop();
                    audi.PlayOneShot(pickup, 0.5f);
                    sr.color = invis;
                    isLit = false;

                    light.SetActive(false);
                    other.GetComponentInChildren<Plinventory>().haveLantern = true;
                    other.GetComponentInChildren<Plinventory>().SetCurrentItemLantern();
                }
            }

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && lanternPanel.activeInHierarchy)
        {
            lanternPanel.SetActive(false);
        }
    }
}
