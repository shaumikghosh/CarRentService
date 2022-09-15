using DataModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentService.Interfaces {
    public interface IContact {
        string SendMail(SendMail sendMail);
    }
}
