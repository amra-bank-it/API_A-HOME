using System.Xml.Serialization;

namespace API_A_HOME.Models.response
{
	[XmlRoot(ElementName = "response")]
	public class ResponsePay
	{
		[XmlElement(ElementName = "osmp_txn_id")]
		public string Osmp_txn_id { get; set; }

		[XmlElement(ElementName = "prv_txn")]
		public string Prv_txn { get; set; }

		[XmlElement(ElementName = "sum")]
		public string Sum { get; set; }

		[XmlElement(ElementName = "result")]
		public int Result { get; set; }

		[XmlElement(ElementName = "comment")]
		public string Comment { get; set; }
	}
}