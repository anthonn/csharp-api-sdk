using FortnoxAPILibrary.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FortnoxAPILibrary.Entities
{
    public enum PersonelType
    {
        ///<summary> Salaried employee </summary>
        [EnumMember(Value = "TJM")]
        TJM,
        ///<summary> Worker </summary>
        [EnumMember(Value = "ARB")]
        ARB,
    }
}