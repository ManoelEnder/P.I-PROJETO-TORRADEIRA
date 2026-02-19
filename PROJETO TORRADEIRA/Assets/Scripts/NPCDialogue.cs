using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public Text dialogueText;

    [Header("Texto do diálogo")]
    [TextArea(2, 6)]
    public string[] lines;

    [Header("Velocidade da digitação")]
    public float typingSpeed = 0.04f;

    private int index = 0;
    private bool playerInside = false;
    private bool isTyping = false;

    private CharacterController playerController;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (!playerInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (!dialoguePanel.activeInHierarchy)
            {
                StartDialogue();
            }
          
            else if (!isTyping)
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
     
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("NPCDialogue: array 'lines' está vazio!");
            return;
        }

        index = 0;
        dialoguePanel.SetActive(true);
        FreezePlayer();
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        index++;

        if (index < lines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

     
        if (index < 0 || index >= lines.Length)
        {
            Debug.LogError("NPCDialogue: índice inválido → " + index);
            isTyping = false;
            yield break;
        }

        foreach (char c in lines[index])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        UnfreezePlayer();
    }

    void FreezePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<CharacterController>();
            if (playerController != null)
                playerController.enabled = false;
        }
    }

    void UnfreezePlayer()
    {
        if (playerController != null)
            playerController.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            EndDialogue();
        }
    }
}