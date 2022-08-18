using System.Xml.Serialization;

namespace API_A_HOME.Models.response
{

    [XmlRoot(ElementName = "response")]
    public class Response
    {

        [XmlElement(ElementName = "osmp_txn_id")]
        public int OsmpTxnId { get; set; }

        [XmlElement(ElementName = "result")]
        public int Result { get; set; }

        [XmlElement(ElementName = "comment")]
        public string Comment { get; set; }

    }
}