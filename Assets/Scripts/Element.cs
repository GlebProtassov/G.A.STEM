using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    static public float SpeedMultipluer = 0.3f;
    public Vector3 SpriteOfset;
    public enum elementType
    {
        None,
        Earth,
        Water,
        Air,
        Fire,
        Magma,
        Slime,
        Ice,
        Rock
    }

    public elementType myType;

    private InvCell lastStorage;
    public int StartInv;
    // Start is called before the first frame update
    void Start()
    {
        if (!InventoryScript.IsInit) 
        {
            InventoryScript.Inventories.Add(new InvCell(new Vector3(-5.79f,0,0)));
            InventoryScript.Inventories.Add(new InvCell(new Vector3(-5.79f, -1.115f, 0)));
            InventoryScript.Inventories.Add(new InvCell(new Vector3(-5.79f, -2.363f, 0)));
            InventoryScript.Inventories.Add(new InvCell(new Vector3(-5.79f, -3.63f, 0)));

            InventoryScript.Inventories.Add(new InvCell(new Vector3(5.79f, 0, 0)));
            InventoryScript.Inventories.Add(new InvCell(new Vector3(5.79f, -1.115f, 0)));
            InventoryScript.Inventories.Add(new InvCell(new Vector3(5.79f, -2.363f, 0)));
            InventoryScript.Inventories.Add(new InvCell(new Vector3(5.79f, -3.63f, 0)));

            InventoryScript.Inventories.Add(new InvCell(new Vector3(-0.2f, -0.63f, 0)));
            InventoryScript.Inventories.Add(new InvCell(new Vector3(-0.57f, -3.05f, 0)));
            InventoryScript.Inventories.Add(new InvCell(new Vector3(2.08f, -1.08f, 0)));
            InventoryScript.IsInit = true;

            MathTask.initTrianglesList();
        }
        checkInv();
    }
    private bool selected;
    private bool homeReached;
    void Update()
    {
        if (selected == true)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        } else
        {
            if (!homeReached)
                runHome();
        }
        if (Input.GetMouseButtonUp(0))
        {
            selected = false;
            if (!homeReached)
                checkInv();
        }


    }

    Vector3 getMyPosition 
    {
        get
        {
            return this.transform.position + SpriteOfset;
        }
        set
        {
            this.transform.position = value - SpriteOfset;
        }
    }

    private void runHome()
    {
        if ((getMyPosition - lastStorage.invLocation).magnitude >= lastStorage.MagneticRadius/3)
            getMyPosition += (lastStorage.invLocation - getMyPosition).normalized * SpeedMultipluer;
        if ((getMyPosition - lastStorage.invLocation).magnitude <= lastStorage.MagneticRadius/3)
        {
            homeReached = true;
            lastStorage.storage = this.gameObject;
            getMyPosition = lastStorage.invLocation;
        }
    }

    void checkInv()
    {
        foreach (InvCell inventory in InventoryScript.Inventories)
        {
            if ((getMyPosition - inventory.invLocation).magnitude <= inventory.MagneticRadius && inventory.IsEmpty)
            {
                inventory.storage = this.gameObject;
                lastStorage = inventory;
                goto abort_scan;
            }
        }
    abort_scan:;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
            homeReached = false;
            lastStorage.IsEmpty = true;
        }
    }

    static public elementType elementFusion(elementType tp1, elementType tp2)
    {
        switch (tp1)
        {
            case elementType.Fire:
                switch (tp2)
                {
                    case elementType.Water:
                        return elementType.Slime;
                    case elementType.Rock:
                        return elementType.Magma;
                    default:
                        return elementType.None;
                }

                break;
            case elementType.Water:
                switch (tp2)
                {
                    case elementType.Rock:
                        return elementType.Ice;
                    case elementType.Magma:
                        return elementType.Rock;
                    case elementType.Fire:
                        return elementType.Slime;
                    default:
                        return elementType.None;
                }

                break;
            case elementType.Rock:
                switch (tp2)
                {
                    case elementType.Water:
                        return elementType.Ice;
                    case elementType.Fire:
                        return elementType.Magma;
                    default:
                        return elementType.None;
                }

                break;
            case elementType.Magma:
                switch (tp2)
                {
                    case elementType.Water:
                        return elementType.Rock;
                    default:
                        return elementType.None;
                }
               break;
                default : 
                return elementType.None; 
        }
    }
}
