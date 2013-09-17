using System;

namespace SmartElk.ElkMate.Common
{
    [Serializable]
    public class Email : IContact
    {
        private string _address;
        private string _alias = string.Empty;

        public Email(string address, string alias = "")
        {
            _address = address;
            _alias = alias;
        }

        public Email(string address)
        {
            _address = address;
            _alias = "";
        }

        public IContact WithAlias(string newAlias)
        {
            return new Email(_address, newAlias);
        }

        public virtual string Address
        {
            get { return _address; }
            protected set { _address = value; }
        }

        public virtual string Alias
        {
            get { return _alias; }
            protected set { _alias = value; }
        }

        public virtual string LocalName
        {
            get { return Address.Substring(0, Address.IndexOf('@')); }
        }
    }
}