namespace Vidly.DTOs
{
    public class CustomerFormViewModelDTO
    {
        public IEnumerable<MembershipTypeDto>? MembershipTypes { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
