/**
 * Autogenerated by Thrift
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Transport;
namespace Apache.Cassandra
{

  [Serializable]
  public partial class AuthenticationException : Exception, TBase
  {
    private string why;

    public string Why
    {
      get
      {
        return why;
      }
      set
      {
        __isset.why = true;
        this.why = value;
      }
    }


    public Isset __isset;
    [Serializable]
    public struct Isset {
      public bool why;
    }

    public AuthenticationException() {
    }

    public void Read (TProtocol iprot)
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.String) {
              this.why = iprot.ReadString();
              this.__isset.why = true;
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
    }

    public void Write(TProtocol oprot) {
      TStruct struc = new TStruct("AuthenticationException");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (this.why != null && __isset.why) {
        field.Name = "why";
        field.Type = TType.String;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(this.why);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("AuthenticationException(");
      sb.Append("why: ");
      sb.Append(this.why);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
