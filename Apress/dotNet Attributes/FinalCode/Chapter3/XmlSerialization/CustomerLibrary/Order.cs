using System;
using System.Xml.Serialization;

namespace CustomerLibrary
{
   [XmlRoot("Test")]
   public class Order
   {
      [XmlAttribute()]
      public int Id;
      public double Total;
      public DateTime Date;

      public Order() {}
      public Order(int id, double total)
      {
         Id = id;
         Total = total;
         Date = DateTime.Now.Date;
      }
   }

   public class OrderHistory
   {  
      [XmlArray(), XmlArrayItem("OrderItem")]
      public Order[] Orders;
     
      public double ComputeTotal()
      {
         double total = 0;
         if (Orders != null)
         {
            foreach(Order o in Orders)
               total += o.Total;
         }
         return total;
      }

      public OrderHistory() {}

      public OrderHistory(int i)
      {
         Orders = new Order[3];
         Orders[0] = new Order(1, 100);
         Orders[1] = new Order(2, 200);
         Orders[2] = new Order(3, 300);
      }
   }
}
