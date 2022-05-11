using System.Collections;
using UnityEngine;


/// <summary>
/// Adds a number to every byte to make it less human readible, it DOES NOT make the file secure, but does discourage tampering with the file
/// </summary>
public class SimpleEncyprtReadWriter : ReadWriter
{
    public override byte[] Read(string fileName, string fileExtension)
    {
        var data = base.Read(fileName, fileExtension);

        for (int i = 0; i < data.Length; i++)
            data[i] += 87;

        return data;
    }

    public override void Write(string fileName, string fileExtension, byte[] content)
    {
        for (int i = 0; i < content.Length; i++)
            content[i] -= 87;

        base.Write(fileName, fileExtension, content);
    }
}
