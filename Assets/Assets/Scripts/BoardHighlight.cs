using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardHighlight : MonoBehaviour {

    public static BoardHighlight Instance { get; set; }

    public GameObject HighlightPrefab;
    public List<GameObject> Highlights;

	// Use this for initialization
	void Start() {
        Instance = this;
        Highlights = new List<GameObject>();
    }
	
    private GameObject GetHiighlightObject()
    {
        GameObject go = Highlights.Find(g => !g.activeSelf);

        if (go == null)
        {
            go = Instantiate(HighlightPrefab);
            Highlights.Add(go);
        }

        return go;
    }

    public void HighlightAllowedMoves(bool[,] moves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = GetHiighlightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i, 0, j);
                }
            }
        }
    }
	
    //public void HideHighlights()
    //{
    //    foreach (GameObject go in Highlights)
    //        go.SetActive(false);
    //}
}
