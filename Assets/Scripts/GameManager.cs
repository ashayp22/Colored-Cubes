using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    //prefabs
    public Cube cubePrefab;
    public Tile tilePrefab;

    //instances
    private Cube cubeInstance;
    private List<Tile> tileList = new List<Tile>();

    //rotations
    private bool moveCube = false;

    //related to the cube/tiles
    private byte[][] cornerColors = new byte[][]
    {
        new byte[] { 255, 0, 0}, new byte[]  { 255, 127, 0}, new byte[]  { 255, 255, 0}, new byte[]  { 0, 255, 0}, new byte[]  { 0, 0, 255}, new byte[]  { 139, 0, 255}
    };

    private int[][][] cubeRepresentation = new int[6][][];

    //camera
    public Camera camera;


    //game over
    private bool gameOver = false;
    private int time = 0;
    private int tilesLeft = 0;

    //text
    public Text timerText;
    public Text tileText;
    public Text topleftText;
    public Text highscoreText;

    public GameObject winPanel;

	// Use this for initialization
	void Start () {
        //sets highscore
        if(!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetInt("highscore", 1800);
        }

        createCube(); //creates the cube
        initialize3DArray(); //initializes the array

        //creates the tiles
        createTiles();

        //cubeRepresentation = solveCube();

        randomizeCube();

        displayTiles(); //sets up on the screen

        InvokeRepeating("increaseTime", 1f, 1f); //increases timer
    }

    private void createCube()
    {
        //creates the cube
        cubeInstance = Instantiate(cubePrefab) as Cube;
        cubeInstance.transform.position = new Vector3(0, 0, 0);
        cubeInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
        cubeInstance.transform.localScale = new Vector3(1, 1, 1);
    }

    private void initialize3DArray()
    {

        //initializes the cube
        for (int i = 0; i < cubeRepresentation.Length; i++) //second layer
        {
            cubeRepresentation[i] = new int[3][];
        }

        for (int i = 0; i < cubeRepresentation.Length; i++)
        {
            for (int j = 0; j < cubeRepresentation[i].Length; j++) //third layer
            {
                cubeRepresentation[i][j] = new int[3];
            }
        }
    }

    private void increaseTime()
    {
        time++;
    }

    private void randomizeCube()
    {
        //randomizes
        for (int i = 0; i < 100; i++)
        {
            int a1 = Random.Range(0, 6);
            int b1 = Random.Range(0, 3);
            int c1 = Random.Range(0, 3);

            int a2 = Random.Range(0, 6);
            int b2 = Random.Range(0, 3);
            int c2 = Random.Range(0, 3);

            int temp = cubeRepresentation[a1][b1][c1];
            cubeRepresentation[a1][b1][c1] = cubeRepresentation[a2][b2][c2];
            cubeRepresentation[a2][b2][c2] = temp;

        }
    }


    private void createTiles() //creates the tiles
    {
    
        //6 sides

        ///------------------------------------------
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {

                //creates tile
                Tile t = Instantiate(tilePrefab) as Tile;
                t.transform.parent = cubeInstance.transform; //sets the parent transform

                //size, position, rotation
                t.transform.localScale = new Vector3(0.33f, 0.33f, 0.005f);

                //alters the color
                byte[] tileColor = clone1dbyte(cornerColors[0]);

                tileColor[0] -= (byte)Random.Range(0, 240);

                t.Initialize(tileColor, 0, (0 * 9) + (i * 3) + j); //initialzes its data

                cubeRepresentation[0][i][j] = (0 * 9) + (i * 3) + j; //sets the representation cube with its number

                tileList.Add(t); //adds to list
            }
        }
        ///------------------------------------------

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Tile t = Instantiate(tilePrefab) as Tile;

                t.transform.parent = cubeInstance.transform;

                t.transform.localScale = new Vector3(0.33f, 0.33f, 0.005f);

                byte[] tileColor = clone1dbyte(cornerColors[1]);

                tileColor[1] += (byte)(Random.Range(0, 40) *randomMultiplier());

                t.Initialize(tileColor, 1, (1 * 9) + (i * 3) + j);

                cubeRepresentation[1][i][j] = (1 * 9) + (i * 3) + j;

                tileList.Add(t);
            }
        }
        ///------------------------------------------

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Tile t = Instantiate(tilePrefab) as Tile;

                t.transform.parent = cubeInstance.transform;

                t.transform.localScale = new Vector3(0.33f, 0.33f, 0.005f);

                byte[] tileColor = clone1dbyte(cornerColors[2]);

                int val = Random.Range(0, 75);

                tileColor[0] -= (byte)val;
                tileColor[1] -= (byte)val;

                t.Initialize(tileColor, 2, (2 * 9) + (i * 3) + j);

                cubeRepresentation[2][i][j] = (2 * 9) + (i * 3) + j;


                tileList.Add(t);
            }
        }
        ///------------------------------------------

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Tile t = Instantiate(tilePrefab) as Tile;

                t.transform.parent = cubeInstance.transform;

                t.transform.localScale = new Vector3(0.33f, 0.33f, 0.005f);

                byte[] tileColor = clone1dbyte(cornerColors[3]);

                tileColor[1] -= (byte)Random.Range(0, 240);


                t.Initialize(tileColor, 3, (3 * 9) + (i * 3) + j);

                cubeRepresentation[3][i][j] = (3 * 9) + (i * 3) + j;


                tileList.Add(t);
            }
        }
        ///------------------------------------------

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Tile t = Instantiate(tilePrefab) as Tile;

                t.transform.parent = cubeInstance.transform;

                t.transform.localScale = new Vector3(0.33f, 0.33f, 0.005f);

                byte[] tileColor = clone1dbyte(cornerColors[4]);

                tileColor[2] -= (byte)Random.Range(0, 240);

                t.Initialize(tileColor, 4, (4 * 9) + (i * 3) + j);

                cubeRepresentation[4][i][j] = (4 * 9) + (i * 3) + j;


                tileList.Add(t);
            }
        }
        ///------------------------------------------

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Tile t = Instantiate(tilePrefab) as Tile;

                t.transform.parent = cubeInstance.transform;

                t.transform.localScale = new Vector3(0.33f, 0.33f, 0.005f);

                byte[] tileColor = clone1dbyte(cornerColors[5]);

                tileColor[0] += (byte)(Random.Range(0, 75) * 1);
                //tileColor[2] -= (byte)Random.Range(0, 50);

                t.Initialize(tileColor, 5, (5 * 9) + (i * 3) + j);

                cubeRepresentation[5][i][j] = (5 * 9) + (i * 3) + j;

                tileList.Add(t);
            }
        }


    }

    private void displayTiles() //displays the tiles on the screen with appropriate pos and rotation, based on the cube representation array - never called repeadetly, only after change
    {
        Quaternion originalrotation = cubeInstance.transform.rotation;

        Debug.Log(originalrotation);

        //makes sure the cube is at original position
        cubeInstance.transform.position = new Vector3(0, 0, 0);
        cubeInstance.transform.rotation = Quaternion.Euler(0, 0, 0);

        //for all six sides
        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                updateTile(cubeRepresentation[0][i][j], new Vector3(0, 0, 0), new Vector3(-0.33f + (0.33f * i), -0.33f + (0.33f * j), -0.5f));
            }
        }

        //------------------------------------
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                updateTile(cubeRepresentation[1][i][j], new Vector3(0, 0, 0), new Vector3(-0.33f + (0.33f * i), 0.33f + (-0.33f * j), 0.5f));
            }
        }

        //------------------------------------

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                updateTile(cubeRepresentation[2][i][j], new Vector3(90, 0, 0), new Vector3(-0.33f + (0.33f * i), 0.5f, -0.33f + (0.33f * j)));
            }
        }

        //------------------------------------

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                updateTile(cubeRepresentation[3][i][j], new Vector3(90, 0, 0), new Vector3(-0.33f + (0.33f * i), -0.5f, 0.33f + (-0.33f * j)));
            }
        }

        //------------------------------------

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                updateTile(cubeRepresentation[4][i][j], new Vector3(0, 90, 0), new Vector3(-0.5f, -0.33f + (0.33f * i), -0.33f + (0.33f * j)));
            }
        }

        //------------------------------------

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                updateTile(cubeRepresentation[5][i][j], new Vector3(0, 90, 0), new Vector3(0.5f, -0.33f + (0.33f * i), 0.33f + (-0.33f * j)));
            }
        }

        cubeInstance.transform.rotation = originalrotation;

    }

    private void updateTile(int number, Vector3 rotation, Vector3 position) //based on the number in the cube representation array, it finds the appropriate tile and updates it
    {
        for(int i = 0; i < tileList.Count; i++)
        {
            if(tileList[i].getNumber() == number)
            {
                tileList[i].transform.rotation = Quaternion.Euler(rotation);
                tileList[i].transform.position = position;
            }
        }
    }


    public void Update()
    {
        if (!gameOver) //as long as the game isn't over
        {
            checkWin(); //checks for win
            checkSwitch(); //checks for switching tiles
            rotateCube(); //rotates the cube
            if (Input.GetKeyDown("space"))
            {
                cubeRepresentation = solveCube();
                displayTiles();
            }
            showText();
        } else
        {
            winPanel.SetActive(true);

            if(Input.GetMouseButtonDown(0))
            {

                //restart
                restart();

            }
        }
    }

    private void showText()
    {
        if((time % 60) < 10)
        {
            timerText.text = "" + (time / 60) + ":0" + (time % 60);
        } else
        {
            timerText.text = "" + (time / 60) + ":" + (time % 60);
        }

        tileText.text = "tiles left: " + tilesLeft;

        int bestTime = PlayerPrefs.GetInt("highscore");
        string t = "";

        if ((bestTime % 60) < 10)
        {
            t = "" + (bestTime / 60) + ":0" + (bestTime % 60);
        }
        else
        {
            t = "" + (bestTime / 60) + ":" + (bestTime % 60);
        }

        Debug.Log(t);

        highscoreText.text = "Best Time: " + t;
    }

    private void checkWin() //checks for win
    {
        int[][][] solved = solveCube();

        bool win = true; //assumes win
        int t = 0;

        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                for(int z = 0; z < 3; z++)
                {
                    if(cubeRepresentation[i][j][z] != solved[i][j][z]) //if one tile isn't correct, no win
                    {
                        win = false;
                        t++;
                    }
                }
            }
        }

        tilesLeft = t;

        if(win)
        {
            //update highscore
            if (time < 10000)
            {
                if (time < PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", time);
                }
            }
            CancelInvoke();
            gameOver = true;
        }


    }

    private void checkSwitch() //checks for switching tiles
    {
        int t1 = -1;
        int t2 = -1;
        for(int i = 0; i < tileList.Count; i++) //checks for selected and gets their number
        {
            if(tileList[i].isSelected())
            {
                if(t1 == -1)
                {
                    t1 = tileList[i].getNumber();
                } else
                {
                    t2 = tileList[i].getNumber();
                    break;
                }
            }
        }

        if(t1 != -1 && t2 != -1) //two are selected
        {

            Debug.Log("worked");
            //positions
            int[] c1 = new int[3];
            int[] c2 = new int[3];

            for(int i = 0; i < cubeRepresentation.Length; i++)
            {
                for(int j = 0; j < cubeRepresentation[i].Length; j++)
                {
                    for(int z = 0; z < cubeRepresentation[i][j].Length; z++)
                    {
                        //gets the position in 3d array
                        if(t1 == cubeRepresentation[i][j][z])
                        {
                            c1[0] = i;
                            c1[1] = j;
                            c1[2] = z;
                        } else if(t2 == cubeRepresentation[i][j][z])
                        {
                            c2[0] = i;
                            c2[1] = j;
                            c2[2] = z;
                        }
                    }
                }
            }

            //swaps
            cubeRepresentation[c1[0]][c1[1]][c1[2]] = t2;
            cubeRepresentation[c2[0]][c2[1]][c2[2]] = t1;

            //resets
            for(int a = 0; a < tileList.Count; a++)
            {
                tileList[a].notSelected();
            }

            displayTiles(); //updates the tiles
            hideText(); //hides hint
        }

    }

    private void rotateCube() //rotates the cube through mouse click
    {
        //mouse

        if (Input.GetMouseButtonDown(0)) //button down
        {
            moveCube = true;
        }
        else if (Input.GetMouseButtonUp(0)) //button released
        {
            moveCube = false;
        }

        if (moveCube) //can move the cube
        {
            //source: https://www.youtube.com/watch?v=S3pjBQObC90
            float rotX = Input.GetAxis("Mouse X") * 5 * Mathf.Deg2Rad; //gets the axis and multiplies by 5 converts to radians
            float rotY = Input.GetAxis("Mouse Y") * 5 * Mathf.Deg2Rad; //same as above

            cubeInstance.transform.RotateAround(Vector3.up, -rotX); //rotates using above information
            cubeInstance.transform.RotateAround(Vector3.right, rotY);
        }
    }

    private int randomMultiplier() //returns a random multiplier, -1 or 1
    {
        if(Random.Range(0, 1) == 0)
        {
            return 1; 
        } else
        {
            return -1;
        }
    }

    private byte[] clone1dbyte(byte[] arr) //returns cloned 1d array of bytes
    {
        byte[] copy = new byte[arr.Length];
        for(int i = 0; i < arr.Length; i++)
        {
            copy[i] = arr[i];
        }
        return copy;
    }

    private int[][][] clone3dbyte(int[][][] arr) //returns cloned 1d array of bytes
    {
        int[][][] copy = new int[arr.Length][][];

        for (int i = 0; i < arr.Length; i++) //second layer
        {
            copy[i] = new int[3][];
        }

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr[i].Length; j++) //third layer
            {
                copy[i][j] = new int[3];
            }
        }

        for (int i = 0; i < arr.Length; i++)
        {
            for(int j = 0; j < arr[i].Length; j++)
            {
                for(int z = 0; z < arr[i][j].Length; z++)
                {
                    copy[i][j][z] = arr[i][j][z];
                }
            }
        }

        return copy;
    }


    private int[][][] solveCube() //returns a solved version of the cube, with the positions(main algorithm)
    {
        int[][][] good = clone3dbyte(cubeRepresentation);
        //step 1: makes sure every tile is on correct side
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                for(int z = 0; z < 3; z++)
                {
                    int currentNumber = good[i][j][z];
                    int side = getSideFromNumber(currentNumber);
                    while(side != i) //while it isn't on the correct side
                    {
                        //replaces with another on its side

                        bool found = false;

                        for(int a = 0; a < 3; a++)
                        {
                            for(int b = 0; b < 3; b++)
                            {
                                int checkingNumber = good[side][a][b];
                                int checkingSide = getSideFromNumber(checkingNumber);

                                if(checkingSide == i) //belongs to the previous side
                                {
                                    //swaps
                                    good[side][a][b] = currentNumber;
                                    good[i][j][z] = checkingNumber;
                                    found = true;
                                    break;
                                }
                            }

                            if(found) //breaks out of loop
                            {
                                break;
                            }

                        }

                        if(!found) //not found replacement
                        {
                            for(int a = 0; a < 3; a++)
                            {
                                for(int b = 0; b < 3; b++)
                                {
                                    int checkingNumber = good[side][a][b];
                                    int checkingSide = getSideFromNumber(checkingNumber);

                                    if(checkingSide != side) //makes sure that the tile being replaced isn't in the correct spot already
                                    {
                                        good[side][a][b] = currentNumber;
                                        good[i][j][z] = checkingNumber;
                                        found = true;
                                        break;
                                    }

                                }

                                if (found) //breaks out of loop
                                {
                                    break;
                                }
                            }
                        }

                        //new number at the position, so updates
                        currentNumber = good[i][j][z];
                        side = getSideFromNumber(currentNumber);

                    }
                }
            }
            
        }



        //step 2: sort in ascending order - darker number is first(bottom left)

        for(int i = 0; i < 6; i++)
        {
            int[] numbers = new int[9]; //numbers
            int[] values = new int[9]; //sum of rgb

            for(int j = 0; j < 3; j++) //sets up the array
            {
                for(int z = 0; z < 3; z++)
                {
                    numbers[j * 3 + z] = good[i][j][z];
                    values[j * 3 + z] = getSumFromNumber(good[i][j][z]);
                }
            }

            //------------------------------------------------------------------------

            int r = Random.RandomRange(0, 4);

            //selection sort
            
            if(r == 0)
            {
                numbers = selectionSort(values, numbers);
            } else if(r == 1)
            {
                numbers = insertionSort(values, numbers);
            }
            else
            {
                mergeSort(values, numbers, 0, 8); //sorts the array
   
            }




            //------------------------------------------------------------------------

            //puts back into place
            for (int a = 0; a < 3; a++)
            {
                for(int b = 0; b < 3; b++)
                {
                    good[i][a][b] = numbers[a * 3 + b];
                }
            }


        }



        return good;
    }



    //sorting algoirthms

    private int[] selectionSort(int[] v, int[] n)
    {
        int[] values = clone1dintArray(v);
        int[] numbers = clone1dintArray(n);

        for (int a = 0; a < 9; a++)
        {
            for (int b = a; b < 9; b++)
            {
                if (values[a] > values[b])
                {
                    //swaps
                    //values
                    int temp1 = values[a];
                    values[a] = values[b];
                    values[b] = temp1;
                    //number
                    int temp2 = numbers[a];
                    numbers[a] = numbers[b];
                    numbers[b] = temp2;
                }
            }
        }


        return numbers;
    }

    private int[] insertionSort(int[] v, int[] n)
    {
        int[] values = clone1dintArray(v);
        int[] numbers = clone1dintArray(n);

        for (int i = 1; i < 9; i++)
        {
            int key = values[i]; //value
            int original = numbers[i];
            int j = i - 1; //index being checked

            while (j >= 0 && values[j] > key) //while you don't reach the end the the value being checked is greater
            {
                //moves it up one
                values[j + 1] = values[j];
                numbers[j + 1] = numbers[j];
                j--;
            }

            values[j + 1] = key;
            numbers[j + 1] = original;
        }

        return numbers;
    }


    private void mergeSort(int[] Arr, int[] Arr2, int start, int end)
    {

        if (start < end)
        {
            int mid = (start + end) / 2;
            mergeSort(Arr, Arr2, start, mid);
            mergeSort(Arr, Arr2, mid + 1, end);
            merge(Arr, Arr2, start, mid, end);
        }
    }

    void merge(int[] Arr, int[] Arr2, int start, int mid, int end)
    {

        // create a temp array
        int[] temp = new int[end - start + 1];
        int[] temp2 = new int[end - start + 1];


        // crawlers for both intervals and for temp
        int i = start, j = mid + 1, k = 0;

        // traverse both arrays and in each iteration add smaller of both elements in temp 
        while (i <= mid && j <= end)
        {
            if (Arr[i] <= Arr[j])
            {
                temp[k] = Arr[i];
                temp2[k] = Arr2[i];
                k += 1; i += 1;
            }
            else
            {
                temp[k] = Arr[j];
                temp2[k] = Arr2[j];
                k += 1; j += 1;
            }
        }

        // add elements left in the first interval 
        while (i <= mid)
        {
            temp[k] = Arr[i];
            temp2[k] = Arr2[i];
            k += 1; i += 1;
        }

        // add elements left in the second interval 
        while (j <= end)
        {
            temp[k] = Arr[j];
            temp2[k] = Arr2[j];
            k += 1; j += 1;
        }

        // copy temp to original interval
        for (i = start; i <= end; i += 1)
        {
            Arr[i] = temp[i - start];
        }

        // copy temp to original interval
        for (i = start; i <= end; i += 1)
        {
            Arr2[i] = temp2[i - start];
        }
    }




    private int[] clone1dintArray(int[] arr)
    {
        int[] copy = new int[arr.Length];
        for(int i = 0; i < arr.Length; i++)
        {
            copy[i] = arr[i];
        }
        return arr;
    }

    //---------------------------------------------------
    private int getSumFromNumber(int number)
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            if (tileList[i].getNumber() == number)
            {
                return tileList[i].rgbSum();
            }
        }
        return -1;
    }

    private int getSideFromNumber(int number)
    {
        for(int i = 0; i < tileList.Count; i++)
        {
            if(tileList[i].getNumber() == number)
            {
                return tileList[i].getSide();
            }
        }
        return -1;
    }


    public void sideFacing(int i)
    {
        switch(i)
        {
            case 0:
                cubeInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                cubeInstance.transform.rotation = Quaternion.Euler(-180, 0, 0);
                break;
            case 2:
                cubeInstance.transform.rotation = Quaternion.Euler(-90, 0, 0);
                break;
            case 3:
                cubeInstance.transform.rotation = Quaternion.Euler(90, 0, 0);
                break;
            case 4:
                cubeInstance.transform.rotation = Quaternion.Euler(-90, 0, -90);
                break;
            case 5:
                cubeInstance.transform.rotation = Quaternion.Euler(90, 0, -90);
                break;
            default: break;
        }
    }

    private int[] getWrongTiles()
    {
        int[][][] correct = solveCube();

        int[] wrong = new int[6];
        for(int i = 0; i < 6; i++)
        {
            int w = 0;
            for(int a = 0; a < 3; a++)
            {
                for(int b = 0; b < 3;  b++)
                {
                    if(correct[i][a][b] != cubeRepresentation[i][a][b])
                    {
                        w++;
                    }
                }
            }
            wrong[i] = w;
        }

        return wrong;
    }


    public void displayInfoText()
    {
        topleftText.text = "INFO\nMove the cube around by dragging and \ntap on two colored tiles to swap them.\nIn order to win, place \nall the tiles in their correct spot.\nIn order to know where \ntheir correct spot is,\nuse the images on the \nright side of the screen.";
    }

    public void hideText()
    {
        topleftText.text = "";
    }


    public void displayhintText()
    {
        int[] wrong = getWrongTiles();

        string text = "Red Side - " + wrong[0] + " wrong";
        text += "\nOrange Side - " + wrong[1] + " wrong";
        text += "\nYellow Side - " + wrong[2] + " wrong";
        text += "\nGreen Side - " + wrong[3] + " wrong";
        text += "\nBlue Side - " + wrong[4] + " wrong";
        text += "\nPurple Side - " + wrong[5] + " wrong";
        topleftText.text = "HINT\n" + text;
        time += 60; //increases the time by a minute
    }


    private void restart()
    {
        //delete the cube

        Destroy(cubeInstance.gameObject); //destroys the gameobject

        tileList = new List<Tile>();

        cubeRepresentation = new int[6][][];

        //creates(from start)

        createCube(); //creates the cube
        initialize3DArray(); //initializes the array

        //creates the tiles
        createTiles();

        //cubeRepresentation = solveCube();

        randomizeCube();

        displayTiles(); //sets up on the screen


        //resets instance fields
        time = 0;
        moveCube = false;
        tilesLeft = 0;

        //on screen
        winPanel.SetActive(false);

        gameOver = false;

        InvokeRepeating("increaseTime", 1f, 1f); //increases timer

    }




    /*
      -highscore

      */







}
