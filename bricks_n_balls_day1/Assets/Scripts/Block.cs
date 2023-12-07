using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI durabilityTextMeshPrefab;
    [SerializeField] private Canvas canvas;
    private int durability = 5;
    private TextMeshProUGUI durabilityText;


    void Start()
    {
        durabilityText = Instantiate(durabilityTextMeshPrefab, transform.position, Quaternion.identity, transform);
        durabilityText.transform.SetParent(canvas.transform);
        durabilityText.transform.localScale = new Vector3(1, 1, 1);
        durabilityText.text = durability.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {

            durability--;
            if (durability <= 0)
            {
                Destroy(durabilityText.gameObject);
                Destroy(gameObject);
            }
            durabilityText.text = durability.ToString();
        }
    }
}
