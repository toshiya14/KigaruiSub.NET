using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMEGo.Kigaruisub.Exceptions
{
    class BadFormatException : Exception
    {
        private int _index;
        private string _message;
        private string _sourceLine;
        public BadFormatException(int index,string sourceLine, string message)
        {
            this._index = index;
            this._message = message;
            this._sourceLine = sourceLine;
        }
        public override string Message
        {
            get
            {
                return _message;
            }
        }
        public int Index
        {
            get
            {
                return _index;
            }
        }

        public string SourceLine
        {
            get
            {
                return _sourceLine;
            }
        }
    }
}
