using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageCreator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI durabilityTextMeshPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject blockPrefab;

    private int BLOCK_NUM_X = 6;
    private int BLOCK_NUM_Y = 3;
    private float STAGE_SIZE_X = 4.0f;
    private float STAGE_SIZE_Y = 8.5f;
    private float distance = 0.8f;

    private List<Block> blockList = new List<Block>();

    void Start()
    {
        for (int i = 0; i < BLOCK_NUM_X; i++)
        {
            for (int j = 0; j < BLOCK_NUM_Y; j++)
            {
                GameObject block = Instantiate(blockPrefab, transform.position, Quaternion.identity);
                block.transform.position = new Vector3((i * distance - (STAGE_SIZE_X / 2)), ((STAGE_SIZE_Y / 2) - j * distance), 0.0f);
                var blockObject = block.GetComponent<Block>();
                blockObject.SetDurability(Random.Range(25, 80));
                blockObject.Initialize(durabilityTextMeshPrefab, canvas);
                blockList.Add(blockObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
