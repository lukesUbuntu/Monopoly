using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MolopolyGame
{
    class testxml
    {
        public void save()
        {
            // Create the product catalog.
            //http://stackoverflow.com/questions/3187444/convert-xml-string-to-object

            //http://tech.pro/tutorial/743/introduction-to-linq-simple-xml-parsing
            //http://tech.pro/tutorial/798/csharp-tutorial-xml-serialization
            Settingsxml objCategory = new Settingsxml();
            luckFactory[] lstProducts = new luckFactory[3];
            //"Go", false, 200
            lstProducts[0] = new luckFactory("Go", false, 2, 20.39m);
            lstProducts[1] = new luckFactory("Community Chest", true, 1, 2.90m);
            lstProducts[2] = new luckFactory("Change Chest", true, 3, 50.70m);

            objCategory.luckFactorys = lstProducts;
            XmlSerializer objXMLSerializer = new XmlSerializer(typeof(Settings));
            FileStream objFS = new FileStream("ProductDetails.xml", FileMode.Create);
            objXMLSerializer.Serialize(objFS, objCategory);
            objFS.Close();
        }
    }

    [XmlRoot("Settings")]
        public class Settingsxml
        {




            [XmlArray("luckFactorys")]
            [XmlArrayItem("luckFactory")]
            public luckFactory[] luckFactorys;
            public Settingsxml()
            {

            }
           
        }
        public class luckFactory
        {

            [XmlElement(ElementName = "isBenefitNotPenalty", DataType = "boolean")]
            public bool isBenefitNotPenalty;

            [XmlElement("productWeight")]
            public decimal ProductWeight;
            [XmlElement("productPrice")]
            public decimal ProductPrice;

            [XmlAttributeAttribute(AttributeName = "name")]
            public string name;
            public luckFactory()
            {
                // Default constructor for serialization.
            }
            //"Go", false, 200
            //string sName, bool isBenefitNotPenalty, decimal amount, IEnum.Game luckType = IEnum.Game.None
            public luckFactory(string sName, bool isBenefitNotPenalty, decimal productWeight, decimal productPrice)
            {
                this.name = sName;
                this.isBenefitNotPenalty = isBenefitNotPenalty;
                this.ProductWeight = productWeight;
                this.ProductPrice = productPrice;

            }
        }
       
}
