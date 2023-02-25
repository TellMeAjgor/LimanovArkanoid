using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    #region Singleton

    private static BlocksManager _instance;
    public static BlocksManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    [SerializeField]
    private Block blockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            InitBlock(new Vector2(0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Block InitBlock(Vector2 position)
    {
        return Instantiate(blockPrefab, position, Quaternion.identity);
    }
}
