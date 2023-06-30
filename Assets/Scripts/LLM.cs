using UnityEngine;
using OpenAI_API;
using TMPro;

public class LLM : MonoBehaviour
{
    public GameObject inputField;
    public TextMeshProUGUI output;

    private string testinput = "I want you to imitate a knight named Sir Geofrrey." +
        "                       You are in a Medieval environment. You are a knight from the little kingdom of Littletown." +
        "                       Nothing ever happens here and you would love to actually go on adventures. I will try to convince you to join me." +
        "                       You will only join me if you are convinced that I am a great fighter." +
        "                       I have no power over you, have no ransom and the only thing that I know is that you're a knight in this little kingdom." +
        "                       Start a conversation with me. I can interact with you 5 times." +
        "                       Afterwards end the conversation with \"CONVERSATION END☆\" and provide a score on how convinced you are to join me." +
        "                       End the conversation earlier if I am aggressive or annoy you in any way. Keep your answers very short." +
        "                       If you are persuaded, please say \"OK, I will join you.★";

    private bool isFirstPrompt = true;
    private int roundCounter = 0;

    private string previousOutput = "";

    async void Update()
    {
        if (roundCounter < 5)
        {

            // If player press enter and the input field is not empty
            if (!Input.GetKeyDown(KeyCode.Return) ||
                inputField.GetComponent<TMP_InputField>().text == "") return;

            // take the API key from discord
            var api = new OpenAIAPI("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");

            // Not sure which version its using
            var chat = api.Chat.CreateConversation();

            // var chat = api.Chat.CreateConversation();

            //chat.AppendSystemMessage("One Two Three");
            // give a few examples as user and assistant
            //chat.AppendUserInput("One Two"); 
            //chat.AppendExampleChatbotOutput("Three"); // Not necessary, but helps the model learn

            chat.AppendUserInput(testinput);

            if (roundCounter < 5)
            {
                if (!isFirstPrompt)
                {
                    chat.AppendExampleChatbotOutput(previousOutput);
                }
            }

            else
            {
                chat.AppendUserInput("Please end the conversation with \\\"CONVERSATION END☆\\\" and provide a score on how convinced you are to join me. If you are persuaded, please say \"OK, I will join you.★\"");
            }


            // now let's ask it a question'
            // chat.AppendUserInput("One Two");
            chat.AppendUserInput(inputField.GetComponent<TMP_InputField>().text);

            // get the response
            var response = await chat.GetResponseFromChatbotAsync();
            Debug.Log(response); // "Three"
            output.text = "\"" + response + "\"";

            previousOutput = response;

            if (isFirstPrompt)
            {
                isFirstPrompt = false;
            }

            roundCounter++;
        }
    }

    }
