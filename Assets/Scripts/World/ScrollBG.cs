using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float speed = 4f;

    private readonly float speedMultiplier = 0.1f;
    private float offset;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed * speedMultiplier;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
