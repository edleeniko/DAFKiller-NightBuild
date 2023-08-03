using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller.Core
{
    /// <summary>
    /// Сохранение и чтение объектов в/из файла используя бинарную сериализацию
    /// </summary>
    public class SaverLoader
    { /// <summary>
      /// Сохранение объекта в файл
      /// </summary>
        public static void Save<T>(T obj, string filePath)
        {
            using (var fs = File.OpenWrite(filePath))
                new BinaryFormatter().Serialize(fs, obj);
        }

        /// <summary>
        /// Чтение объекта из файла
        /// </summary>
        public static T Load<T>(string filePath)
        {
            using (var fs = File.OpenRead(filePath))
                return (T)new BinaryFormatter().Deserialize(fs);
        }
    }
}
