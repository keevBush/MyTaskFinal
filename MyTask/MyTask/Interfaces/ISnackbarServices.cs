using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTasks.Core.Data.Interfaces
{
    public interface ISnackbarServices
    {
        Task ShowDefaultSnackBar(string message);
        Task ShowErrorSnackBar(string message);
        Task ShowWarningSnackBar(string message);
    }
}
