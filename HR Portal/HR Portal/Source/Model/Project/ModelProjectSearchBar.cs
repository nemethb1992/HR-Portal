using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Project
{
    public class ModelProjectSearchBar
    {
        public string projektnev { get; set; }
        public string jeloltszam { get; set; }
        public string publikalva { get; set; }
        public string interjuk { get; set; }
        public string pc { get; set; }
        public string nyelvkStr { get; set; }
        public int nyelvIndex { get; set; }
        public string vegzettsegStr { get; set; }
        public int vegzettsegIndex { get; set; }
        public string cimke { get; set; }
        public string jeloltnev { get; set; }
        public string publikalt { get; set; }
        public bool publikaltBool { get; set; }
        public string HeaderSelected { get; set; }
        public string sorrend { get; set; }
        
    }
}
