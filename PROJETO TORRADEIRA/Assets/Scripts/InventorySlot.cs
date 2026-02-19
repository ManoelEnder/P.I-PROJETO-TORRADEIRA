using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconeExibicao; 
    public bool ocupado = false;

    public void AdicionarItem(ItemData novoItem)
    {
        if (iconeExibicao != null)
        {
            iconeExibicao.sprite = novoItem.icone;
            iconeExibicao.enabled = true;          
            ocupado = true;
        }
    }
}