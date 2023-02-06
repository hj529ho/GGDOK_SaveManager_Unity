using System;
using UnityEngine;
using System.IO;


namespace GGDok.SaveManager
{
    public class SaveManager : MonoBehaviour
    {
        private Serializer _serializer = new Serializer();
        private string filePath;
        // Data data = new Data();
        private void Awake()
        {
            filePath =  $"C:/Users/{Environment.UserName}/{Application.productName}";
            var folder = Directory.CreateDirectory(filePath);
        }
        private void Start()
        {
            Save(new Data(){name = "gg111",age = 33});
            Data data = (Data)Load(typeof(Data));
            Debug.Log(data.name);
            Debug.Log(data.age);
        }
        public void Save(object obj)
        {
            FileStream test = new FileStream(filePath+"/"+$"{obj.GetType().Name}.ggdok",FileMode.Create);
            StreamWriter writer = new StreamWriter(test);
            writer.Write(_serializer.Serialize(obj));
            writer.Close();
        }
        public object Load(Type type)
        {
            string path = filePath + "/" + $"{type.Name}.ggdok";
            if (File.Exists(path) == false)
            {
                return null;
            }
            string text = File.ReadAllText(path);
            return _serializer.DeSerialize(text);
        }
    }
}