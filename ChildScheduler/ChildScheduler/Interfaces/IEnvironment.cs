using System.Drawing;
using ChildScheduler.Models;

namespace ChildScheduler.Interfaces
{
    public interface IEnvironment
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}
