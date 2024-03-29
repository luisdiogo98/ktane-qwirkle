﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;
using KModkit;
using rnd = UnityEngine.Random;

public class qwirkleScript : MonoBehaviour
{
    public KMBombInfo bomb;
    public KMAudio Audio;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    Tile[][] board;
    List<Tile> placed = new List<Tile>();
    int stage = 0;
    bool row6 = false;

    int selectedColor = 0;
    int selectedShape = 0;

    public GameObject[] tiles;
    public GameObject[] stageObj;
    public GameObject selected;
    public Material[] tileMats;
    public Material emptyMat;
    public Material blackMat;
    public Material greenMat;
    public KMSelectable[] sideBtns;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        tiles[0].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(0); return false; };
        tiles[1].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(1); return false; };
        tiles[2].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(2); return false; };
        tiles[3].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(3); return false; };
        tiles[4].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(4); return false; };
        tiles[5].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(5); return false; };
        tiles[6].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(6); return false; };
        tiles[7].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(7); return false; };
        tiles[8].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(8); return false; };
        tiles[9].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(9); return false; };
        tiles[10].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(10); return false; };
        tiles[11].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(11); return false; };
        tiles[12].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(12); return false; };
        tiles[13].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(13); return false; };
        tiles[14].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(14); return false; };
        tiles[15].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(15); return false; };
        tiles[16].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(16); return false; };
        tiles[17].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(17); return false; };
        tiles[18].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(18); return false; };
        tiles[19].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(19); return false; };
        tiles[20].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(20); return false; };
        tiles[21].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(21); return false; };
        tiles[22].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(22); return false; };
        tiles[23].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(23); return false; };
        tiles[24].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(24); return false; };
        tiles[25].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(25); return false; };
        tiles[26].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(26); return false; };
        tiles[27].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(27); return false; };
        tiles[28].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(28); return false; };
        tiles[29].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(29); return false; };
        tiles[30].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(30); return false; };
        tiles[31].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(31); return false; };
        tiles[32].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(32); return false; };
        tiles[33].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(33); return false; };
        tiles[34].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(34); return false; };
        tiles[35].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(35); return false; };
        tiles[36].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(36); return false; };
        tiles[37].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(37); return false; };
        tiles[38].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(38); return false; };
        tiles[39].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(39); return false; };
        tiles[40].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(40); return false; };
        tiles[41].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(41); return false; };
        tiles[42].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(42); return false; };
        tiles[43].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(43); return false; };
        tiles[44].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(44); return false; };
        tiles[45].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(45); return false; };
        tiles[46].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(46); return false; };
        tiles[47].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(47); return false; };
        tiles[48].GetComponentInChildren<KMSelectable>().OnInteract += delegate () { PressGrid(48); return false; };
        sideBtns[0].OnInteract += delegate () { PressSide(0, -1); return false; };
        sideBtns[1].OnInteract += delegate () { PressSide(0, 1); return false; };
        sideBtns[2].OnInteract += delegate () { PressSide(-1, 0); return false; };
        sideBtns[3].OnInteract += delegate () { PressSide(1, 0); return false; };
    }

    void PressSide(int colorDelta, int shapeDelta)
    {
        Audio.PlaySoundAtTransform("select", transform);
        selectedColor += colorDelta;
        selectedShape += shapeDelta;
        if (selectedColor > 5) selectedColor = 0;
        if (selectedColor < 0) selectedColor = 5;
        if (selectedShape > 5) selectedShape = 0;
        if (selectedShape < 0) selectedShape = 5;

        selected.GetComponentInChildren<Renderer>().material = tileMats[selectedColor * 6 + selectedShape];
    }

    void PressGrid(int btn)
    {
        if (moduleSolved)
            return;

        int row = btn / 7;
        int column = btn % 7;

        if (!board[row][column].IsEmpty())
        {
            Debug.LogFormat("[Qwirkle #{0}] Strike! Tried to place a tile at {1}{2}, which isn't empty.", moduleId, (char)(column + 65), row + 1);
            GetComponent<KMBombModule>().HandleStrike();
            Restart();
            return;
        }

        if (!CheckValidTile(row, column))
        {
            Debug.LogFormat("[Qwirkle #{0}] Strike! Tried to place a tile at {1}{2}, which has no adjacent tiles.", moduleId, (char)(column + 65), row + 1);
            GetComponent<KMBombModule>().HandleStrike();
            Restart();
            return;
        }

        if (!CheckConnectors(row, column, selectedColor, selectedShape))
        {
            Debug.LogFormat("[Qwirkle #{0}] Strike! Tried to place {1}_{2} at {3}{4}, which violates placement rules.", moduleId, new Tile(selectedColor, selectedShape).GetColorName(), new Tile(selectedColor, selectedShape).GetShapeName(), (char)(column + 65), row + 1);
            GetComponent<KMBombModule>().HandleStrike();
            Restart();
            return;
        }

        board[row][column] = new Tile(selectedColor, selectedShape);
        placed.Add(board[row][column]);
        stageObj[stage].GetComponentInChildren<Renderer>().material = greenMat;
        stage++;
        Debug.LogFormat("[Qwirkle #{0}] Successfully placed {1}_{2} at {3}{4}.", moduleId, board[row][column].GetColorName(), board[row][column].GetShapeName(), (char)(column + 65), row + 1);

        if (stage == 1)
        {
            StartCoroutine("HideBoard");
        }

        if (stage == 4)
        {
            Debug.LogFormat("[Qwirkle #{0}] Module solved.", moduleId);
            Audio.PlaySoundAtTransform("solve", transform);
            moduleSolved = true;
            GetComponent<KMBombModule>().HandlePass();
            StartCoroutine("ShowBoard");
        }
        else
        {
            Audio.PlaySoundAtTransform("placement", transform);
            ApplyStageEffects();
        }

    }

    void ApplyStageEffects()
    {
        Tile[][] newBoard = new Tile[][] {
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) }
                             };

        if (stage == 1)
        {
            if (row6)
            {
                Debug.LogFormat("[Qwirkle #{0}] Initial board had line of 6 tiles. Rotating 180º.", moduleId);
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 7; j++)
                        newBoard[6 - i][6 - j] = board[i][j];

                board = newBoard;
            }
            else
            {
                Debug.LogFormat("[Qwirkle #{0}] Initial board did not have a line of 6 tiles. No effect.", moduleId);
            }
        }
        else if (stage == 2)
        {
            if (placed.ElementAt(1).shape == Tile.star4 || placed.ElementAt(1).shape == Tile.star8 || placed.ElementAt(1).shape == Tile.cross)
            {
                Debug.LogFormat("[Qwirkle #{0}] Previously placed tile was not a square, diamond, or circle. Mirroring about the X-axis.", moduleId);
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 7; j++)
                        newBoard[6 - i][j] = board[i][j];

                board = newBoard;
            }
            else
            {
                Debug.LogFormat("[Qwirkle #{0}] Previously placed tile was a square, diamond, or circle. Mirroring about the Y-axis.", moduleId);
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 7; j++)
                        newBoard[i][6 - j] = board[i][j];

                board = newBoard;
            }
        }
        else if (stage == 3)
        {
            if ((placed.ElementAt(0).color == placed.ElementAt(1).color && placed.ElementAt(0).color != placed.ElementAt(2).color) ||
                (placed.ElementAt(1).color == placed.ElementAt(2).color && placed.ElementAt(1).color != placed.ElementAt(0).color) ||
                (placed.ElementAt(0).color == placed.ElementAt(2).color && placed.ElementAt(0).color != placed.ElementAt(1).color))
            {
                Debug.LogFormat("[Qwirkle #{0}] Exactly two of the previous three tiles placed were the same color. Rotating 90° CW.", moduleId);
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 7; j++)
                        newBoard[j][6 - i] = board[i][j];

                board = newBoard;
            }
            else
            {
                Debug.LogFormat("[Qwirkle #{0}] Not exactly two of the previous three tiles placed were the same color. Rotating 90° CCW.", moduleId);
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 7; j++)
                        newBoard[6 - j][i] = board[i][j];

                board = newBoard;
            }
        }

        Debug.LogFormat("[Qwirkle #{0}] Stage {1} board: {2}", moduleId, stage + 1, GetBoardString());
    }

    void Restart()
    {
        stage = 0;
        row6 = false;
        foreach (GameObject stage in stageObj)
            stage.transform.GetComponentInChildren<Renderer>().material = blackMat;
        placed = new List<Tile>();
        GenerateBoard();
    }

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        foreach (GameObject tile in tiles)
            tile.transform.GetComponentInChildren<Renderer>().material = emptyMat;

        board = new Tile[][] {
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) },
                                new Tile[] { new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty), new Tile(Tile.empty) }
                             };

        List<int> priority = Enumerable.Range(0, 49).ToList().OrderBy(x => rnd.Range(0, 1000)).ToList();
        List<int> order = new List<int>();
        bool firstTile = true;

        int cnt = 0;

        while (priority.Count() != 0)
        {
            int row = priority[0] / 7;
            int column = priority[0] % 7;

            if (cnt == priority.Count()) break;

            if (!CheckValidTile(row, column) && !firstTile)
            {
                cnt++;
                priority.Add(priority.ElementAt(0));
                priority.RemoveAt(0);
                continue;
            }

            cnt = 0;

            firstTile = false;

            List<Tile> possibilities = GetPossibilities(row, column, true);

            if (possibilities.Count() != 0)
            {
                board[row][column] = possibilities.OrderBy(x => rnd.Range(0, 1000)).ElementAt(0);
                order.Add(priority.ElementAt(0));
                tiles[priority.ElementAt(0)].transform.GetComponentInChildren<Renderer>().material = tileMats[board[row][column].color * 6 + board[row][column].shape];
            }
            priority.RemoveAt(0);
        }

        for (int i = order.Count() - 7; i < order.Count; i++)
        {
            int row = order[i] / 7;
            int column = order[i] % 7;
            board[row][column] = new Tile(Tile.empty);
            tiles[order.ElementAt(i)].transform.GetComponentInChildren<Renderer>().material = emptyMat;
        }

        CheckRow6();

        Debug.LogFormat("[Qwirkle #{0}] Stage 1 board: {1}", moduleId, GetBoardString());
    }

    bool CheckValidTile(int row, int column)
    {
        if (row != 0 && !board[row - 1][column].IsEmpty())
            return true;

        if (column != 0 && !board[row][column - 1].IsEmpty())
            return true;

        if (row != 6 && !board[row + 1][column].IsEmpty())
            return true;

        if (column != 6 && !board[row][column + 1].IsEmpty())
            return true;

        return false;
    }

    List<Tile> GetPossibilities(int row, int column, bool boring)
    {
        List<Tile> ret = new List<Tile>();

        bool[][] down = GetColumnPossibilities(row, column, true);
        bool[][] up = GetColumnPossibilities(row, column, false);
        bool[][] right = GetRowPossibilities(row, column, true);
        bool[][] left = GetRowPossibilities(row, column, false);

        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 6; j++)
                if (down[i][j] && up[i][j] && right[i][j] && left[i][j] && (!boring || !BoringBoard(row, column)) && CheckConnectors(row, column, i, j))
                    ret.Add(new Tile(i, j));

        return ret;
    }

    bool BoringBoard(int row, int column)
    {
        List<Tile> adj = new List<Tile>();

        if (row != 0 && !board[row - 1][column].IsEmpty())
            adj.Add(board[row - 1][column]);
        if (row != 6 && !board[row + 1][column].IsEmpty())
            adj.Add(board[row + 1][column]);
        if (column != 0 && !board[row][column - 1].IsEmpty())
            adj.Add(board[row][column - 1]);
        if (column != 6 && !board[row][column + 1].IsEmpty())
            adj.Add(board[row][column + 1]);

        if (adj.Count() > 1)
            return true;

        return false;
    }

    bool CheckConnectors(int row, int column, int color, int shape)
    {
        List<int> connectColor = new List<int>();
        List<int> connectShape = new List<int>();

        for (int i = 0; i < 7; i++)
        {
            int nextColor, nextShape;

            if (row == i)
            {
                nextColor = color;
                nextShape = shape;
            }
            else
            {
                if (board[i][column].IsEmpty())
                {
                    connectColor = new List<int>();
                    connectShape = new List<int>();
                    continue;
                }

                nextColor = board[i][column].color;
                nextShape = board[i][column].shape;
            }

            if (connectColor.Count() == 0 && connectShape.Count() == 0)
            {
                connectColor.Add(nextColor);
                connectShape.Add(nextShape);
                continue;
            }

            if (connectColor.Exists(x => x == nextColor) && connectShape.Exists(x => x == nextShape))
                return false;
            else if (!connectColor.All(x => x == nextColor) && !connectShape.All(x => x == nextShape))
                return false;

            connectColor.Add(nextColor);
            connectShape.Add(nextShape);
        }

        connectColor = new List<int>();
        connectShape = new List<int>();

        for (int i = 0; i < 7; i++)
        {
            int nextColor, nextShape;

            if (column == i)
            {
                nextColor = color;
                nextShape = shape;
            }
            else
            {
                if (board[row][i].IsEmpty())
                {
                    connectColor = new List<int>();
                    connectShape = new List<int>();
                    continue;
                }

                nextColor = board[row][i].color;
                nextShape = board[row][i].shape;
            }

            if (connectColor.Count() == 0 && connectShape.Count() == 0)
            {
                connectColor.Add(nextColor);
                connectShape.Add(nextShape);
                continue;
            }

            if (connectColor.Exists(x => x == nextColor) && connectShape.Exists(x => x == nextShape))
                return false;
            else if (!connectColor.All(x => x == nextColor) && !connectShape.All(x => x == nextShape))
                return false;

            connectColor.Add(nextColor);
            connectShape.Add(nextShape);
        }

        return true;
    }

    bool[][] GetColumnPossibilities(int row, int column, bool direction)
    {
        bool[][] ret = new bool[][] {
                                        new bool[] { true, true, true, true, true, true},
                                        new bool[] { true, true, true, true, true, true},
                                        new bool[] { true, true, true, true, true, true},
                                        new bool[] { true, true, true, true, true, true},
                                        new bool[] { true, true, true, true, true, true},
                                        new bool[] { true, true, true, true, true, true},
                                    };

        for (int i = (direction ? row + 1 : row - 1); i >= 0 && i < 7; i += direction ? 1 : -1)
        {
            if (board[i][column].IsEmpty())
                break;

            for (int j = 0; j < 6; j++)
                for (int k = 0; k < 6; k++)
                    if ((j != board[i][column].color && k != board[i][column].shape) || (j == board[i][column].color && k == board[i][column].shape))
                        ret[j][k] = false;
        }

        return ret;
    }

    bool[][] GetRowPossibilities(int row, int column, bool direction)
    {
        bool[][] ret = new bool[][] {
                                        new bool[] { true, true, true, true, true, true},
                                        new bool[] { true, true, true, true, true, true},
                                        new bool[] { true, true, true, true, true, true},
                                        new bool[] { true, true, true, true, true, true},
                                        new bool[] { true, true, true, true, true, true},
                                        new bool[] { true, true, true, true, true, true},
                                    };

        for (int i = (direction ? column + 1 : column - 1); i >= 0 && i < 7; i += direction ? 1 : -1)
        {
            if (board[row][i].IsEmpty())
                break;

            for (int j = 0; j < 6; j++)
                for (int k = 0; k < 6; k++)
                    if ((j != board[row][i].color && k != board[row][i].shape) || (j == board[row][i].color && k == board[row][i].shape))
                        ret[j][k] = false;
        }

        return ret;
    }

    void CheckRow6()
    {
        for (int i = 0; i < 7; i++)
        {
            int accHor = 0;
            int accVer = 0;
            for (int j = 0; j < 7; j++)
            {
                if (board[i][j].IsEmpty()) accHor = 0;
                else accHor++;
                if (board[j][i].IsEmpty()) accVer = 0;
                else accVer++;

                if (accHor == 6 || accVer == 6)
                {
                    row6 = true;
                    return;
                }
            }
        }
    }

    String GetBoardString()
    {
        String ret = "";

        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
            {
                if (board[i][j].IsEmpty())
                    ret += "empty ";
                else
                    ret += board[i][j].GetColorName() + "_" + board[i][j].GetShapeName() + " ";
            }

        return ret;
    }

    IEnumerator HideBoard()
    {
        List<int> priority = Enumerable.Range(0, 49).ToList().OrderBy(x => rnd.Range(0, 1000)).ToList();
        foreach (int tile in priority)
        {
            tiles[tile].transform.GetComponentInChildren<Renderer>().material = blackMat;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ShowBoard()
    {
        List<int> priority = Enumerable.Range(0, 49).ToList().OrderBy(x => rnd.Range(0, 1000)).ToList();
        foreach (int tile in priority)
        {
            int row = tile / 7;
            int column = tile % 7;

            if (board[row][column].IsEmpty())
            {
                tiles[tile].transform.GetComponentInChildren<Renderer>().material = emptyMat;
                continue;
            }
            tiles[tile].transform.GetComponentInChildren<Renderer>().material = tileMats[board[row][column].color * 6 + board[row][column].shape];
            yield return new WaitForSeconds(0.01f);
        }
    }

    //twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} select <color> <shape> [Selects the specified piece to place; r = red, o = orange, y = yellow, g = green, b = blue, p = purple; s = square, d = diamond, c = circle, cr = cross, 4 = 4-pointstar, 8 = 8-pointstar] | !{0} place in <cell> [Places the selected piece in the specified cell; proper format is <letter><number> where letter is columns A-G (from left to right) and number is row 1-7 (from top to bottom)]";
    #pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*select\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            if (parameters.Length == 3)
            {
                bool color = false;
                bool shape = false;
                string[] validcols = { "r", "red", "o", "orange", "y", "yellow", "g", "green", "b", "blue", "p", "purple" };
                string[] validshas = { "s", "square", "d", "diamond", "c", "circle", "cr", "cross", "4", "4-pointstar", "8", "8-pointstar" };
                for (int i = 0; i < 12; i++)
                {
                    if (validcols[i].EqualsIgnoreCase(parameters[1]))
                    {
                        color = true;
                    }
                    if (validshas[i].EqualsIgnoreCase(parameters[2]))
                    {
                        shape = true;
                    }
                }
                if (shape == true && color == true)
                {
                    yield return null;
                    if (parameters[1].EqualsIgnoreCase("r") || parameters[1].EqualsIgnoreCase("red"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("red"))
                        {
                            sideBtns[3].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else if (parameters[1].EqualsIgnoreCase("o") || parameters[1].EqualsIgnoreCase("orange"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("orange"))
                        {
                            sideBtns[3].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else if (parameters[1].EqualsIgnoreCase("y") || parameters[1].EqualsIgnoreCase("yellow"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("yellow"))
                        {
                            sideBtns[3].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else if (parameters[1].EqualsIgnoreCase("g") || parameters[1].EqualsIgnoreCase("green"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("green"))
                        {
                            sideBtns[3].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else if (parameters[1].EqualsIgnoreCase("b") || parameters[1].EqualsIgnoreCase("blue"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("blue"))
                        {
                            sideBtns[3].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else if (parameters[1].EqualsIgnoreCase("p") || parameters[1].EqualsIgnoreCase("purple"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("purple"))
                        {
                            sideBtns[3].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    if (parameters[2].EqualsIgnoreCase("s") || parameters[2].EqualsIgnoreCase("square"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("square"))
                        {
                            sideBtns[0].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else if (parameters[2].EqualsIgnoreCase("d") || parameters[2].EqualsIgnoreCase("diamond"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("diamond"))
                        {
                            sideBtns[0].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else if (parameters[2].EqualsIgnoreCase("c") || parameters[2].EqualsIgnoreCase("circle"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("circle"))
                        {
                            sideBtns[0].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else if (parameters[2].EqualsIgnoreCase("cr") || parameters[2].EqualsIgnoreCase("cross"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("cross"))
                        {
                            sideBtns[0].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else if (parameters[2].EqualsIgnoreCase("4") || parameters[2].EqualsIgnoreCase("4-pointstar"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("4point"))
                        {
                            sideBtns[0].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else if (parameters[2].EqualsIgnoreCase("8") || parameters[2].EqualsIgnoreCase("8-pointstar"))
                    {
                        while (!selected.GetComponent<Renderer>().material.name.Contains("8point"))
                        {
                            sideBtns[0].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                }
                else
                {
                    yield return "sendtochaterror I don't think I've ever seen a '" + parameters[1] + " " + parameters[2] + "' Qwirkle piece before...";
                }
                yield break;
            }
        }
        if (Regex.IsMatch(parameters[0], @"^\s*place\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            if(parameters.Length == 3)
            {
                if(Regex.IsMatch(parameters[1], @"^\s*in\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                {
                    if(parameters[2].Length == 2)
                    {
                        bool letter = false;
                        bool number = false;
                        char[] validlets = { 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g' };
                        char[] validnums = { '1', '2', '3', '4', '5', '6', '7' };
                        for (int i = 0; i < 14; i++)
                        {
                            if (validlets[i].Equals(parameters[2].ElementAt(0)))
                            {
                                letter = true;
                            }
                        }
                        for (int i = 0; i < 7; i++)
                        {
                            if (validnums[i].Equals(parameters[2].ElementAt(1)))
                            {
                                number = true;
                            }
                        }
                        if(letter == true && number == true)
                        {
                            yield return null;
                            if (parameters[2].EqualsIgnoreCase("A1"))
                            {
                                tiles[0].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("B1"))
                            {
                                tiles[1].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("C1"))
                            {
                                tiles[2].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("D1"))
                            {
                                tiles[3].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("E1"))
                            {
                                tiles[4].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("F1"))
                            {
                                tiles[5].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("G1"))
                            {
                                tiles[6].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("A2"))
                            {
                                tiles[7].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("B2"))
                            {
                                tiles[8].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("C2"))
                            {
                                tiles[9].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("D2"))
                            {
                                tiles[10].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("E2"))
                            {
                                tiles[11].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("F2"))
                            {
                                tiles[12].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("G2"))
                            {
                                tiles[13].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("A3"))
                            {
                                tiles[14].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("B3"))
                            {
                                tiles[15].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("C3"))
                            {
                                tiles[16].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("D3"))
                            {
                                tiles[17].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("E3"))
                            {
                                tiles[18].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("F3"))
                            {
                                tiles[19].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("G3"))
                            {
                                tiles[20].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("A4"))
                            {
                                tiles[21].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("B4"))
                            {
                                tiles[22].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("C4"))
                            {
                                tiles[23].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("D4"))
                            {
                                tiles[24].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("E4"))
                            {
                                tiles[25].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("F4"))
                            {
                                tiles[26].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("G4"))
                            {
                                tiles[27].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("A5"))
                            {
                                tiles[28].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("B5"))
                            {
                                tiles[29].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("C5"))
                            {
                                tiles[30].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("D5"))
                            {
                                tiles[31].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("E5"))
                            {
                                tiles[32].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("F5"))
                            {
                                tiles[33].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("G5"))
                            {
                                tiles[34].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("A6"))
                            {
                                tiles[35].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("B6"))
                            {
                                tiles[36].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("C6"))
                            {
                                tiles[37].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("D6"))
                            {
                                tiles[38].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("E6"))
                            {
                                tiles[39].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("F6"))
                            {
                                tiles[40].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("G6"))
                            {
                                tiles[41].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("A7"))
                            {
                                tiles[42].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("B7"))
                            {
                                tiles[43].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("C7"))
                            {
                                tiles[44].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("D7"))
                            {
                                tiles[45].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("E7"))
                            {
                                tiles[46].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("F7"))
                            {
                                tiles[47].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                            else if (parameters[2].EqualsIgnoreCase("G7"))
                            {
                                tiles[48].GetComponentInChildren<KMSelectable>().OnInteract();
                            }
                        }
                        else
                        {
                            yield return "sendtochaterror The cell '" + parameters[2] + "' doesn't exist in my grid!";
                        }
                        yield break;
                    }
                }
            }
        }
    }
}