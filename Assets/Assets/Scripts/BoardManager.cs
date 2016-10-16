using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    public static BoardManager Instance { get; set; }

    private const float TILE_SIZE = 1f, TILE_OFFSET = 0.5f;           //Constant values for tile's size and center.

    public List<GameObject> Chesses;                                  //List of chesses.
    private List<GameObject> ActiveChesses = new List<GameObject>();  //List of activeChesses.
    public List<Light> Lights = new List<Light>();                    //List of lights.

    public bool[,] allowedMoves { get; set; }

    private int selectionX = -1;                                      //Corrent selection on X axis.
    private int selectionY = -1;                                      //Corrent selection on Y axis.

    public Chessman[,] Chessmans { get; set; }                       //Array with chesses on board.
    private Chessman selectedChessman;                                //Corrent selected chessman.

    public bool isWhiteTurn = true;                                   //Checker for turn.
    public Camera mainCam;                                            //Main camera.

    private float cameraBlindTime = 0f;                               //How long should camera be blind after a turn.

    private Material lastMat, selectedMat;

    // Use this for initialization
    void Start() {
        Instance = this;  
        DrawChesses();                                                //Draw all chesses.
    }

    // Update is called once per frame
    void Update() {
        UpdateSelection();                                            //Update corrent selection.

        if (cameraBlindTime <= 0f) {                                  //Camera blinds after every turn.
            if (Input.GetMouseButtonDown(0)) {                        //Chech for left mouse button input.
                if (selectionX >= 0 && selectionY >= 0) {             //Check for selections.
                    if (selectedChessman == null) {
                        SelectChessman(selectionX, selectionY);       //Select Chessman if there's no selection.
                    }
                    else {
                        MoveChessman(selectionX, selectionY);         //Try to move it otherwise.
                    }
                }
            }
        }
        else {                                                        //Script for lights.
            if (cameraBlindTime < 1.5f) {
                if (isWhiteTurn) {                 
                    mainCam.transform.position = new Vector3(4f, 7f, -1f);
                    mainCam.transform.rotation = Quaternion.Euler(60f, 0f, 0f);
                }
                else {
                    mainCam.transform.position = new Vector3(4f, 7f, 9f);
                    mainCam.transform.rotation = Quaternion.Euler(60f, 180f, 0f);
                }             
                for (int i = 0; i < Lights.Count; i++)
                    Lights[i].intensity += 0.04f;
            }
            else {
                for (int i = 0; i < Lights.Count; i++)
                    Lights[i].intensity -= 0.04f;
            }
            cameraBlindTime -= 0.02f;
        }
    }

    private void SelectChessman(int x, int y) {
        foreach (GameObject go in BoardHighlight.Instance.Highlights)
            Destroy(go);

        BoardHighlight.Instance.Highlights.Clear();

        if (Chessmans[x, y] == null)
            return;
        
        if (Chessmans[x, y].isWhite != isWhiteTurn)
            return;

        bool hasAtLeastOneMove = false;

        allowedMoves = Chessmans[x, y].PossibleMove();

        for (int i = 0; i < 8 && hasAtLeastOneMove != true; i++)
        {
            for (int j = 0; j < 8 && hasAtLeastOneMove != true; j++)
            {
                if (allowedMoves[i, j])   
                    hasAtLeastOneMove = true;             
            }
        }

        if (!hasAtLeastOneMove)
        {
            Chessmans[x, y].GetComponentInChildren<Light>().intensity = 0;
            return;
        }

        selectedChessman = Chessmans[x, y];
        Chessmans[x, y].GetComponentInChildren<Light>().intensity = 6.5f;

        BoardHighlight.Instance.HighlightAllowedMoves(allowedMoves);
    }           //Selecting the chessman.
    private void MoveChessman(int x, int y) {
        if (allowedMoves[x, y]) {
            Chessman c = Chessmans[x, y];

            if (c != null && c.isWhite != isWhiteTurn)
            {
                if (c.GetType() == typeof(King))
                {
                    EndGame();
                    return;
                }

                ActiveChesses.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;           
            selectedChessman.transform.position = GetCenter(x, y);
            selectedChessman.CurrentX = x;
            selectedChessman.CurrentY = y;
            Chessmans[x, y] = selectedChessman;
            isWhiteTurn = !isWhiteTurn;
            cameraBlindTime = 3f;      
        }

        foreach (GameObject go in BoardHighlight.Instance.Highlights)
            Destroy(go);

        BoardHighlight.Instance.Highlights.Clear();
        Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY].GetComponentInChildren<Light>().intensity = 0;
        selectedChessman = null;
    }             //Move the chessman.
    private void SpawnChessman(int index, int x, int y) 
    {
        GameObject go = Instantiate(Chesses[index], GetCenter(x, y), Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        ActiveChesses.Add(go);
    } //Spawn the chessman.

    private void DrawChesses() {

        Chessmans = new Chessman[8, 8];

        //Draw Blacks

        SpawnChessman(0, 0, 0);   //Rook       
        SpawnChessman(1, 1, 0);   //Knight       
        SpawnChessman(2, 2, 0);   //Bishop      
        SpawnChessman(3, 3, 0);   //King       
        SpawnChessman(4, 4, 0);   //Queen     
        SpawnChessman(2, 5, 0);   //Bishop      
        SpawnChessman(1, 6, 0);   //Knight      
        SpawnChessman(0, 7, 0);   //Rook

        for (int i = 0; i < 8; i++)          //Pawns
            SpawnChessman(5, i, 1);

        //Draw Whites

        SpawnChessman(6, 0, 7);   //Rook       
        SpawnChessman(7, 1, 7);   //Knight       
        SpawnChessman(8, 2, 7);   //Bishop      
        SpawnChessman(9, 3, 7);   //King       
        SpawnChessman(10, 4, 7);  //Queen     
        SpawnChessman(8, 5, 7);   //Bishop      
        SpawnChessman(7, 6, 7);   //Knight      
        SpawnChessman(6, 7, 7);   //Rook

        for (int i = 0; i < 8; i++)          //Pawns
            SpawnChessman(11, i, 6);    
    }                          //Draws a single chessman.

    private void UpdateSelection() {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, LayerMask.GetMask("Board"))) {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else {
            selectionX = -1;
            selectionY = -1;
        }
    }                      //Update for corrent selection

    private Vector3 GetCenter(int x, int y) {
        Vector3 origin = Vector3.zero;
        origin.x += (x * TILE_SIZE) + TILE_OFFSET;
        origin.z += (y * TILE_SIZE) + TILE_OFFSET;
        return origin;
    }             //Returns the center of a tile.

    private void EndGame()
    {
        if(isWhiteTurn)
        {
            
        }
        else
        {

        }

        foreach (GameObject go in ActiveChesses)
            Destroy(go);

        foreach (GameObject go in BoardHighlight.Instance.Highlights)
            Destroy(go);

        BoardHighlight.Instance.Highlights.Clear();


        DrawChesses();
    }
}
