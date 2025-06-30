using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Dialogue2Boss : MonoBehaviour
{
    public GameObject bossBar;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;
    public TextMeshProUGUI dialogueName;

    private string[] names;
    private string[] sentences;
    private Sprite[] images;

    private int index = 0;
    [HideInInspector] public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private string currentSentence;
    private Coroutine typingCoroutine;

    public AudioClip typingSound;

    private AudioSource audioSource;

    private Color[] colors;
    private Color lightBlue = new Color(0, 255, 255, 255);

    void Start()
    {
        images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Garance angry"), //1
            Resources.Load<Sprite>("sprites/Garance furious"), //2
            Resources.Load<Sprite>("sprites/Samira smug"), //3
            Resources.Load<Sprite>("sprites/Garance irreverent"), //4
            Resources.Load<Sprite>("sprites/Maverick convinced"), //5
            Resources.Load<Sprite>("sprites/Maverick angry"), //6
            Resources.Load<Sprite>("sprites/Garance default"), //7
            Resources.Load<Sprite>("sprites/Garance psycho"), //8
            Resources.Load<Sprite>("sprites/Garance convinced"), //9
            Resources.Load<Sprite>("sprites/Samira surprised"), //10
            Resources.Load<Sprite>("sprites/Maverick furious"), //11
            Resources.Load<Sprite>("sprites/Samira pissed-off"), //12
            Resources.Load<Sprite>("sprites/Maverick convinced"), //13
            Resources.Load<Sprite>("sprites/Maverick convinced talkin'"), //14
            Resources.Load<Sprite>("sprites/Samira cheeky (OuO)"), //15
            Resources.Load<Sprite>("sprites/Maverick determined talkin'") //16
        };
        audioSource = GetComponent<AudioSource>();
        dialoguePanel.SetActive(false);
        names = new string[]{
                "GARANCE",//1
                "GARANCE",//2
                "SAMIRA",//3
                "GARANCE",//4
                "MAVERICK",//5
                "MAVERICK",//6
                "GARANCE",//7
                "GARANCE",//8
                "GARANCE",//9
                "SAMIRA",//10
                "MAVERICK",//11
                "SAMIRA",//12
                "MAVERICK",//13
                "MAVERICK",//14
                "SAMIRA",//15
                "MAVERICK"//16
            };
        colors = new Color[]{
                Color.red,//1
                Color.red,//2
                lightBlue,//3
                Color.red,//4
                lightBlue,//5
                lightBlue,//6
                Color.red,//7
                Color.red,//8
                Color.red,//9
                lightBlue,//10
                lightBlue,//11
                lightBlue,//12
                lightBlue,//13
                lightBlue,//14
                lightBlue,//15
                lightBlue//16
            };
    }

    void Update()
    {
        if (GlobalVariables.bossCounter == 50 && sentences == null)
        {
            sentences = new string[] {
            "TSK...", //1
            "YOU WEREN'T SUPPOSED TO HAVE THOSE FOOLISH ROCKETS", //2
            "I KNEW THEY WOULD BE USEFUL", //3
            "THAT WAS ONLY A CHEAP TRICK", //4
            "WELL. THAT CHEAP TRICK JUST BROUGHT YOU DOWN, HAVEN'T IT~?", //5
            "YOUR AIRSHIP HAVE TAKEN TOO MUCH DAMAGE ALREADY", //6
            "HA...", //7
            "NOT ENOUGH FOR YOU TO CATCH ME, HANDSOME~!", //8
            "I AM NOT LOSING HERE! AHUHUHUHU~", //9
            "WHAT!?", //10
            "DAMMIT! THOSE RASCALS SURELY KNOW HOW TO ESCAPE...", //11
            "LET'S JUST MAKE SURE WE CATCH HER NEXT TIME", //12
            "YEAH, WE DEFEATED HER ONCE", //13
            "WHICH ACTUALLY MEANS WE CAN DO IT TWICE", //14
            "HECK YEAH, YOU DORK", //15
            "NOW... IT'S TIME TO PAY A VISIT TO THE LAST DISTRICT" //16
        };

            StartDialogue();
        }

        if (dialoguePanel.activeInHierarchy &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.L) || Input.GetKey(KeyCode.C)))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                isTyping = false;
                dialogueText.text = currentSentence;
            }
            else
            {
                index++;
                if (index < sentences.Length)
                {
                    currentSentence = sentences[index];
                    dialogueImage.sprite = images[index];
                    dialogueName.text = names[index];
                    dialogueName.color = colors[index];
                    typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    public void StartDialogue()
    {
        bossBar.SetActive(false);
        index = 0;
        dialoguePanel.SetActive(true);
        currentSentence = sentences[index];
        dialogueImage.sprite = images[index];
        dialogueName.text = names[index];
        dialogueName.color = colors[index];
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            audioSource.Play();
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        sentences = null;
        SceneManager.LoadScene("Victory");
    }
}