using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChildScheduler.Utils
{

    public static class Dialogs
    {
        public static async Task Alert(AlertInfo info)
        {
            var task = Application.Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);
            if (task != null)
            {
                await task;
                info?.OnCompleted?.Invoke();
            }
        }

        public static async Task Question(QuestionInfo info)
        {
            var task = Application.Current?.MainPage?.DisplayAlert(info.Title, info.Question, info.Positive, info.Negative);
            if (task != null)
            {
                var result = await task;
                info?.OnCompleted?.Invoke(result);
            }
        }
        public static async Task<string> Input(QuestionInfo info)
        {
            var task = Application.Current?.MainPage?.DisplayPromptAsync(info.Title, info.Question, info.Positive, info.Negative);

            if (task != null)
            {
                var result = await task;
                return result;
            }
            return string.Empty;
        }
    }
}
