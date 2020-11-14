namespace SpaceScavengersSocial
{
    public interface ISaveGame {

        byte[] ObjectToBytes();
        ISaveGame BytesToObject(byte[] b);
    } 
}