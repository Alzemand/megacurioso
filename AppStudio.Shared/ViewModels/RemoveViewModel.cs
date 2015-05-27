using AppStudio.Data;
using System;
using Windows.ApplicationModel;

namespace AppStudio.ViewModels
{
    public class RemoveViewModel : BindableBase
    {
        public Uri Url
        {
            get
            {
                return new Uri(UrlText, UriKind.RelativeOrAbsolute);
            }
        }
        public string UrlText
        {
            get
            {
                return "http://www.windowsphone.com/s?appid=cfe0cda0-5ee0-44d4-b25f-af3c6776479a";
            }
        }
    }
}

