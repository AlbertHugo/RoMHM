using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class Dialogue3Manager : MonoBehaviour
{
    public GameObject spawner;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;

    public TextMeshProUGUI dialogueName;
    private string[] names;
    private string[] sentences;
    private Sprite[] images;
    private Color[] colors;
    private Color lightBlue = new Color(0, 255, 255, 255);

    private int index = 0;
    [HideInInspector] public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private bool dialogueHappened = true;
    private string currentSentence;
    private Coroutine typingCoroutine;

    public AudioClip typingSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (GlobalVariables.waveCounter == 0 && sentences == null&&dialogueHappened)
        {
            dialogueHappened=false;
            sentences = new string[] {
            "THE THIRD DISTRICT...", //1
            "HERE'S THE PLACE THEY TOOK YOU OUT, AIN'T IT?", //2
            "YEAH...", //3
            "THE PHANTOM OMERTA'S BOSS IS HERE", //4
            "HE... HAD SET TRAPS FOR ME EVERYWHERE THROUGH THE DISTRICT", //5
            "THERE WAS NOTHING I COULD DO...", //6
            "BE AWARE, THEY MAY HAVE A FEW *WAYS TO COUNTER YOUR FOCUS*", //7
            "BUT I MADE YOU A *CHARGED SHOT* TO GIVE THEM A LITTLE SURPRISE", //8
            "YOU WILL HEAR A SOUND AND SEE THE ENERGY FLOWING WHEN IT IS CHARGING", //9
            "IT DOES SOUNDS POWERFUL. GOOD JOB, SAM", //10
            "I'LL CATCH THEIR LEADER AND EVERYONE ELSE, YOU KNOW IT", //11
            "TAKE CARE, YOU DORK. AND DON'T UNDERESTIMATE THEM" //12
        };

            images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Samira worried"), //1
            Resources.Load<Sprite>("sprites/Maverick worried"), //2
            Resources.Load<Sprite>("sprites/Samira thinking"), //3
            Resources.Load<Sprite>("sprites/Samira worried"), //4
            Resources.Load<Sprite>("sprites/Samira pissed-off"), //5
            Resources.Load<Sprite>("sprites/Samira thinking"), //6
            Resources.Load<Sprite>("sprites/Samira worried"), //7
            Resources.Load<Sprite>("sprites/Samira smug"), //8
            Resources.Load<Sprite>("sprites/Samira cheeky (OuO)"), //9
            Resources.Load<Sprite>("sprites/Maverick smug talkin'"), //10
            Resources.Load<Sprite>("sprites/Maverick determined talkin'"), //11
            Resources.Load<Sprite>("sprites/Samira reluctant") //12
        };        
            names = new string[]{
                "SAMIRA",//1
                "MAVERICK",//2
                "SAMIRA",//3
                "SAMIRA",//4
                "SAMIRA",//5
                "SAMIRA",//6
                "SAMIRA",//7
                "SAMIRA",//8
                "SAMIRA",//9
                "MAVERICK",//10
                "MAVERICK",//11
                "SAMIRA"//12
            };
            colors = new Color[]{
                lightBlue,//1
                lightBlue,//2
                lightBlue,//3
                lightBlue,//4
                lightBlue,//5
                lightBlue,//6
                lightBlue,//7
                lightBlue,//8
                lightBlue,//9
                lightBlue,//10
                lightBlue,//11
                lightBlue//12
            };

            StartDialogue();
        }
        if(GlobalVariables.waveCounter == 13 &&sentences == null)
        {
            dialogueHappened = false;
            sentences = new string[] {
            "...", //1
            "I'M AMAZED YOU HAVE MANAGED GETTING THIS FAR, HUMAN", //2
            "SAM...? SAM!? WHAT'S GOING ON!?", //3
            "THE CONNECTIO- IS CUTTIN-- OUT", //4
            "I AM SEEING AN AIRSHIP", //5
            "I THINK YOU'RE THE BOSS-MAN HIMSELF, HUH?", //6
            "I AM INDEED. AND YET YOU FINALLY FOUND ME", //7
            "OH SWEET! TIME TO END THIS MISSION AT ONCE!", //8
            "N- IT'S A TR--", //9
            "DO- FOLL-- HIM!", //10
            "IT SEEMS LIKE YOU'RE HAVING SOME LACK OF COMMUNICATION~", //11
            "MAYBE THIS WILL BE THE CAUSE OF YOUR ULTIMATE DEFEAT", //12
            "BUT YOU WON'T RETREAT... WILL YOU? NO, YOU'RE TOO DETERMINED FOR SUCH", //13
            "HEH! OF COURSE NOT, YOU SLIPPERY RASCAL!", //14
            "IT'S TIME TO PAY FOR YOUR CRIMES...", //15
            "YOU BETTER BE PREPARED TO HAVE YOUR ASS KICKED, DUMBASS!", //16
            "THAT'S MORE LIKE IT. I REALLY APPRECIATE THAT ENTUSIASM OF YOURS", //17
            "ALTHOUGH. NOW...", //18
            "YOU ARE EXACTLY WHERE I WANTED!" //19
        };

            images = new Sprite[] {
            Resources.Load<Sprite>("sprites/OM3"), //1
            Resources.Load<Sprite>("sprites/OM1"), //2
            Resources.Load<Sprite>("sprites/Maverick worried"), //3
            Resources.Load<Sprite>("sprites/Samira worried"), //4
            Resources.Load<Sprite>("sprites/Maverick worried talkin'"), //5
            Resources.Load<Sprite>("sprites/Maverick determined talkin'"), //6
            Resources.Load<Sprite>("sprites/OM4"), //7
            Resources.Load<Sprite>("sprites/Maverick determined"), //8
            Resources.Load<Sprite>("sprites/Samira angry"), //9
            Resources.Load<Sprite>("sprites/Samira pissed-off"), //10
            Resources.Load<Sprite>("sprites/OM2"), //11
            Resources.Load<Sprite>("sprites/OM4"), //12
            Resources.Load<Sprite>("sprites/OM1"), //13
            Resources.Load<Sprite>("sprites/Maverick determined"), //14
            Resources.Load<Sprite>("sprites/Maverick angry"), //15
            Resources.Load<Sprite>("sprites/Maverick angry talkin'"), //16
            Resources.Load<Sprite>("sprites/OM1"), //17
            Resources.Load<Sprite>("sprites/OM4"), //18
            Resources.Load<Sprite>("sprites/OM5") //19
        };
        names = new string[]{
                "???",//1
                "???",//2
                "MAVERICK",//3
                "SAMIRA",//4
                "MAVERICK",//5
                "MAVERICK",//6
                "???",//7
                "MAVERICK",//8
                "SAMIRA",//9
                "SAMIRA",//10
                "???",//11
                "???",//12
                "???",//13
                "MAVERICK",//14
                "MAVERICK",//15
                "MAVERICK",//16
                "???",//17
                "???",//18
                "???"//19
            };
            colors = new Color[]{
                Color.red,//1
                Color.red,//2
                lightBlue,//3
                lightBlue,//4
                lightBlue,//5
                lightBlue,//6
                Color.red,//7
                lightBlue,//8
                lightBlue,//9
                lightBlue,//10
                Color.red,//11
                Color.red,//12
                Color.red,//13
                lightBlue,//14
                lightBlue,//15
                lightBlue,//16
                Color.red,//17
                Color.red,//18
                Color.red//19
            };

            StartDialogue();
        }

        if (dialoguePanel.activeInHierarchy &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.L)||Input.GetKey(KeyCode.C)))
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
        spawner.SetActive(true);
        if(GlobalVariables.waveCounter == 13)
        {
            GlobalVariables.enemiesAlive = 0;
            SceneManager.LoadScene("Boss3");
        }
    }
}
