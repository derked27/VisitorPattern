using System;
using System.Collections.Generic;
using System.Linq;

namespace Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            IShoppingCartVisitor shoppingCartVisitor = new ShoppingCartVisitor();

            List<ItemElement> items = new List<ItemElement>
            {
                new Book { Name = "The Very Hungry Caterpillar", Price = 8.99m },
                new Fruit { Name = "Banana", CostPerPound = 0.60m, Weight = 8 },
                new Book { Name = "The Cat in the Hat", Price = 12 }
            };

            decimal total = items.Sum(x => x.Accept(shoppingCartVisitor));
            Console.WriteLine("Total Cost: " + total);
        }
    }

    // Visitors
    // All of the code for handling each different item is handled by the visitor.
    // This means that if we were to add a new item, the only code change would be
    //  1. Create the new item type and have it implement ItemElement
    //  2. Handle the visit functionality for the new item type in the visitor
    public class ShoppingCartVisitor : IShoppingCartVisitor
    {
        public decimal Visit(Book book)
        {
            decimal cost = book.Price;
            if (book.Price > 50)
            {
                cost -= 5;
            }
            Console.WriteLine(book.Name + " cost =" + cost);
            return cost;
        }

        public decimal Visit(Fruit fruit)
        {
            decimal cost = fruit.CostPerPound * fruit.Weight;
            Console.WriteLine(fruit.Name + " cost =" + cost);
            return cost;
        }
    }

    public interface IShoppingCartVisitor
    {
        decimal Visit(Book book);
        decimal Visit(Fruit fruit);
    }

    // Item Elements
    // Each type of item should implement the ItemElement interface
    // The accept should take in the visitor and call Visit passing in itself.

    interface ItemElement
    {
        public decimal Accept(IShoppingCartVisitor visitor);
    }

    public class Book : ItemElement
    {
        public decimal Price { get; set; }
        public string Name { get; set; }

        public decimal Accept(IShoppingCartVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class Fruit : ItemElement
    {
        public decimal CostPerPound { get; set; }
        public int Weight { get; set; }
        public string Name { get; set; }

        public decimal Accept(IShoppingCartVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
