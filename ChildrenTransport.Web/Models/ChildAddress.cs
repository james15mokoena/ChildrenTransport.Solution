namespace ChildrenTransport.Web.Models
{
    public class ChildAddress(Child _child, Address _address)
    {
        private readonly Child child = _child;
        private readonly Address address = _address;

        public Child GetChild() { return child; }
        public Address GetAddress() { return address; }
    }
}
