using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Common.Core.FileAccess
{
    public static class FileProcessor
    {
        public static int[][] GetMatrixFromFile(string fileName, string matrixName)
        {
            var jsonTestDataFileName = fileName;
            var absolutePathForDocs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory
                + jsonTestDataFileName);

            var json = File.ReadAllText(absolutePathForDocs);
            var jObject = JObject.Parse(json);
            var values = jObject[matrixName]?.ToObject<int[][]>();

            return values != null ? values : throw new ArgumentNullException();
        }
    }
}