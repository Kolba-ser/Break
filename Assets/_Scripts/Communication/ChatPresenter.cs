using Break.Communication;
using Break.Pool;
using Break.Utility.XML;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public sealed class ChatPresenter : MonoBehaviour
{
    [SerializeField] private Images dialogueImages;

    [Space(20)]
    [SerializeField] private Transform chatContent;
    [SerializeField] private Transform buttonContent;
    [SerializeField] private int maxMessagesOnScreen = 5;

    [Space(20)]
    [SerializeField] private ContentSide companionSide;
    [SerializeField] private ContentSide ourSide;

    [Inject] private PoolSystem poolSystem;

    private Dialogue dialogue = new Dialogue();
    private Queue<Message> messages;
    private Queue<AnswerButton> answers;

    private Node currentNode;
    private int currentNodeIndex;

    private void Start()
    {
        messages = new Queue<Message>();
        answers = new Queue<AnswerButton>();

        if (SerializerXML.Instance.TryDeserialize(ref dialogue, LevelNumber.LVL_1))
        {
            currentNode = dialogue.Nodes[currentNodeIndex];
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        RemoveAnswers();
        if (currentNode.ContainsMessage)
        {
            Observable.Timer(currentNode.DelayBeforeSending.InSec())
                .TakeUntilDisable(gameObject)
                .Subscribe(_ => SendToChat(currentNode.Message, companionSide, currentNode.spriteIndex, GiveAnswers));
        }
        else
        {
            GiveAnswers(currentNode);
        }
    }
    private void StopDialogue()
    {
    }

    private void GiveAnswers(Node node)
    {
        for (int i = 0; i < node.Answers.Length; i++)
        {
            if (poolSystem.TryGet(out AnswerButton answerButton))
            {
                Answer answer = node.Answers[i];

                answerButton.SetParameters(answer, SendAnswerMessage);
                answerButton.transform.SetParent(buttonContent);
                answers.Enqueue(answerButton);
            }
        }
    }
    private void RemoveAnswers()
    {
        int iterations = answers.Count;
        for (int i = 0; i < iterations; i++)
        {
            poolSystem.TryRemove<AnswerButton>(answers.Dequeue());
        }
    }

    private void SendAnswerMessage(string text, int toNode, bool isEnd, int spriteIndex)
    {
        SendToChat(text, ourSide, spriteIndex);
        SetNode(toNode);
        StartDialogue();

        if (isEnd)
        {
            StopDialogue();
        }
    }

    private void SendToChat(string text, ContentSide side, int spriteIndex, Action<Node> callback = null)
    {
        if (poolSystem.TryGet(out Message message))
        {
            message.transform.SetParent(chatContent);
            message.SetParameters(side, text, dialogueImages.Get(spriteIndex));
            messages.Enqueue(message);

            callback?.Invoke(currentNode);
        }
    }

    private void SetNode(int index)
    {
        currentNode = dialogue.Nodes[index];
        currentNodeIndex = index;
    }
}


