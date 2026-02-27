using UnityEngine;

public class vine : MonoBehaviour
{
    [SerializeField] private float stepBoost = 250f;
    [SerializeField] private float startpos = 2f;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.enabled = true;
            // other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + startpos);
            other.transform.SetParent(transform);
        }
    }
    void JumpOff()
    {
        GameObject.FindGameObjectWithTag("Player").transform.SetParent(null);
        GameObject.FindGameObjectWithTag("Player").transform.rotation = Quaternion.identity;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().AddForce(new Vector2(0, stepBoost));

    }
}
