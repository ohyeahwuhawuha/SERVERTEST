using System.IO;

public class NetMessage
{
    public const ushort NM_INVALID = ushort.MinValue;    //0;

    //==================1024之前保留用作系统消息=================//
    public const ushort REQ_CONNECTION = 1;
    public const ushort RLT_CONNECTION = 2;
    //==================1024之前保留用作系统消息===============//

    //=========================================================//
    public const ushort LOGIN = 1025;  //登入请求.
    public const ushort EnterLevel = 1026;  //登入请求.
    public const ushort Move = 1027;  //登入请求.    
    //=========================================================//

    public const ushort BC_EnterLevel = 59999;  //登入请求.
    public const ushort BC_Move = 60000;  //登入结果.
    public const ushort BC_Logout = 50000;
    //=========================================================//

    public const ushort NM_MAX = ushort.MaxValue; //65535;
}

public class NetResult
{
    public const int RLT_SUCCESS = 0;
}