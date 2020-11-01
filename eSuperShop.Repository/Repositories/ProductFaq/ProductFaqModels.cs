using System;

namespace eSuperShop.Repository
{
    public class ProductFaqAddModel
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Question { get; set; }
    }
    public class ProductFaqAnswerModel
    {
        public int ProductFaqId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerUserName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsVisible { get; set; }
        public DateTime AnswerOnUtc { get; set; }
    }
    public class FaqProductWiseViewModel
    {
        public int ProductFaqId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerUserName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime QuestionOnUtc { get; set; }
        public DateTime AnswerOnUtc { get; set; }
    }
    public class FaqCustomerWiseViewModel
    {
        public int ProductFaqId { get; set; }
        public ProductShortInfo Product { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime QuestionOnUtc { get; set; }
        public DateTime AnswerOnUtc { get; set; }
    }

    public class FaqVendorWiseViewModel
    {
        public int ProductFaqId { get; set; }
        public ProductShortInfo Product { get; set; }
        public int CustomerId { get; set; }
        public string CustomerUserName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsVisible { get; set; }
        public DateTime QuestionOnUtc { get; set; }
        public DateTime AnswerOnUtc { get; set; }
    }

}