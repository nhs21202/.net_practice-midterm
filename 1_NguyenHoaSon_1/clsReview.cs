using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_NguyenHoaSon_1
{
    public class clsReview
    {
        public int reviewID = 0;
        public int productID = 0;
        public string customerName = "";
        public string customerEmail = "";
        public int rating = 0;
        public string comments = "";

        public clsReview() { }
        public clsReview(int reviewID, int productID, string customerName, string customerEmail, string comments, int rating)
        {
            this.rating = rating;
            this.comments = comments;
            this.reviewID = reviewID;
            this.productID = productID;
            this.customerName = customerName;
            this.customerEmail = customerEmail;

        }
        public override string ToString()
        {
            return "review ID: " + this.reviewID +"/n product ID: "+this.productID + "/n customer's name: " + this.customerName +
                "/ncustomer's email: " + this.customerEmail + "/nrating: " + this.rating +"/ncomments: "+this.comments;
        }

    }
}
