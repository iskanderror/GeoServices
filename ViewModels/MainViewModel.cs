using System;
using System.IO;
using System.Threading.Tasks;
using GeoServices.Classes;
using GeoServices.Model;
using Microsoft.Win32;

namespace GeoServices.ViewModels
{
  class MainViewModel : NotifiedEntity, IErrorHandler
  {
    #region Fields

    private bool _isGettingData;
    private bool _isSavingFile;
    private string _searchString;
    private string _logString;
    private string _geoData;

    private static IGeoDataProvider _geoDataProvider;

    #endregion

    #region Properties
    public string SearchString { get => _searchString; set => SetProperty(ref _searchString, value); }
    public string LogString { get => _logString; set => SetProperty(ref _logString, value); }
    public string GeoData { get => _geoData; set => SetProperty(ref _geoData, value); }
    public bool IsGettingData
    {
      get => _isGettingData;
      set => SetProperty(ref _isGettingData, value);
    }
    public bool IsSavingFile
    {
      get => _isSavingFile;
      set => SetProperty(ref _isSavingFile, value);
    }
    
    public IAsyncCommand LoadDataCommand { get; private set; }
    public IAsyncCommand SaveFileCommand { get; private set; }
    #endregion

    
    public MainViewModel()
    {
      _geoDataProvider = new OsmDataProvider();
      LoadDataCommand = new AsyncCommand(LoadDataAsync, CanLoadData,this);
      SaveFileCommand = new AsyncCommand(SaveFileAsync, CanSaveFile, this);
    }

    #region Commands
  
    private async Task LoadDataAsync()
    {
      try
      {
        IsGettingData = true;
        Log(String.Format("Requesting data for [{0}]",SearchString));
        GeoData = await _geoDataProvider.GetGeoData(SearchString, 0);
        Log(String.Format("Receiving data for [{0}]", SearchString));
      }
      finally
      {
        IsGettingData = false;
      }
    }

    private bool CanLoadData()
    {
      return !IsGettingData;
    }

    private async Task SaveFileAsync()
    {
      try
      {
        IsSavingFile = true;
        SaveFileDialog saveFileDialog = new SaveFileDialog()
        {
          Filter = "text files (*.txt) | *.txt; ",
          InitialDirectory = Directory.GetCurrentDirectory()
        };
        if (saveFileDialog.ShowDialog() == true)
        {
          string fileName = saveFileDialog.FileName;
          Log(String.Format("Start saving file {0}", fileName));
          using (StreamWriter sw = new StreamWriter(fileName))
          {
            await sw.WriteAsync(GeoData);
          }
          Log(String.Format("Finish saving file {0}", fileName));
        }
      }
      finally
      {
        IsSavingFile = false;
      }
    }

    private bool CanSaveFile()
    {
      return !IsSavingFile;
    }
    #endregion

    public void HandleError(Exception ex)
    {
      Log(String.Format("Exception: {0}",ex.Message)); ;
    }

    public void Log(string message)
    {
      LogString += String.Format("{0} - {1}\n", DateTime.Now, message);
    }
  }

}
