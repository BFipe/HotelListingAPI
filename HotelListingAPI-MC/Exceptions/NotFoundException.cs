namespace HotelListingAPI_MC.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() : base("Object not found") { }

        public NotFoundException(string name, object key) : base($"{name} (key {key}) not found") { }
    }

    public class AlreadyExistsException : ApplicationException
    {
        public AlreadyExistsException(string name, object key) : base($"{name} ({key}) already exists") { }
    }
}
