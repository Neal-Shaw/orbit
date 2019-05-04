using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public static float energy = 101f;
    public static float score = 0;
    public GameObject scoreText = null;

    // Start is called before the first frame update
    void Start()
    {
        energy = 101f;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<TextMesh>().text = ((int)score).ToString();
        if (energy >= 100f)
            energy = 100f;
    }
}
