namespace ParkingMaster.DTOs
{
   //generic DTO for tranfering
    public class TransferDTO<T>
    {
        // Automatic Properties
        public T Data { get; set; }
    }
}
