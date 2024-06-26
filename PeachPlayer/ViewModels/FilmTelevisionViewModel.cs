﻿using Peach.Application.Interfaces;
using Peach.Model.Models;
using PeachPlayer.Foundation;
using Splat;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;

namespace PeachPlayer.ViewModels;

public class FilmTelevisionViewModel : ViewModelBase
{
    [Reactive]
    public ObservableCollection<ClassifyListViewModel> Types { get; set; } = new();

    private ClassifyListViewModel selectedType;
    public ClassifyListViewModel SelectedType
    {
        get { return selectedType; }
        set { selectedType = value; this.RaiseAndSetIfChanged(ref selectedType, value); selectedType?.LoadVodList(); }
    }

    public ReactiveCommand<SiteModel, Unit> SwitchSiteCommand { get; }

    [Reactive]
    public ObservableCollection<SiteModel> ListItems { get; set; }

    [Reactive]
    public bool IsLoading { get; set; }

    private ISourceService source;
    private ICmsService vod;
    public FilmTelevisionViewModel(ISourceService _source = null)
    {
        source = _source ?? Locator.Current.GetService<ISourceService>();
        vod = Locator.Current.GetService<ICmsService>();
        SwitchSiteCommand = ReactiveCommand.Create<SiteModel>(SwitchSite);
        LoadSource();

        // 接收消息
        MessageBus.Current.Listen<bool>("IsLoading").Subscribe(x => IsLoading = x);
       // MessageBus.Current.RegisterMessageSource(RootVisual.Events().KeyUpObs);
    }

    private async void LoadSource()
    {
        if (string.IsNullOrEmpty(ConfigStorage.Instance.AppConfig.SourceUrl))
        {
            _ = Interactions.ShowError.Handle("请先配置数据源地址。");
            return;
        }
        IsLoading = true;
        //测试加载数据
        var req = await source.LoadConfig(ConfigStorage.Instance.AppConfig.SourceUrl);
        if (req)
            ListItems = new ObservableCollection<SiteModel>(source.Source.Sites);
        IsLoading = false;
    }

    public async void SwitchSite(SiteModel site)
    {
        IsLoading = true;
        Types.Clear();
        await vod.InitSite(site);
        var filter = await vod.HomeAsync();
        if (filter != null && filter?.Class?.Count > 0)
        {
            var classify = filter.Class.Select(x =>
            {
                if (filter.filters?.ContainsKey(x.Type_Id) == true)
                    return new ClassifyListViewModel(x, filter.filters[x.Type_Id]);
                else
                    return new ClassifyListViewModel(x);
            });
            foreach (var item in classify)
            {
                Types.Add(item);
            }
        }
        IsLoading = false;
    }


}
