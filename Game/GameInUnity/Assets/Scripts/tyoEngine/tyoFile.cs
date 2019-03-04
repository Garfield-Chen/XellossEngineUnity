using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyoFile
{
    TextAsset textAsset = null;
    bool textAssetFlag = false;
    public bool isLoaded = false;
    public tyoFile(bool _isTextAsset)
    {
        textAssetFlag = _isTextAsset;
    }

    public bool LoadFile(string _file)
    {
        if ( isLoaded )
        {
            return false;
        }

        if ( textAssetFlag )
        {
            textAsset = Resources.Load<TextAsset>(_file);

            if ( textAsset.name.Length > 0 )
            {
                isLoaded = true;
                return true;
            }
        }       

        return false;
    }

    public string GetText()
    {
        if ( isLoaded )
        {
            return textAsset.text;
        }

        return "";
    }

    public byte[] GetBytes()
    {
        if ( isLoaded )
        {
            return textAsset.bytes;
        }

        return null;
    }
}