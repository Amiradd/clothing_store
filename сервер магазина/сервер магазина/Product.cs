using System;
using System.Collections.Generic;
using System.Text;

namespace сервер_магазина
{
    class Product
    {
        public string title;
        public int cost;

        public Product(string title, int cost)
        {
            this.title = title;
            this.cost = cost;
        }
    }
}
