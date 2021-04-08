using System;
using System.Collections.ObjectModel;
using System.IO;
using GeoServices.Classes;
using Microsoft.Win32;

namespace GeoServices.ViewModels
{
  class MainViewModel : NotifiedEntity
  {
    #region Fields
    private string _searchString;
    private string _logString;
    private string _geoData;

    private int _counter;


    private DelegateCommand _loadDataCommand;
    private DelegateCommand _saveFileCommand;
    #endregion

    #region Properties
    public string SearchString { get => _searchString; set => SetProperty(ref _searchString, value); }
    public string LogString { get => _logString; set => SetProperty(ref _logString, value); }
    public string GeoData { get => _geoData; set => SetProperty(ref _geoData, value); }

    

    public DelegateCommand LoadDataCommand => _loadDataCommand ?? (_loadDataCommand = new DelegateCommand(LoadData));
    public DelegateCommand SaveFileCommand => _saveFileCommand ?? (_saveFileCommand = new DelegateCommand(SaveFile));
    #endregion

    public MainViewModel()
    {

    }

    #region Methods

    public void LoadData(object parameter)
    {
      LogString += String.Format("Search string {0} \n", SearchString);
    }

    public void SaveFile(object parameter)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog()
      {
        Filter = "text files (*.txt) | *.txt; ",
        InitialDirectory = Directory.GetCurrentDirectory()
      };
      if (saveFileDialog.ShowDialog() == true)
      {
        File.WriteAllText(saveFileDialog.FileName, GeoData);
        LogString += String.Format("File saved \n");
      }
      
    }
    #endregion

  }
}
