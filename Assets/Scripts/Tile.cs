using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private byte[] rgb; //color of it
    private int actualSide; //actual side it belongs to
    private int number; //number in the 3d array
    private bool selected; //is clicked on 


    public void Initialize(byte[] color, int side, int num)
    {
        rgb = color;
        actualSide = side;
        number = num;
        selected = false;
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.color = new Color32(rgb[0], rgb[1], rgb[2], 255);
    }

    public int getNumber()
    {
        return number;
    }

    public int getSide()
    {
        return actualSide;
    }

    public int rgbSum()
    {
        int s = 0;
        s += rgb[0] + rgb[1] + rgb[2];
        return s;
    }


    private Vector2 mousePos;

    //makes sure that the person wasn't rotating the cube
    public void OnMouseDown()
    {
        mousePos = Input.mousePosition;
        Debug.Log("ye");
    }

    public void OnMouseUpAsButton()
    {
        Vector2 mousePos2 = Input.mousePosition;
        if(mousePos.x == mousePos2.x && mousePos.y == mousePos2.y)
        {
            selected = true;
        }
    }

    public bool isSelected()
    {
        return selected;
    }

    public void notSelected()
    {
        selected = false;
    }

}
