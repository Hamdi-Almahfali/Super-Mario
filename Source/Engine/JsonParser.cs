using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Super_Mario
{
    internal class JsonParser
    {
        static JObject wholeObj;
        static string currentFileName;
        public static void GetJObjectFromFile(string fileName)
        {
            currentFileName = fileName;
            StreamReader file = File.OpenText(fileName);
            JsonTextReader reader = new JsonTextReader(file);
            //string s=File.ReadAllText(fileName);
            wholeObj = JObject.Load(reader);
        }
        public static Rectangle GetRectangle(string fileName, string
        propertyName)
        {
            if (wholeObj == null || currentFileName == null ||
            currentFileName != fileName)
            {
                GetJObjectFromFile(fileName);
            }
            JObject obj = (JObject)wholeObj.GetValue(propertyName);
            return GetRectangle(obj);
        }
        public static List<PlatformData> GetType(string fileName, string
        propertyName)
        {
            if (wholeObj == null || currentFileName == null || currentFileName != fileName)
            {
                GetJObjectFromFile(fileName);
            }

            List<PlatformData> platformDataList = new List<PlatformData>();
            JArray arrayObj = (JArray)wholeObj.GetValue(propertyName);

            for (int i = 0; i < arrayObj.Count; i++)
            {
                JObject obj = (JObject)arrayObj[i];
                Rectangle rect = GetRectangle(obj);
                int type = GetType(obj);
                int prize = GetPrize(obj);

                PlatformData platformData = new PlatformData
                {
                    rect = rect,
                    type = type,
                    prize = prize
                };

                platformDataList.Add(platformData);
            }

            return platformDataList;
        }
        public static List<Rectangle> GetRectangleList(string fileName,
        string propertyName)
        {
            if (wholeObj == null || currentFileName == null ||
            currentFileName != fileName)
            {
                GetJObjectFromFile(fileName);
            }
            List<Rectangle> rectList = new List<Rectangle>();
            JArray arrayObj = (JArray)wholeObj.GetValue(propertyName);
            for (int i = 0; i < arrayObj.Count; i++)
            {
                JObject obj = (JObject)arrayObj[i];
                Rectangle rect = GetRectangle(obj);
                rectList.Add(rect);
            }
            return rectList;
        }
        private static Rectangle GetRectangle(JObject obj)
        {
            int x = Convert.ToInt32(obj.GetValue("positionX"));
            int y = Convert.ToInt32(obj.GetValue("positionY"));
            int height = Convert.ToInt32(obj.GetValue("height"));
            int width = Convert.ToInt32(obj.GetValue("width"));
            Rectangle rect = new Rectangle(x, y, width, height);
            return rect;
        }
        private static int GetType(JObject obj)
        {
            return obj.Value<int>("type");
        }
        private static int GetPrize(JObject obj)
        {
            return obj.Value<int>("prize");
        }
        public static void WriteJsonToFile(string filename,
        List<GameObject> gList)
        {
            JArray enemyArray = new JArray();
            JArray platformArray = new JArray();
            JObject bigobj = new JObject();
            JArray array = new JArray();
            for (int i = 0; i < gList.Count; i++)
            {
                if (gList[i] is Enemy)
                {
                    JObject obj = CreateObject(gList[i].GetBounds());
                    enemyArray.Add(obj);
                }
                else if (gList[i] is Platform)
                {
                    JObject obj = CreateObject(gList[i].GetBounds(), gList[i].GetPlatformType(), gList[i].GetPlatformPrize());
                    platformArray.Add(obj);
                }
                else if (gList[i] is Mario)
                {
                    JObject obj = CreateObject(gList[i].GetBounds());
                    bigobj.Add("player", obj);
                }
            }
            bigobj.Add("enemies", enemyArray);
            bigobj.Add("platforms", platformArray);
            System.Diagnostics.Debug.WriteLine(bigobj.ToString());
            File.WriteAllText(filename, bigobj.ToString());
        }
        private static JObject CreateObject(Rectangle rect)
        {
            JObject obj = new JObject();
            obj.Add("positionX", rect.X);
            obj.Add("positionY", rect.Y);
            obj.Add("height", rect.Height);
            obj.Add("width", rect.Width);
            return obj;
        }
        private static JObject CreateObject(Rectangle rect, int type, int prize)
        {
            JObject obj = new JObject();
            obj.Add("positionX", rect.X);
            obj.Add("positionY", rect.Y);
            obj.Add("height", rect.Height);
            obj.Add("width", rect.Width);
            obj.Add("type", type);
            obj.Add("prize", prize);
            return obj;
        }
    }
}
