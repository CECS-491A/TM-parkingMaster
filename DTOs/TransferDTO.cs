namespace ParkingMaster.DTOs
{
   //generic DTO for tranfering
    public class TransferDTO<T>
    {
        // Default Properties
        public T Data { get; set; }
    }
}
