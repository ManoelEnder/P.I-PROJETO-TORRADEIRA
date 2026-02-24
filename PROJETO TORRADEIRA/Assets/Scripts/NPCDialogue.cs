using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public Text dialogueText;
    public GameObject interactionPrompt; 

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
        if (interactionPrompt != null) interactionPrompt.SetActive(false);
    }

    void Update()
    {
        if (!playerInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!dialoguePanel.activeInHierarchy)
            {
              
                if (interactionPrompt != null) interactionPrompt.SetActive(false);
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
        if (lines == null || lines.Length == 0) return;

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

     
        if (playerInside && interactionPrompt != null)
            interactionPrompt.SetActive(true);
    }

    void FreezePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<CharacterController>();
            if (playerController != null) playerController.enabled = false;
        }
    }

    void UnfreezePlayer()
    {
        if (playerController != null) playerController.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            
            if (interactionPrompt != null && !dialoguePanel.activeInHierarchy)
                interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
           
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);

            EndDialogue();
        }
    }
}