using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public static class MathTask
{
    static GameObject A, B, C;
    static int val_A, val_B, val_C;
    public static int curentAnswer;

    static bool A_stored, B_stored, C_stored;

    static void startTask(int variant)
    {
        if (variant == 0)
        {
            triangle triang = genTriangle();

            GameObject Cat1Text = GameObject.Find("Cat1");
            Cat1Text.GetComponent<Text>().text = triang.a.ToString();
            GameObject Cat2Text = GameObject.Find("Cat2");
            Cat2Text.GetComponent<Text>().text = triang.b.ToString();
            GameObject GippText = GameObject.Find("GippText");
            GippText.GetComponent<Text>().text = "?";

            askQuest(triang.c);
        } else if (variant == 1)
        {
            triangle triang = genTriangle();

            GameObject Cat1Text = GameObject.Find("Cat1");
            Cat1Text.GetComponent<Text>().text = triang.a.ToString();
            GameObject Cat2Text = GameObject.Find("Cat2");
            Cat2Text.GetComponent<Text>().text = "?";
            GameObject GippText = GameObject.Find("GippText");
            GippText.GetComponent<Text>().text = triang.c.ToString();

            askQuest(triang.b);
        } else
        {
            triangle triang = genTriangle();

            GameObject Cat1Text = GameObject.Find("Cat1");
            Cat1Text.GetComponent<Text>().text = "?";
            GameObject Cat2Text = GameObject.Find("Cat2");
            Cat2Text.GetComponent<Text>().text = triang.b.ToString();
            GameObject GippText = GameObject.Find("GippText");
            GippText.GetComponent<Text>().text = triang.c.ToString();

            askQuest(triang.a);
        }
    }

    public static void askQuest(int answ)
    {

        GameObject Answ1Text = GameObject.Find("Answer1");
        GameObject Answ2Text = GameObject.Find("Answer2");
        GameObject Answ3Text = GameObject.Find("Answer3");

        switch (Random.Range(0, 2))
        {

            case 1:
                Answ1Text.GetComponentInChildren<Text>().text = (answ - Random.Range(2, 6)).ToString();
                Answ2Text.GetComponentInChildren<Text>().text = (answ + Random.Range(2, 6)).ToString();
                Answ3Text.GetComponentInChildren<Text>().text = answ.ToString();
                curentAnswer = 3;
                break;
            case 0:
                Answ1Text.GetComponentInChildren<Text>().text = (answ + Random.Range(2, 6)).ToString();
                Answ2Text.GetComponentInChildren<Text>().text = answ.ToString();
                Answ3Text.GetComponentInChildren<Text>().text = (answ - Random.Range(2, 6)).ToString();
                curentAnswer = 2;
                break;
            case 2:
                Answ1Text.GetComponentInChildren<Text>().text = answ.ToString();
                Answ2Text.GetComponentInChildren<Text>().text = (answ - Random.Range(2, 6)).ToString();
                Answ3Text.GetComponentInChildren<Text>().text = (answ + Random.Range(2, 6)).ToString();
                curentAnswer = 1;
                break;
        }

        GameObject.Find("AnswerBox").GetComponent<Canvas>().enabled = true;
    }

    static public void ret_answer(int answ)
    {
        if (answ == curentAnswer)
        {

        }
    }



    public static void Cat1Stored(GameObject element)
    {
        A_stored = true;
        if (B_stored)
            startTask(0);
        if (C_stored)
            startTask(1);
    }
    public static void Cat1Release()
    {
        A_stored = false;
    }
    public static void Cat2Stored(GameObject element)
    {
        B_stored = true;
        if (A_stored)
            startTask(0);
        if (C_stored)
            startTask(2);
    }
    public static void Cat2Release()
    {
        B_stored = false;
    }
    public static void GippStored(GameObject element)
    {
        C_stored = true;
        if (B_stored)
            startTask(2);
        if (A_stored)
            startTask(1);
    }
    public static void GippRelease()
    {
        C_stored = false;
    }

    public class triangle
    {
        public int a, b, c;
        public triangle(int t_a, int t_b, int t_c)
        {
            a = t_a;
            b = t_b;
            c = t_c;
        }
    }

    static private int[] sqareIntA = { 1, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121, 144, 169, 196, 225, 256, 289, 324, 361, 400, 441, 484, 529, 576, 625, 676, 729, 784, 841, 900, 961, 1024, 1089, 1156 };
    static private int[] sqareIntExtendA = { 1, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121, 144, 169, 196, 225, 256, 289, 324, 361, 400, 441, 484, 529, 576, 625, 676, 729, 784, 841, 900, 961, 1024, 1089, 1156, 1225, 1296, 1369, 1444, 1521, 1600, 1681, 1764, 1849, 1936, 2025, 2116, 2209, 2304, 2401, 2500 };
    static private List<int> sqareInt = new List<int>(sqareIntA);
    static private List<int> sqareIntExtend = new List<int>(sqareIntExtendA);

    static public List<triangle> triangles = new List<triangle>();

    static public void initTrianglesList()
    {
        InventoryScript.Inventories[8].OnElementStored += GippStored;
        InventoryScript.Inventories[8].OnElementLoose += GippRelease;
        InventoryScript.Inventories[9].OnElementStored += Cat1Stored;
        InventoryScript.Inventories[9].OnElementLoose += Cat1Release;
        InventoryScript.Inventories[10].OnElementStored += Cat2Stored;
        InventoryScript.Inventories[10].OnElementLoose += Cat2Release;

        foreach (int A in sqareInt)
        {
            foreach (int B in sqareInt)
            {
                if (sqareIntExtend.Contains(A + B))
                    triangles.Add(new triangle(
                        (int)(Mathf.Sqrt(A)),
                        (int)(Mathf.Sqrt(B)),
                        (int)(Mathf.Sqrt(A + B))
                        ));
            }
        }
    }

    static public triangle genTriangle()
    {
        return triangles[(int)(Random.Range(0, triangles.Count))];
    }
}
