using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public static class Serializer
{
    public static byte[] Serialize<T>(T obj) {
        BinaryFormatter bf = new BinaryFormatter();
        using (var ms = new MemoryStream()) {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

    }
    public static T Deserialize<T>(byte[] data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            ms.Write(data, 0, data.Length);
            ms.Seek(0, SeekOrigin.Begin);
            var obj = bf.Deserialize(ms);

            if (obj is T objT) return objT;

            throw new InvalidCastException(
                $"cannot cast {obj.GetType().Name} to {typeof(T).Name}"
                );
        }
    }
}