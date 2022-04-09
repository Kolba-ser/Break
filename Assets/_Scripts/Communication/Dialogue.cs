using System;
using System.Xml.Serialization;
using UnityEngine;

namespace Break.Communication
{
    [Serializable]
    [XmlRoot("Dialogue")]
    public sealed class Dialogue
    {
        [XmlElement("node")]
        public Node[] Nodes;
        
        public bool IsFull()
        {
            bool isFull = false;
            foreach (var node in Nodes)
            {
                isFull |= node.ContainsAnswers | node.ContainsMessage;
            }

            return isFull;
        }

    }

    [Serializable]
    public sealed class Node
    {
        [XmlElement("message")]
        public string Message;

        [XmlElement("spriteIndex")]
        public int spriteIndex = -1;

        [XmlElement("sendingDelay")]
        public float DelayBeforeSending = 1f;

        [XmlArray("answers")]
        [XmlArrayItem("answer")]
        public Answer[] Answers;

        public bool ContainsMessage => Message.Length > 0;
        public bool ContainsImage => spriteIndex > 0;
        public bool ContainsAnswers => Answers.Length > 0;
    }
    
    [Serializable]
    public class Answer
    {
        [SerializeField] private Sprite sprite;

        [XmlAttribute("toNode")]
        public int NextNode;

        [XmlElement("text")]
        public string Text;

        [XmlElement("spriteIndex")]
        public int spriteIndex = -1;

        [XmlElement("isEnd")]
        public bool IsEnd;

        public bool ContainsMessage => Text.Length > 0;
        public bool ContainsImage => spriteIndex > 0;
    }

}
