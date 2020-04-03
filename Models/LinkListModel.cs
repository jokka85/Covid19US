using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Covid19Texas.Models
{
    /// <summary>
    /// Full Link List Model
    /// </summary>
    public class LinkListModel
    {
        public StateModel State { get; set; }
    }

    /// <summary>
    /// List By State
    /// </summary>
    public class StateModel
    {
        public StatePropertyModel Texas { get; set; }
    }

    public class StatePropertyModel
    {
        public string Link { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Type
        {
            [EnumMember(Value = "Excel")]
            Excel,

            [EnumMember(Value = "Other")]
            Other
        }
    }
}
