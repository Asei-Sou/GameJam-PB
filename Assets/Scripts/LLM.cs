using UnityEngine;
using OpenAI_API;
using TMPro;

public class LLM : MonoBehaviour
{
    private async void Update()
    {
        // If player press enter and the input field is not empty
        if (!Input.GetKeyDown(KeyCode.Return) ||
            GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>().text == "") return;
        
        // take the API key from discord
        var api = new OpenAIAPI("");
        
        // Not sure which version its using
        var chat = api.Chat.CreateConversation();
        
        // var chat = api.Chat.CreateConversation();
        
        chat.AppendSystemMessage("One Two Three");
        // give a few examples as user and assistant
        chat.AppendUserInput("One Two"); 
        chat.AppendExampleChatbotOutput("Three"); // Not necessary, but helps the model learn
            
        // now let's ask it a question'
        // chat.AppendUserInput("One Two");
        chat.AppendUserInput(GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>().text);
        
        // get the response
        var response = await chat.GetResponseFromChatbotAsync();
        Debug.Log(response); // "Three"
        

    }
}
