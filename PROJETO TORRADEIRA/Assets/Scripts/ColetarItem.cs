using UnityEngine;

public class ColetarItem : MonoBehaviour
{
    private Inventario inventario;
    private ItemColetavel itemPerto;

    void Start()
    {
        inventario = GetComponent<Inventario>();
    }

    void Update()
    {
        if (itemPerto != null && Input.GetKeyDown(KeyCode.E))
        {
            inventario.AdicionarItem(itemPerto.itemNome);
            Destroy(itemPerto.gameObject);
            itemPerto = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemColetavel item = other.GetComponent<ItemColetavel>();

        if (item != null)
        {
            itemPerto = item;
            Debug.Log("Aperte E para coletar " + item.itemNome);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ItemColetavel>() != null)
        {
            itemPerto = null;
        }
    }
}
