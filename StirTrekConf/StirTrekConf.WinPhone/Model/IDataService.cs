using System;

namespace StirTrekConf.WinPhone.Model
{
    using System.Collections.Generic;
    using PortableCore.DomainLayer;

    public interface IDataService
    {
        void GetSpeakers(Action<List<Speaker>, Exception> callback);
    }
}
