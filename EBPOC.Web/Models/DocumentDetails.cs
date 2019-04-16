using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBPOC.Web.Models
{
    public class DocumentDetails
    {
        private string _name = "N/A";
        private string _type = "N/A";
        private string _image = "N/A";
        private string _source = "N/A";
        private string _mediaUrl = "N/A";
        //private int age = 0;

        // Declare a Name property of type string:
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        // Declare a Name property of type string:
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        // Declare a Name property of type string:
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }
        // Declare a Name property of type string:
        public string Scource
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        public string MediaUrl
        {
            get {
                return _mediaUrl;
            }

            set {

                _mediaUrl = value;

            }

        }

        //// Declare an Age property of type int:
        //public int Age
        //{
        //    get
        //    {
        //        return age;
        //    }

        //    set
        //    {
        //        age = value;
        //    }
        //}

        //public override string ToString()
        //{
        //    return "Name = " + Name + ", Age = " + Age;
        //}
    }
}