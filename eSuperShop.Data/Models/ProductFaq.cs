using System;

namespace eSuperShop.Data
{
    public class ProductFaq
    {
        public int ProductFaqId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsVisible { get; set; }
        public DateTime QuestionOnUtc { get; set; }
        public DateTime? AnswerOnUtc { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}