using System;
using System.Collections.Generic;
using System.Text;

namespace SDMX_ML.Framework.Structure
{
    public class TextFormatType
    {
        private string _texttype;

        public string Texttype
        {
            get { return _texttype; }
            set { _texttype = value; }
        }
        private bool _issequence;

        public bool IsSequence
        {
            get { return _issequence; }
            set { _issequence = value; }
        }
        private int _minlength;

        public int Minlength
        {
            get { return _minlength; }
            set { _minlength = value; }
        }
        private int _maxlength;

        public int Maxlength
        {
            get { return _maxlength; }
            set { _maxlength = value; }
        }
        private double _startvalue;

        public double Startvalue
        {
            get { return _startvalue; }
            set { _startvalue = value; }
        }
        private double _endvalue;

        public double Endvalue
        {
            get { return _endvalue; }
            set { _endvalue = value; }
        }
        private double _interval;

        public double Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }
        private double _timeinterval;

        public double Timeinterval
        {
            get { return _timeinterval; }
            set { _timeinterval = value; }
        }
        private int _decimals;

        public int Decimals
        {
            get { return _decimals; }
            set { _decimals = value; }
        }
        private string _pattern;

        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

    }
}
