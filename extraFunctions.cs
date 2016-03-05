  /*
  Here are a few extra functions that will come in very handy when sending data over the network!
  */
  
    public byte[] SerializeObject<_T>(_T objectToSerialize)
    {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream memStr = new MemoryStream();

        bf.Serialize(memStr, objectToSerialize);
        memStr.Position = 0;
        return memStr.ToArray();
    }

    public static _T DeserializeObject<_T>(byte[] dataStream)
    {
        MemoryStream memStr = new MemoryStream(dataStream);
        memStr.Position = 0;
        BinaryFormatter bf = new BinaryFormatter();
        //bf.Binder = new VersionFixer();
        return (_T)bf.Deserialize(memStr);
    }
