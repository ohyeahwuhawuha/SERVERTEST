//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: MessageType.proto
namespace Protocl
{
    [global::ProtoBuf.ProtoContract(Name=@"EMessage")]
    public enum EMessage
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"LOGIN_SERVER", Value=1024)]
      LOGIN_SERVER = 1024,
            
      [global::ProtoBuf.ProtoEnum(Name=@"C2L_LOGIN", Value=1025)]
      C2L_LOGIN = 1025,
            
      [global::ProtoBuf.ProtoEnum(Name=@"L2C_LOGIN", Value=1026)]
      L2C_LOGIN = 1026,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_SERVER", Value=10000)]
      GAME_SERVER = 10000,
            
      [global::ProtoBuf.ProtoEnum(Name=@"C2S_MATCH", Value=10001)]
      C2S_MATCH = 10001,
            
      [global::ProtoBuf.ProtoEnum(Name=@"S2C_MATCH", Value=10002)]
      S2C_MATCH = 10002,
            
      [global::ProtoBuf.ProtoEnum(Name=@"C2S_MATCH_CANCEL", Value=10003)]
      C2S_MATCH_CANCEL = 10003,
            
      [global::ProtoBuf.ProtoEnum(Name=@"S2C_MATCH_CANCEL", Value=10004)]
      S2C_MATCH_CANCEL = 10004,
            
      [global::ProtoBuf.ProtoEnum(Name=@"S2C_MATCH_SUCCESS", Value=10006)]
      S2C_MATCH_SUCCESS = 10006,
            
      [global::ProtoBuf.ProtoEnum(Name=@"C2S_MATCH_READY", Value=10007)]
      C2S_MATCH_READY = 10007,
            
      [global::ProtoBuf.ProtoEnum(Name=@"S2C_MATCH_READY", Value=10008)]
      S2C_MATCH_READY = 10008,
            
      [global::ProtoBuf.ProtoEnum(Name=@"S2C_MATCH_ALL_READY", Value=10010)]
      S2C_MATCH_ALL_READY = 10010,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BATTLE_SERVER", Value=20000)]
      BATTLE_SERVER = 20000,
            
      [global::ProtoBuf.ProtoEnum(Name=@"C2B_COMMAND", Value=20001)]
      C2B_COMMAND = 20001,
            
      [global::ProtoBuf.ProtoEnum(Name=@"B2C_COMMAND", Value=20002)]
      B2C_COMMAND = 20002,
            
      [global::ProtoBuf.ProtoEnum(Name=@"C2B_LOAD_PROGRESS", Value=20003)]
      C2B_LOAD_PROGRESS = 20003,
            
      [global::ProtoBuf.ProtoEnum(Name=@"B2C_LOAD_PROGRESS", Value=20004)]
      B2C_LOAD_PROGRESS = 20004,
            
      [global::ProtoBuf.ProtoEnum(Name=@"B2C_ALL_LOADED", Value=20006)]
      B2C_ALL_LOADED = 20006,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MAX", Value=65535)]
      MAX = 65535
    }
  
}