using System;

namespace OxigenIIPresentation
{
    public class UploadForm
    {
        private readonly string _inviteToOverrideFieldText;
        private string _creator;
        private DateTime? _date;
        private string _description;
        private float _displayDuration;
        private string _title;
        private string _url;
        private bool _userHasProvidedDate;
        private bool _userHasProvidedTitle;
        private bool _userHasProvidedDisplayDuration;

        public UploadForm(string inviteToOverrideFieldText)
        {
            _inviteToOverrideFieldText = inviteToOverrideFieldText;
        }

        public string Title
        {
            get { return _title; }
            set 
            {
                if (UserHasProvidedTextFieldValue(value))
                {
                    _title = value;
                    _userHasProvidedTitle = true;
                }
                else
                {
                    _title = null;
                }
            }
        }

        public string Creator
        {
            get { return _creator; }
            set { _creator = GetFieldIfProvidedOrNull(value); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = GetFieldIfProvidedOrNull(value); }
        }

        public DateTime? Date
        {
            get { return _date; }
        }

        public void SetDateIfNotEmpty(string date)
        {
            if (!UserHasProvidedTextFieldValue(date))
                 _date = null;

            DateTime dt;

            if (!DateTime.TryParse(date, out dt))
                _date = null;
            else
            {
                _date = dt;
                _userHasProvidedDate = true;
            }
                
        }

        public string Url
        {
            get { return _url; }
            set { _url = GetFieldIfProvidedOrNull(value); }
        }

        public float DisplayDuration
        {
            get { return _displayDuration; }
        }

        private string GetFieldIfProvidedOrNull(string textFieldValue)
        {
            if (UserHasProvidedTextFieldValue(textFieldValue))
                return textFieldValue;

            return null;
        }

        private bool UserHasProvidedTextFieldValue(string textFieldValue)
        {
            return !string.IsNullOrEmpty(textFieldValue) && textFieldValue != _inviteToOverrideFieldText;
        }

        public void SetDisplayDuration(string displayDuration, int minDisplayDuration, int maxDisplayDuration)
        {
            if (!UserHasProvidedTextFieldValue(displayDuration))
            {
                _displayDuration = -1;

                return;
            }

            if (!float.TryParse(displayDuration, out _displayDuration) || _displayDuration < minDisplayDuration || _displayDuration > maxDisplayDuration)
                _displayDuration = -1;

            _userHasProvidedDisplayDuration = true;
        }

        public bool UserHasProvidedDate
        {
            get { return _userHasProvidedDate; }
        }

        public bool UserHasProvidedTitle
        {
            get { return _userHasProvidedTitle; }
        }

        public bool UserHasProvidedDisplayDuration
        {
            get { return _userHasProvidedDisplayDuration; }
        }
    }
}