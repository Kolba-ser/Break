
using Break.Communication;
using UnityEditor;
using UnityEngine;

namespace Break.Utility.XML
{

    public sealed class UnityXmlHelper : MonoBehaviour
    {
        [Space(20)]
        [SerializeField] private LevelNumber levelNumber;
        [SerializeField] private Dialogue dialogue;


        public void SetDeserialized(Dialogue dialogue)
        {
            if (dialogue.IsFull())
            {
                this.dialogue = dialogue;
            }
        }

        public Dialogue Dialogue => dialogue;
        public LevelNumber LevelNumber => levelNumber;
    }

    [CustomEditor(typeof(UnityXmlHelper))]
    public sealed class UnityXmlHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            UnityXmlHelper xmlHelper = (UnityXmlHelper)target;
            if (GUILayout.Button("Serialize"))
            {
                SerializerXML.Instance.TrySerialize(xmlHelper.LevelNumber, xmlHelper.Dialogue);
            }
            if (GUILayout.Button("Deserialize"))
            {
                var dialogue = new Dialogue();
                SerializerXML.Instance.TryDeserialize(ref dialogue, xmlHelper.LevelNumber);
                xmlHelper.SetDeserialized(dialogue);
            }

            base.OnInspectorGUI();
        }
    }
}
