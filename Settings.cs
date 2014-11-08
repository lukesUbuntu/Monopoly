using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MonopolyGame_9901623
{
   public class Settings
    {
        public void save()
        {
            // Create the product catalog.
            //http://stackoverflow.com/questions/3187444/convert-xml-string-to-object

            //http://tech.pro/tutorial/743/introduction-to-linq-simple-xml-parsing
            //http://tech.pro/tutorial/798/csharp-tutorial-xml-serialization
            Settingsxml objCategory = new Settingsxml();
            PropertyType[] lstProducts = new PropertyType[3];
            String[] theArray = {"1","2"};
            //"Go", false, 200
            lstProducts[0] = new PropertyType("LuckFactory", false, 2, 20.39m, theArray);
            lstProducts[1] = new PropertyType("ResidentialFactory", true, 1, 2.90m, theArray);
            lstProducts[2] = new PropertyType("Change Chest", true, 3, 50.70m, theArray);

            objCategory.Properties = lstProducts;
            XmlSerializer objXMLSerializer = new XmlSerializer(typeof(Settingsxml));
            FileStream objFS = new FileStream("ProductDetails.xml", FileMode.Create);
            objXMLSerializer.Serialize(objFS, objCategory);
            objFS.Close();
        }
    }

    [XmlRoot("Settings")]
    public class Settingsxml
    {




        [XmlArray("Properties")]
        [XmlArrayItem("PropertyType")]
        public PropertyType[] Properties;
        public Settingsxml()
        {

        }

    }
    public class PropertyType
    {

        [XmlElement(ElementName = "isBenefitNotPenalty", DataType = "boolean")]
        public bool isBenefitNotPenalty;

        [XmlElement("productWeight")]
        public decimal ProductWeight;
        [XmlElement("productPrice")]
        public decimal ProductPrice;

        [XmlArray(ElementName = "args")]
        public String[] args;


        [XmlAttributeAttribute(AttributeName = "type")]
        public string type;
        public PropertyType()
        {
            // Default constructor for serialization.
        }
        //"Go", false, 200
        //string sName, bool isBenefitNotPenalty, decimal amount, Game.CardType luckType = Game.CardType.None
        public PropertyType(string sName, bool isBenefitNotPenalty, decimal productWeight, decimal productPrice,String[] args)
        {
            this.type = sName;
            this.isBenefitNotPenalty = isBenefitNotPenalty;
            this.ProductWeight = productWeight;
            this.ProductPrice = productPrice;
            this.args = args;
        }
    }

}
