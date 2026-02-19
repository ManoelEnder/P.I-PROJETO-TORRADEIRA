using UnityEngine;

[CreateAssetMenu(fileName = "Novo Item", menuName = "Inventario/Item")]
public class ItemData : ScriptableObject
{
    public string itemNome; 
    public Sprite icone;    
    public GameObject prefab; 
}