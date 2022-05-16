using ChildScheduler.Interfaces;
using ChildScheduler.Models;
using MvvmHelpers;
using Rg.Plugins.Popup.Contracts;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChildScheduler.ViewModels
{
	public abstract class ViewModelBase<T> : BaseViewModel, INavigation
	{
		protected Page MainPage => Application.Current.MainPage;
		IDataSource<T> dataSource;

		protected IDataSource<T> DataSource =>
			dataSource ??= DependencyService.Get<IDataSource<T>>();

		ILoginProvider _loginProvider;
		protected ILoginProvider LoginProvider => _loginProvider ??= DependencyService.Get<ILoginProvider>();

		IHttpService _httpService;
		protected IHttpService HttpService => _httpService ??= DependencyService.Get<IHttpService>();

		INavigation Navigation => Application.Current?.MainPage?.Navigation;
		IPopupNavigation _popupNavigation;
		protected IPopupNavigation PopupNavigation => _popupNavigation ??= DependencyService.Get<IPopupNavigation>();

		public virtual void OnAppearing() { }
		public virtual void OnDisappearing() { }
		protected virtual Task InvokeOnMainThread(Func<Task> func) => Device.InvokeOnMainThreadAsync(func);
		protected virtual Task Alert(string message, string title = "Info")
			=> this.InvokeOnMainThread(() => this.MainPage.DisplayAlert(title, message, "OK"));

		#region INavigation implementation

		protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null) =>
			SetProperty<T>(ref field, newValue, propertyName, null, null);

		public void RemovePage(Page page)
		{
			Navigation?.RemovePage(page);
		}

		public void InsertPageBefore(Page page, Page before)
		{
			Navigation?.InsertPageBefore(page, before);
		}

		public async Task PushAsync(Page page)
		{
			var task = Navigation?.PushAsync(page);
			if (task != null)
				await task;
		}

		public async Task<Page> PopAsync()
		{
			var task = Navigation?.PopAsync();
			return task != null ? await task : await Task.FromResult(null as Page);
		}

		public async Task PopToRootAsync()
		{
			var task = Navigation?.PopToRootAsync();
			if (task != null)
				await task;
		}

		public async Task PushModalAsync(Page page)
		{
			var task = Navigation?.PushModalAsync(page);
			if (task != null)
				await task;
		}

		public async Task<Page> PopModalAsync()
		{
			var task = Navigation?.PopModalAsync();
			return task != null ? await task : await Task.FromResult(null as Page);
		}

		public async Task PushAsync(Page page, bool animated)
		{
			var task = Navigation?.PushAsync(page, animated);
			if (task != null)
				await task;
		}

		public async Task<Page> PopAsync(bool animated)
		{
			var task = Navigation?.PopAsync(animated);
			return task != null ? await task : await Task.FromResult(null as Page);
		}

		public async Task PopToRootAsync(bool animated)
		{
			var task = Navigation?.PopToRootAsync(animated);
			if (task != null)
				await task;
		}

		public async Task PushModalAsync(Page page, bool animated)
		{
			var task = Navigation?.PushModalAsync(page, animated);
			if (task != null)
				await task;
		}

		public async Task<Page> PopModalAsync(bool animated)
		{
			var task = Navigation?.PopModalAsync(animated);
			return task != null ? await task : await Task.FromResult(null as Page);
		}

		public IReadOnlyList<Page> NavigationStack => Navigation?.NavigationStack;

		public IReadOnlyList<Page> ModalStack => Navigation?.ModalStack;

		#endregion
	}
}
