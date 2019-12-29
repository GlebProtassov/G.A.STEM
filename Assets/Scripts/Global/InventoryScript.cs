using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryScript
{
    static public List<InvCell> Inventories = new List<InvCell>();
    static public bool IsInit;
}
public class InvCell
{
    public InvCell(Vector3 invLoc)
    {
        invLocation = invLoc;
    }

    public delegate void InventoryStoreHandler(GameObject element);
    public delegate void InventoryItemLooseHandler();
    public event InventoryStoreHandler OnElementStored;
    public event InventoryItemLooseHandler OnElementLoose;

    public Vector3 invLocation;
    public float MagneticRadius = 1f;


    private bool IsStorageEmpty = true;
    public bool IsEmpty
    {
        get
        {
            return IsStorageEmpty;
        }
        set
        {
            IsStorageEmpty = value;
            if (IsStorageEmpty == true)
                OnElementLoose?.Invoke();
        }
    }
    private GameObject myStorage;
    public GameObject storage
    {
        get
        {
            OnElementLoose?.Invoke();
            return myStorage;
        }
        set
        {
            IsStorageEmpty = false;
            myStorage = value;
            OnElementStored?.Invoke(myStorage);
        }
    }
    public static void forge()
    {
        Element.elementType myType;

        Element A, B, C;

        A = InventoryScript.Inventories[9].storage.GetComponent<Element>();
        B = InventoryScript.Inventories[10].storage.GetComponent<Element>();
        C = InventoryScript.Inventories[8].storage.GetComponent<Element>();

        switch (MathTask.curentAnswer)
        {
            case (1):
                myType = Element.elementFusion(A.myType, B.myType);
                break;
            case (2):
                myType = Element.elementFusion(A.myType, B.myType);
                break;
            case (3):
                myType = Element.elementFusion(A.myType, B.myType);
                break;
        }

    }
}
