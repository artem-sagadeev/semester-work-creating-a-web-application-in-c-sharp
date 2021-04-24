using System.ComponentModel.DataAnnotations;

namespace PaymentAPI
{
    public class StorageOfMoney
    {

        public int Sum { get; set; }
        [Key]
        public int Number { get; set; }
    }
}