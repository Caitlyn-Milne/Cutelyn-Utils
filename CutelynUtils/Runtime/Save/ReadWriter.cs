using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class ReadWriter : MonoBehaviour
{
    public virtual void Write(string fileName, string fileExtension, byte[] content)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName + fileExtension, FileMode.Create)))
        { 
            foreach (byte b in content) writer.Write(b);
        }
    }
    public virtual byte[] Read(string fileName, string fileExtension)
    {
        byte[] contents;
        contents = SaveExtensions.ReadAllBytes(new BinaryReader(File.Open(fileName + fileExtension, FileMode.Open)));
        return contents;
    }
}
public static class SaveExtensions
{
    public static byte[] ReadAllBytes(this BinaryReader reader)
    {
        const int bufferSize = 4096;
        using (var ms = new MemoryStream())
        {
            byte[] buffer = new byte[bufferSize];
            int count;
            while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                ms.Write(buffer, 0, count);
            return ms.ToArray();
        }
    }
}
