//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Match.proto
// Note: requires additional types generated from: Player.proto
namespace Protocl
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"C2S_Match")]
  public partial class C2S_Match : global::ProtoBuf.IExtensible
  {
    public C2S_Match() {}
    
    private string _name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"S2C_MatchSuccess")]
  public partial class S2C_MatchSuccess : global::ProtoBuf.IExtensible
  {
    public S2C_MatchSuccess() {}
    
    private readonly global::System.Collections.Generic.List<PlayerBase> _players = new global::System.Collections.Generic.List<PlayerBase>();
    [global::ProtoBuf.ProtoMember(1, Name=@"players", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<PlayerBase> players
    {
      get { return _players; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}