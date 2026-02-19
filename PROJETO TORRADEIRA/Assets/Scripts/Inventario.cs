using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public List<string> itens = new List<string>();

    public void AdicionarItem(string item)
    {
        itens.Add(item);
        Debug.Log("Item coletado: " + item);
    }
}
