using Break.Communication;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UnityEngine;

namespace Break.Utility.XML
{

    public enum LevelNumber
    {
        LVL_1,
        LVL_2,
    }

    public class SerializerXML : Singletone<SerializerXML>
    {
        private const string PATH = @".\Assets\Resourses\";

        /// <summary>
        /// Filename
        /// </summary>
        private readonly (string, Type) DIALOGUE_LVL1 = ("Dialogue_lvl1.xml", typeof(Dialogue));
        private readonly (string, Type) DIALOGUE_LVL2 = ("Dialogue_lvl2.xml", typeof(Dialogue));

        public bool TrySerialize<T>(LevelNumber fileType, T serializable)
        {
            if (IsSerializable(serializable) && TryGetFile(out (string, Type) file, fileType))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(file.Item2);
                using (FileStream fileStream = new FileStream(PATH + file.Item1, FileMode.Create))
                {
                    xmlSerializer.Serialize(fileStream, serializable);
                }
                return true;
            }

            return false;
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializable"></param>
        /// <param name="fileType"></param>
        /// <returns>Deserialized data type T</returns>
        public bool TryDeserialize<T>(ref T serializable, LevelNumber fileType) 
        {
            if (IsSerializable(serializable) && TryGetFile(out (string, Type) file, fileType))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(file.Item2);
                using (FileStream fileStream = new FileStream(PATH + file.Item1, FileMode.OpenOrCreate))
                {
                    serializable = (T)xmlSerializer.Deserialize(fileStream);
                }
                return true;
            }

            return false;
        }

        private bool IsSerializable<T>(T serializable)
        {
            if (serializable != null && serializable.GetType().IsSerializable)
            {
                return true;
            }

            return false;
        }

        private bool TryGetFile(out (string, Type) file, LevelNumber fileType)
        {
            file = (null, null);

            switch (fileType)
            {
                case LevelNumber.LVL_1:
                    file.Item1 = DIALOGUE_LVL1.Item1;
                    file.Item2 = DIALOGUE_LVL1.Item2;
                    return true;
                case LevelNumber.LVL_2:
                    file.Item1 = DIALOGUE_LVL2.Item1;
                    file.Item2 = DIALOGUE_LVL2.Item2;
                    return true;
            }

            return false;
        }
    }
}
